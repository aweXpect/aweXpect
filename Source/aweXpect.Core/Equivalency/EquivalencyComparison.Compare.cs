﻿using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using aweXpect.Core.Helpers;

namespace aweXpect.Equivalency;

public static partial class EquivalencyComparison
{
	private static bool CompareByValue<TActual, TExpected>(
		[DisallowNull] TActual actual,
		[DisallowNull] TExpected expected,
		StringBuilder failureBuilder,
		string memberPath,
		MemberType memberType)
	{
		if (!actual.Equals(expected))
		{
			failureBuilder.AppendLine();
			if (failureBuilder.Length > 2)
			{
				failureBuilder.AppendLine("and");
			}

			failureBuilder.Append("  ");
			failureBuilder.Append(GetMemberPath(memberType, memberPath));
			failureBuilder.AppendLine(" differed:");
			failureBuilder.Append("       Found: ");
			Formatter.Format(failureBuilder, actual, FormattingOptions.SingleLine);
			failureBuilder.AppendLine().Append("    Expected: ");
			Formatter.Format(failureBuilder, expected, FormattingOptions.SingleLine);
			return false;
		}

		return true;
	}

	private static bool CompareNulls<TActual, TExpected>(TActual actual, TExpected expected,
		StringBuilder failureBuilder, string memberPath, MemberType memberType)
	{
		if (actual is null && expected is null)
		{
			return true;
		}

		failureBuilder.AppendLine();
		if (failureBuilder.Length > 2)
		{
			failureBuilder.AppendLine("and");
		}

		failureBuilder.Append("  ");
		failureBuilder.Append(GetMemberPath(memberType, memberPath));
		failureBuilder.Append(" was ");
		Formatter.Format(failureBuilder, actual, FormattingOptions.SingleLine);
		failureBuilder.Append(" instead of ");
		Formatter.Format(failureBuilder, expected, FormattingOptions.SingleLine);
		return false;
	}

	private static string ConcatMemberPath(string memberPath, string memberName)
	{
		if (string.IsNullOrEmpty(memberPath))
		{
			return memberName;
		}

		return $"{memberPath}.{memberName}";
	}

	private static string GetMemberPath(MemberType type, string memberPath)
	{
		if (string.IsNullOrEmpty(memberPath))
		{
			return "It";
		}

		return $"{type} {memberPath}";
	}

	private enum MemberType
	{
		Property,
		Field,
		Value,
		Element,
	}
#pragma warning disable S3776 // https://rules.sonarsource.com/csharp/RSPEC-3776
#pragma warning disable S107 // https://rules.sonarsource.com/csharp/RSPEC-107
	private static bool Compare<TActual, TExpected>(
		TActual actual,
		TExpected expected,
		EquivalencyOptions equivalencyOptions,
		EquivalencyTypeOptions typeOptions,
		StringBuilder failureBuilder,
		string memberPath,
		MemberType memberType,
		EquivalencyContext context)
	{
		if (actual is null || expected is null)
		{
			return CompareNulls(actual, expected, failureBuilder, memberPath, memberType);
		}

		EquivalencyComparisonType comparisonType = typeOptions.ComparisonType
		                                           ?? equivalencyOptions.DefaultComparisonTypeSelector.Invoke(
			                                           actual.GetType());
		if (comparisonType == EquivalencyComparisonType.ByValue)
		{
			return CompareByValue(actual, expected, failureBuilder, memberPath, memberType);
		}

		try
		{
			if (!context.ComparedObjects.Add(actual) || actual.Equals(expected))
			{
				return true;
			}
		}
		catch (Exception exception)
		{
			throw new InvalidOperationException(
					$"The equals method of {Formatter.Format(actual.GetType())} threw an {Formatter.Format(exception.GetType())}: {exception.Message}",
					exception)
				.LogTrace();
		}

		if (actual is IDictionary actualDictionary && expected is IDictionary expectedDictionary)
		{
			return CompareDictionaries(actualDictionary, expectedDictionary, failureBuilder, memberPath,
				equivalencyOptions, typeOptions, context);
		}

		if (actual is IEnumerable actualEnumerable && expected is IEnumerable expectedEnumerable)
		{
			return CompareEnumerables(actualEnumerable, expectedEnumerable, failureBuilder, memberPath,
				equivalencyOptions, typeOptions, context);
		}

		return CompareObjects(actual, expected, failureBuilder, memberType, memberPath,
			equivalencyOptions, typeOptions, context);
	}

	private static bool CompareObjects<TActual, TExpected>([DisallowNull] TActual actual,
		[DisallowNull] TExpected expected,
		StringBuilder failureBuilder, MemberType memberType, string memberPath,
		EquivalencyOptions options, EquivalencyTypeOptions typeOptions, EquivalencyContext context)
	{
		bool result = true;
		int memberCount = 0;
		if (typeOptions.Fields != IncludeMembers.None)
		{
			BindingFlags fieldBindingFlags = typeOptions.Fields.GetBindingFlags();
			foreach (string fieldName in expected.GetType().GetFields(typeOptions.Fields).Select(x => x.Name))
			{
				memberCount++;
				string fieldMemberPath = ConcatMemberPath(memberPath, fieldName);
				if (typeOptions.MembersToIgnore.Contains(fieldMemberPath))
				{
					continue;
				}

				object? actualFieldValue =
					actual.GetType().GetField(fieldName, fieldBindingFlags)?.GetValue(actual);
				object? expectedFieldValue =
					expected.GetType().GetField(fieldName, fieldBindingFlags)?.GetValue(expected);

				if (!Compare(actualFieldValue, expectedFieldValue,
					    options, options.GetTypeOptions(actualFieldValue?.GetType(), typeOptions),
					    failureBuilder, fieldMemberPath, MemberType.Field, context))
				{
					result = false;
				}
			}
		}

		if (typeOptions.Properties != IncludeMembers.None)
		{
			BindingFlags propertyBindingFlags = typeOptions.Properties.GetBindingFlags();
			foreach (string propertyName in expected.GetType().GetProperties(typeOptions.Properties)
				         .Select(x => x.Name))
			{
				memberCount++;
				string propertyMemberPath = ConcatMemberPath(memberPath, propertyName);
				if (typeOptions.MembersToIgnore.Contains(propertyMemberPath))
				{
					continue;
				}

				object? actualPropertyValue = actual.GetType().GetProperty(propertyName, propertyBindingFlags)
					?.GetValue(actual);
				object? expectedPropertyValue =
					expected.GetType().GetProperty(propertyName, propertyBindingFlags)?.GetValue(expected);

				if (!Compare(actualPropertyValue, expectedPropertyValue,
					    options, options.GetTypeOptions(actualPropertyValue?.GetType(), typeOptions),
					    failureBuilder, propertyMemberPath, MemberType.Property, context))
				{
					result = false;
				}
			}
		}

		if (memberCount == 0 && actual.GetType() != expected.GetType())
		{
			failureBuilder.AppendLine();
			if (failureBuilder.Length > 2)
			{
				failureBuilder.AppendLine("and");
			}

			failureBuilder.Append("  ");
			failureBuilder.Append(GetMemberPath(memberType, memberPath));
			failureBuilder.AppendLine(" differed:");
			failureBuilder.Append("       Found: ");
			Formatter.Format(failureBuilder, actual, FormattingOptions.SingleLine);
			failureBuilder.AppendLine().Append("    Expected: ");
			Formatter.Format(failureBuilder, expected, FormattingOptions.SingleLine);
			result = false;
		}

		return result;
	}

	private static bool CompareDictionaries(
		IDictionary actual,
		IDictionary expected,
		StringBuilder failureBuilder,
		string memberPath,
		EquivalencyOptions options,
		EquivalencyTypeOptions typeOptions,
		EquivalencyContext context)
	{
		bool result = true;

		foreach (object? key in actual.Keys)
		{
			string elementMemberPath = $"{memberPath}[{key}]";
			if (typeOptions.MembersToIgnore.Contains(elementMemberPath))
			{
				continue;
			}

			object? actualObject = actual[key];
			if (expected.Contains(key))
			{
				object? expectedObject = expected[key];

				if (!Compare(actualObject, expectedObject,
					    options, options.GetTypeOptions(actualObject?.GetType(), typeOptions),
					    failureBuilder, elementMemberPath, MemberType.Element, context))
				{
					result = false;
				}
			}
			else
			{
				failureBuilder.AppendLine();
				if (failureBuilder.Length > 2)
				{
					failureBuilder.AppendLine("and");
				}

				failureBuilder.Append("  ");
				failureBuilder.Append(GetMemberPath(MemberType.Element, elementMemberPath));
				failureBuilder.Append(" had superfluous ");
				Formatter.Format(failureBuilder, actualObject, FormattingOptions.SingleLine);
				result = false;
			}
		}

		foreach (object? key in expected.Keys)
		{
			if (actual.Contains(key))
			{
				continue;
			}

			string elementMemberPath = $"{memberPath}[{key}]";
			if (typeOptions.MembersToIgnore.Contains(elementMemberPath))
			{
				continue;
			}


			object? expectedObject = expected[key];

			failureBuilder.AppendLine();
			if (failureBuilder.Length > 2)
			{
				failureBuilder.AppendLine("and");
			}

			failureBuilder.Append("  ");
			failureBuilder.Append(GetMemberPath(MemberType.Element, elementMemberPath));
			failureBuilder.Append(" was missing ");
			Formatter.Format(failureBuilder, expectedObject, FormattingOptions.SingleLine);
			result = false;
		}

		return result;
	}

	private static bool CompareEnumerables(
		IEnumerable actual,
		IEnumerable expected,
		StringBuilder failureBuilder,
		string memberPath,
		EquivalencyOptions options,
		EquivalencyTypeOptions typeOptions,
		EquivalencyContext context)
	{
		bool result = true;
		object?[] actualObjects = actual.Cast<object?>().ToArray();
		object?[] expectedObjects = expected.Cast<object?>().ToArray();

		int[]? keys = null;
		if (typeOptions.IgnoreCollectionOrder)
		{
			keys = new int[actualObjects.Length];
			for (int i = 0; i < actualObjects.Length; i++)
			{
				keys[i] = i;
			}

			Array.Sort(actualObjects, keys);
			Array.Sort(expectedObjects);
		}

		for (int i = 0; i < Math.Min(actualObjects.Length, expectedObjects.Length); i++)
		{
			string elementMemberPath = $"{memberPath}[{(keys is null ? i : keys[i])}]";
			if (typeOptions.MembersToIgnore.Contains(elementMemberPath))
			{
				continue;
			}

			object? actualObject = actualObjects.ElementAtOrDefault(i);
			object? expectedObject = expectedObjects.ElementAtOrDefault(i);

			if (!Compare(actualObject, expectedObject,
				    options, options.GetTypeOptions(actualObject?.GetType(), typeOptions),
				    failureBuilder, elementMemberPath, MemberType.Element, context))
			{
				result = false;
			}
		}

		if (expectedObjects.Length > actualObjects.Length)
		{
			for (int i = actualObjects.Length; i < expectedObjects.Length; i++)
			{
				string elementMemberPath = $"{memberPath}[{i}]";
				if (typeOptions.MembersToIgnore.Contains(elementMemberPath))
				{
					continue;
				}

				object? expectedObject = expectedObjects.ElementAtOrDefault(i);

				failureBuilder.AppendLine();
				if (failureBuilder.Length > 2)
				{
					failureBuilder.AppendLine("and");
				}

				failureBuilder.Append("  ");
				failureBuilder.Append(GetMemberPath(MemberType.Element, elementMemberPath));
				failureBuilder.Append(" was missing ");
				Formatter.Format(failureBuilder, expectedObject, FormattingOptions.SingleLine);
				result = false;
			}
		}

		if (expectedObjects.Length < actualObjects.Length)
		{
			for (int i = expectedObjects.Length; i < actualObjects.Length; i++)
			{
				string elementMemberPath = $"{memberPath}[{(keys is null ? i : keys[i])}]";
				if (typeOptions.MembersToIgnore.Contains(elementMemberPath))
				{
					continue;
				}

				object? actualObject = actualObjects.ElementAtOrDefault(i);

				failureBuilder.AppendLine();
				if (failureBuilder.Length > 2)
				{
					failureBuilder.AppendLine("and");
				}

				failureBuilder.Append("  ");
				failureBuilder.Append(GetMemberPath(MemberType.Element, elementMemberPath));
				failureBuilder.Append(" had superfluous ");
				Formatter.Format(failureBuilder, actualObject, FormattingOptions.SingleLine);
				result = false;
			}
		}

		return result;
	}
#pragma warning restore S107
#pragma warning restore S3776
}

// Copyright (c) 2024 by Tom Longhurst
// https://github.com/thomhurst/TUnit

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using aweXpect.Helpers;

namespace aweXpect.Equivalency;

internal static class Compare
{
	private static readonly BindingFlags BindingFlags =
		BindingFlags.Instance
		| BindingFlags.Static
		| BindingFlags.Public
		| BindingFlags.NonPublic
		| BindingFlags.FlattenHierarchy;

	public static bool CheckEquivalent<TActual, TExpected>(
		TActual actual,
		TExpected expected,
		CompareOptions options,
		StringBuilder failureBuilder)
		=> CheckEquivalent(actual, expected, options, failureBuilder, "", MemberType.Value,
			new EquivalencyContext());

	private static bool CheckEquivalent<TActual, TExpected>(
		TActual actual,
		TExpected expected,
		CompareOptions options,
		StringBuilder failureBuilder,
		string memberPath,
		MemberType memberType,
		EquivalencyContext context)
	{
		if (actual is null && expected is null)
		{
			return true;
		}

		if (actual is null || expected is null)
		{
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

		if (actual.GetType().IsSimpleType())
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

		if (!context.ComparedObjects.Add(actual) || actual.Equals(expected))
		{
			return true;
		}

		bool result = true;
		if (actual is IEnumerable actualEnumerable && expected is IEnumerable expectedEnumerable)
		{
			object?[] actualObjects = actualEnumerable.Cast<object?>().ToArray();
			object?[] expectedObjects = expectedEnumerable.Cast<object?>().ToArray();

			int[]? keys = null;
			if (options.IgnoreCollectionOrder)
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
				if (options.MembersToIgnore.Contains(elementMemberPath))
				{
					continue;
				}

				object? actualObject = actualObjects.ElementAtOrDefault(i);
				object? expectedObject = expectedObjects.ElementAtOrDefault(i);

				if (!CheckEquivalent(actualObject, expectedObject, options, failureBuilder,
					    elementMemberPath, MemberType.Element, context))
				{
					result = false;
				}
			}

			if (expectedObjects.Length > actualObjects.Length)
			{
				for (int i = actualObjects.Length; i < expectedObjects.Length; i++)
				{
					string elementMemberPath = $"{memberPath}[{i}]";
					if (options.MembersToIgnore.Contains(elementMemberPath))
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
					if (options.MembersToIgnore.Contains(elementMemberPath))
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

		foreach (string? fieldName in actual.GetType().GetFields().Concat(expected.GetType().GetFields())
			         .Where(x => !x.Name.StartsWith('<'))
			         .Select(x => x.Name)
			         .Distinct())
		{
			string fieldMemberPath = ConcatMemberPath(memberPath, fieldName);
			if (options.MembersToIgnore.Contains(fieldMemberPath))
			{
				continue;
			}

			object? actualFieldValue = actual.GetType().GetField(fieldName, BindingFlags)?.GetValue(actual);
			object? expectedFieldValue = expected.GetType().GetField(fieldName, BindingFlags)?.GetValue(expected);

			if (!CheckEquivalent(actualFieldValue, expectedFieldValue, options, failureBuilder,
				    fieldMemberPath, MemberType.Field, context))
			{
				result = false;
			}
		}

		foreach (string? propertyName in actual.GetType().GetProperties().Concat(expected.GetType().GetProperties())
			         .Where(p => p.GetIndexParameters().Length == 0)
			         .Select(x => x.Name)
			         .Distinct())
		{
			string propertyMemberPath = ConcatMemberPath(memberPath, propertyName);
			if (options.MembersToIgnore.Contains(propertyMemberPath))
			{
				continue;
			}

			object? actualPropertyValue = actual.GetType().GetProperty(propertyName, BindingFlags)?.GetValue(actual);
			object? expectedPropertyValue =
				expected.GetType().GetProperty(propertyName, BindingFlags)?.GetValue(expected);

			if (!CheckEquivalent(actualPropertyValue, expectedPropertyValue, options, failureBuilder,
				    propertyMemberPath, MemberType.Property, context))
			{
				result = false;
			}
		}

		return result;
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

	private sealed class EquivalencyContext
	{
		/// <summary>
		///     Tracks already compared objects to catch recursions.
		/// </summary>
		public HashSet<object> ComparedObjects { get; } = new();
	}
}

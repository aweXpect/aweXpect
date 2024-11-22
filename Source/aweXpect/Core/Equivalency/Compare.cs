// Copyright (c) 2024 by Tom Longhurst
// https://github.com/thomhurst/TUnit

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Testably.Expectations.Core.Equivalency;

internal static class Compare
{
	internal static IEnumerable<ComparisonFailure> CheckEquivalent<T>(T actual, T expected,
		CompareOptions options)
	{
		return CheckEquivalent(actual, expected, options, [], MemberType.Value);
	}

	internal static IEnumerable<ComparisonFailure> CheckEquivalent<T>(T actual, T expected,
		CompareOptions options, string[] memberNames, MemberType memberType)
	{
		if (actual is null && expected is null)
		{
			yield break;
		}

		if (actual is null || expected is null)
		{
			yield return new ComparisonFailure
			{
				Type = memberType,
				Actual = actual,
				Expected = expected,
				NestedMemberNames = memberNames
			};

			yield break;
		}

		if (actual is IEnumerable actualEnumerable && expected is IEnumerable expectedEnumerable)
		{
			object[] actualObjects = actualEnumerable.Cast<object>().ToArray();
			object[] expectedObjects = expectedEnumerable.Cast<object>().ToArray();

			for (int i = 0; i < Math.Max(actualObjects.Length, expectedObjects.Length); i++)
			{
				string?[] readOnlySpan = [..memberNames, $"[{i}]"];

				if (options.MembersToIgnore.Contains(string.Join(".", readOnlySpan)))
				{
					continue;
				}

				object? actualObject = actualObjects.ElementAtOrDefault(i);
				object? expectedObject = expectedObjects.ElementAtOrDefault(i);

				foreach (ComparisonFailure comparisonFailure in CheckEquivalent(actualObject,
					expectedObject, options, [..memberNames, $"[{i}]"], MemberType.EnumerableItem))
				{
					yield return comparisonFailure;
				}
			}
		}

		if (actual.GetType().IsPrimitive
		    || actual.GetType().IsEnum
		    || actual.GetType().IsValueType
		    || actual is string)
		{
			if (!actual.Equals(expected))
			{
				yield return new ComparisonFailure
				{
					Type = MemberType.Value,
					Actual = actual,
					Expected = expected,
					NestedMemberNames = memberNames
				};
			}

			yield break;
		}

		foreach (FieldInfo fieldInfo in actual.GetType().GetFields()
			.Concat(expected.GetType().GetFields()).Distinct())
		{
			string?[] readOnlySpan = [..memberNames, fieldInfo.Name];

			if (options.MembersToIgnore.Contains(string.Join(".", readOnlySpan)))
			{
				continue;
			}

			object? actualFieldValue = fieldInfo.GetValue(actual);
			object? expectedFieldValue = fieldInfo.GetValue(expected);

			if (actualFieldValue?.Equals(actual) == true &&
			    expectedFieldValue?.Equals(expected) == true)
			{
				// To prevent cyclical references/stackoverflow
				continue;
			}

			if (actualFieldValue?.Equals(actual) == true ||
			    expectedFieldValue?.Equals(expected) == true)
			{
				yield return new ComparisonFailure
				{
					Type = MemberType.Value,
					Actual = actual,
					Expected = expected,
					NestedMemberNames = memberNames
				};

				yield break;
			}

			foreach (ComparisonFailure comparisonFailure in CheckEquivalent(actualFieldValue,
				expectedFieldValue, options, [..memberNames, fieldInfo.Name], MemberType.Field))
			{
				yield return comparisonFailure;
			}
		}

		foreach (PropertyInfo propertyInfo in actual.GetType().GetProperties()
			.Concat(expected.GetType().GetProperties())
			.Distinct()
			.Where(p => p.GetIndexParameters().Length == 0))
		{
			string?[] readOnlySpan = [..memberNames, propertyInfo.Name];

			if (options.MembersToIgnore.Contains(string.Join(".", readOnlySpan)))
			{
				continue;
			}

			object? actualPropertyValue = propertyInfo.GetValue(actual);
			object? expectedPropertyValue = propertyInfo.GetValue(expected);

			if (actualPropertyValue?.Equals(actual) == true &&
			    expectedPropertyValue?.Equals(expected) == true)
			{
				// To prevent cyclical references/stackoverflow
				continue;
			}

			if (actualPropertyValue?.Equals(actual) == true ||
			    actualPropertyValue?.Equals(expected) == true)
			{
				yield return new ComparisonFailure
				{
					Type = MemberType.Value,
					Actual = actual,
					Expected = expected,
					NestedMemberNames = memberNames
				};

				yield break;
			}

			foreach (ComparisonFailure comparisonFailure in CheckEquivalent(actualPropertyValue,
				expectedPropertyValue, options, [..memberNames, propertyInfo.Name],
				MemberType.Property))
			{
				yield return comparisonFailure;
			}
		}
	}
}

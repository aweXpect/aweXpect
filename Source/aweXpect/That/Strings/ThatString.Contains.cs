﻿using System;
using System.Diagnostics.CodeAnalysis;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatString
{
	/// <summary>
	///     Verifies that the subject contains the <paramref name="expected" /> <see langword="string" />.
	/// </summary>
	public static StringEqualityTypeCountResult<string?, IThat<string?>> Contains(
		this IThat<string?> source,
		string expected)
	{
		Quantifier quantifier = new();
		StringEqualityOptions options = new();
		return new StringEqualityTypeCountResult<string?, IThat<string?>>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ContainsConstraint(it, grammars, expected, quantifier, options)),
			source,
			quantifier,
			options);
	}

	/// <summary>
	///     Verifies that the subject contains the <paramref name="unexpected" /> <see langword="string" />.
	/// </summary>
	public static StringEqualityTypeResult<string?, IThat<string?>> DoesNotContain(
		this IThat<string?> source,
		string unexpected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityTypeResult<string?, IThat<string?>>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ContainsConstraint(it, grammars, unexpected, Quantifier.Never(), options)),
			source,
			options);
	}

	private sealed class ContainsConstraint(
		string it,
		ExpectationGrammars grammars,
		string expected,
		Quantifier quantifier,
		StringEqualityOptions options)
		: ConstraintResult(grammars),
			IValueConstraint<string?>
	{
		private string? _actual;
		private int _actualCount;

		/// <inheritdoc />
		public ConstraintResult IsMetBy(string? actual)
		{
			_actual = actual;
			if (actual is null)
			{
				Outcome = Outcome.Failure;
				return this;
			}

			_actualCount = CountOccurrences(actual, expected, options);
			Outcome = quantifier.Check(_actualCount, true) == true ? Outcome.Success : Outcome.Failure;
			return this;
		}

		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			string quantifierString = quantifier.ToString();
			if (quantifierString == "never")
			{
				stringBuilder.Append("does not contain ");
				Formatter.Format(stringBuilder, expected);
			}
			else
			{
				stringBuilder.Append("contains ");
				Formatter.Format(stringBuilder, expected);
				stringBuilder.Append(' ').Append(quantifier);
			}

			stringBuilder.Append(options);
		}

		/// <inheritdoc cref="ConstraintResult.TryGetValue{TValue}(out TValue)" />
		public override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value) where TValue : default
		{
			if (_actual is TValue typedValue)
			{
				value = typedValue;
				return true;
			}

			value = default;
			return typeof(TValue).IsAssignableFrom(typeof(string));
		}

		private static int CountOccurrences(string actual, string expected,
			StringEqualityOptions comparer)
		{
			if (expected.Length > actual.Length)
			{
				return 0;
			}

			int count = 0;
			int index = 0;
			while (index < actual.Length)
			{
				if (comparer.AreConsideredEqual(
					    actual.Substring(index, Math.Min(expected.Length, actual.Length - index)),
					    expected))
				{
					count++;
					index += expected.Length;
				}
				else
				{
					index++;
				}
			}

			return count;
		}

		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (_actual is null)
			{
				stringBuilder.ItWasNull(it);
			}
			else
			{
				stringBuilder.Append(it).Append(" contained it ").Append(_actualCount).Append(" times in ");
				Formatter.Format(stringBuilder, _actual);
			}
		}

		public override ConstraintResult Negate()
		{
			quantifier.Negate();
			Outcome = Outcome switch
			{
				Outcome.Failure => Outcome.Success,
				Outcome.Success => Outcome.Failure,
				_ => Outcome,
			};
			return this;
		}
	}
}

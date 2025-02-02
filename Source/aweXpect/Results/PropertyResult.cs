﻿using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;

namespace aweXpect.Results;

/// <summary>
///     Result for a property.
/// </summary>
public static class PropertyResult
{
	/// <summary>
	///     Result for an <see langword="int" /> property.
	/// </summary>
	public class Int<TItem>(
		IThat<TItem> source,
		Func<TItem, int?> mapper,
		string propertyExpression,
		Action<int?, string>? validation = null)
	{
		/// <summary>
		///     … is equal to the <paramref name="expected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> EqualTo(
			int? expected)
		{
			validation?.Invoke(expected, nameof(expected));
			return new AndOrResult<TItem, IThat<TItem>>(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, int>(
						it,
						expected,
						mapper,
						propertyExpression,
						(a, e) => a?.Equals(e) == true,
						$"have {propertyExpression} equal to {Formatter.Format(expected)}")),
				source);
		}

		/// <summary>
		///     … is not equal to the <paramref name="unexpected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> NotEqualTo(
			int? unexpected)
		{
			validation?.Invoke(unexpected, nameof(unexpected));
			return new AndOrResult<TItem, IThat<TItem>>(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, int>(
						it,
						unexpected,
						mapper,
						propertyExpression,
						(a, u) => a?.Equals(u) != true,
						$"have {propertyExpression} not equal to {Formatter.Format(unexpected)}")),
				source);
		}
		
		/// <summary>
		///     … is greater than the <paramref name="expected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> GreaterThan(
			int? expected)
		{
			validation?.Invoke(expected, nameof(expected));
			return new AndOrResult<TItem, IThat<TItem>>(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, int>(
						it,
						expected,
						mapper,
						propertyExpression,
						(a, e) => a > e,
						$"have {propertyExpression} greater than {Formatter.Format(expected)}")),
				source);
		}
		
		/// <summary>
		///     … is greater than or equal to the <paramref name="expected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> GreaterThanOrEqualTo(
			int? expected)
		{
			validation?.Invoke(expected, nameof(expected));
			return new AndOrResult<TItem, IThat<TItem>>(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, int>(
						it,
						expected,
						mapper,
						propertyExpression,
						(a, e) => a >= e,
						$"have {propertyExpression} greater than or equal to {Formatter.Format(expected)}")),
				source);
		}
		
		/// <summary>
		///     … is less than the <paramref name="expected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> LessThan(
			int? expected)
		{
			validation?.Invoke(expected, nameof(expected));
			return new AndOrResult<TItem, IThat<TItem>>(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, int>(
						it,
						expected,
						mapper,
						propertyExpression,
						(a, e) => a < e,
						$"have {propertyExpression} less than {Formatter.Format(expected)}")),
				source);
		}
		
		/// <summary>
		///     … is less than or equal to the <paramref name="expected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> LessThanOrEqualTo(
			int? expected)
		{
			validation?.Invoke(expected, nameof(expected));
			return new AndOrResult<TItem, IThat<TItem>>(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, int>(
						it,
						expected,
						mapper,
						propertyExpression,
						(a, e) => a <= e,
						$"have {propertyExpression} less than or equal to {Formatter.Format(expected)}")),
				source);
		}
	}

	/// <summary>
	///     Result for a nullable <see langword="int" /> property.
	/// </summary>
	public class NullableInt<TItem>(
		IThat<TItem> source,
		Func<TItem, int?> mapper,
		string propertyExpression,
		Action<int?, string>? validation = null)
	{
		/// <summary>
		///     … is equal to the <paramref name="expected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> EqualTo(
			int? expected)
		{
			validation?.Invoke(expected, nameof(expected));
			return new AndOrResult<TItem, IThat<TItem>>(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, int>(
						it,
						expected,
						mapper,
						propertyExpression,
						(a, e) => a?.Equals(e) == true,
						$"have {propertyExpression} equal to {Formatter.Format(expected)}")),
				source);
		}

		/// <summary>
		///     … is not equal to the <paramref name="unexpected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> NotEqualTo(
			int? unexpected)
		{
			validation?.Invoke(unexpected, nameof(unexpected));
			return new AndOrResult<TItem, IThat<TItem>>(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, int>(
						it,
						unexpected,
						mapper,
						propertyExpression,
						(a, u) => a?.Equals(u) != true,
						$"have {propertyExpression} not equal to {Formatter.Format(unexpected)}")),
				source);
		}
		
		/// <summary>
		///     … is greater than the <paramref name="expected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> GreaterThan(
			int? expected)
		{
			validation?.Invoke(expected, nameof(expected));
			return new AndOrResult<TItem, IThat<TItem>>(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, int>(
						it,
						expected,
						mapper,
						propertyExpression,
						(a, e) => a > e,
						$"have {propertyExpression} greater than {Formatter.Format(expected)}")),
				source);
		}
		
		/// <summary>
		///     … is greater than or equal to the <paramref name="expected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> GreaterThanOrEqualTo(
			int? expected)
		{
			validation?.Invoke(expected, nameof(expected));
			return new AndOrResult<TItem, IThat<TItem>>(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, int>(
						it,
						expected,
						mapper,
						propertyExpression,
						(a, e) => a >= e,
						$"have {propertyExpression} greater than or equal to {Formatter.Format(expected)}")),
				source);
		}
		
		/// <summary>
		///     … is less than the <paramref name="expected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> LessThan(
			int? expected)
		{
			validation?.Invoke(expected, nameof(expected));
			return new AndOrResult<TItem, IThat<TItem>>(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, int>(
						it,
						expected,
						mapper,
						propertyExpression,
						(a, e) => a < e,
						$"have {propertyExpression} less than {Formatter.Format(expected)}")),
				source);
		}
		
		/// <summary>
		///     … is less than or equal to the <paramref name="expected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> LessThanOrEqualTo(
			int? expected)
		{
			validation?.Invoke(expected, nameof(expected));
			return new AndOrResult<TItem, IThat<TItem>>(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, int>(
						it,
						expected,
						mapper,
						propertyExpression,
						(a, e) => a <= e,
						$"have {propertyExpression} less than or equal to {Formatter.Format(expected)}")),
				source);
		}
	}

	/// <summary>
	///     Result for a nullable <see langword="long" /> property.
	/// </summary>
	public class NullableLong<TItem>(
		IThat<TItem> source,
		Func<TItem, long?> mapper,
		string propertyExpression,
		Action<long?, string>? validation = null)
	{
		/// <summary>
		///     … is equal to the <paramref name="expected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> EqualTo(
			long? expected)
		{
			validation?.Invoke(expected, nameof(expected));
			return new AndOrResult<TItem, IThat<TItem>>(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, long>(
						it,
						expected,
						mapper,
						propertyExpression,
						(a, e) => a?.Equals(e) == true,
						$"have {propertyExpression} equal to {Formatter.Format(expected)}")),
				source);
		}

		/// <summary>
		///     … is not equal to the <paramref name="unexpected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> NotEqualTo(
			long? unexpected)
		{
			validation?.Invoke(unexpected, nameof(unexpected));
			return new AndOrResult<TItem, IThat<TItem>>(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, long>(
						it,
						unexpected,
						mapper,
						propertyExpression,
						(a, u) => a?.Equals(u) != true,
						$"have {propertyExpression} not equal to {Formatter.Format(unexpected)}")),
				source);
		}
		
		/// <summary>
		///     … is greater than the <paramref name="expected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> GreaterThan(
			long? expected)
		{
			validation?.Invoke(expected, nameof(expected));
			return new AndOrResult<TItem, IThat<TItem>>(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, long>(
						it,
						expected,
						mapper,
						propertyExpression,
						(a, e) => a > e,
						$"have {propertyExpression} greater than {Formatter.Format(expected)}")),
				source);
		}
		
		/// <summary>
		///     … is greater than or equal to the <paramref name="expected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> GreaterThanOrEqualTo(
			long? expected)
		{
			validation?.Invoke(expected, nameof(expected));
			return new AndOrResult<TItem, IThat<TItem>>(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, long>(
						it,
						expected,
						mapper,
						propertyExpression,
						(a, e) => a >= e,
						$"have {propertyExpression} greater than or equal to {Formatter.Format(expected)}")),
				source);
		}
		
		/// <summary>
		///     … is less than the <paramref name="expected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> LessThan(
			long? expected)
		{
			validation?.Invoke(expected, nameof(expected));
			return new AndOrResult<TItem, IThat<TItem>>(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, long>(
						it,
						expected,
						mapper,
						propertyExpression,
						(a, e) => a < e,
						$"have {propertyExpression} less than {Formatter.Format(expected)}")),
				source);
		}
		
		/// <summary>
		///     … is less than or equal to the <paramref name="expected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> LessThanOrEqualTo(
			long? expected)
		{
			validation?.Invoke(expected, nameof(expected));
			return new AndOrResult<TItem, IThat<TItem>>(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, long>(
						it,
						expected,
						mapper,
						propertyExpression,
						(a, e) => a <= e,
						$"have {propertyExpression} less than or equal to {Formatter.Format(expected)}")),
				source);
		}
	}

	/// <summary>
	///     Result for a <see cref="DateTimeKind" /> property.
	/// </summary>
	public class DateTimeKind<TItem>(
		IThat<TItem> source,
		Func<TItem, DateTimeKind?> mapper,
		string propertyExpression)
	{
		/// <summary>
		///     … is equal to the <paramref name="expected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> EqualTo(
			DateTimeKind expected)
			=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, DateTimeKind>(
						it,
						expected,
						mapper,
						propertyExpression,
						(a, e) => a?.Equals(e) == true,
						$"have {propertyExpression} equal to {Formatter.Format(expected)}")),
				source);

		/// <summary>
		///     … is not equal to the <paramref name="unexpected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> NotEqualTo(
			DateTimeKind unexpected)
			=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, DateTimeKind>(
						it,
						unexpected,
						mapper,
						propertyExpression,
						(a, u) => a?.Equals(u) != true,
						$"have {propertyExpression} not equal to {Formatter.Format(unexpected)}")),
				source);
	}

	/// <summary>
	///     Result for a <see cref="DateTimeKind" /> property.
	/// </summary>
	public class TimeSpan<TItem>(
		IThat<TItem> source,
		Func<TItem, TimeSpan?> mapper,
		string propertyExpression)
	{
		/// <summary>
		///     … is equal to the <paramref name="expected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> EqualTo(
			TimeSpan? expected)
			=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, TimeSpan>(
						it,
						expected,
						mapper,
						propertyExpression,
						(a, e) => a?.Equals(e) == true,
						$"have {propertyExpression} equal to {Formatter.Format(expected)}")),
				source);

		/// <summary>
		///     … is not equal to the <paramref name="unexpected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> NotEqualTo(
			TimeSpan? unexpected)
			=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, TimeSpan>(
						it,
						unexpected,
						mapper,
						propertyExpression,
						(a, u) => a?.Equals(u) != true,
						$"have {propertyExpression} not equal to {Formatter.Format(unexpected)}")),
				source);
	}

	private readonly struct PropertyConstraint<TItem, TProperty>(
		string it,
		TProperty? expected,
		Func<TItem, TProperty?> mapper,
		string propertyExpression,
		Func<TProperty?, TProperty?, bool> condition,
		string expectation) : IValueConstraint<TItem>
		where TProperty : struct
	{
		public ConstraintResult IsMetBy(TItem actual)
		{
			TProperty? value = mapper(actual);
			if (condition(value, expected))
			{
				return new ConstraintResult.Success<TItem>(actual, ToString());
			}

			return new ConstraintResult.Failure<TItem>(actual, ToString(),
				actual is null
					? $"{it} was <null>"
					: $"{it} had {propertyExpression} {Formatter.Format(value)}");
		}

		public override string ToString()
			=> expectation;
	}
}

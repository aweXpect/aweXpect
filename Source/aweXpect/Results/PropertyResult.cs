using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;

namespace aweXpect.Results;

/// <summary>
///     Result for a property.
/// </summary>
public class PropertyResult
{
	/// <summary>
	///     Result for an <see langword="int" /> property.
	/// </summary>
	public class Int<TItem>(
		IThat<TItem> source,
		Func<TItem, int> mapper,
		string propertyExpression)
	{
		/// <summary>
		///     … is equal to the <paramref name="expected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> EqualTo(
			int? expected)
			=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, int?>(
						it,
						expected,
						(a, _) => mapper(a).Equals(expected),
						$"have {propertyExpression} equal to {Formatter.Format(expected)}")),
				source);

		/// <summary>
		///     … is not equal to the <paramref name="unexpected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> NotEqualTo(
			int? unexpected)
			=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, int?>(
						it,
						unexpected,
						(a, _) => !mapper(a).Equals(unexpected),
						$"have {propertyExpression} not equal to {Formatter.Format(unexpected)}")),
				source);
	}

	/// <summary>
	///     Result for a nullable <see langword="int" /> property.
	/// </summary>
	public class NullableInt<TItem>(
		IThat<TItem> source,
		Func<TItem, int?> mapper,
		string propertyExpression)
	{
		/// <summary>
		///     … is equal to the <paramref name="expected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> EqualTo(
			int? expected)
			=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, int?>(
						it,
						expected,
						(a, _) => mapper(a)?.Equals(expected) == true,
						$"have {propertyExpression} equal to {Formatter.Format(expected)}")),
				source);

		/// <summary>
		///     … is not equal to the <paramref name="unexpected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> NotEqualTo(
			int? unexpected)
			=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, int?>(
						it,
						unexpected,
						(a, _) => mapper(a)?.Equals(unexpected) != true,
						$"have {propertyExpression} not equal to {Formatter.Format(unexpected)}")),
				source);
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
						(a, _) => mapper(a)?.Equals(expected) == true,
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
						(a, _) => mapper(a)?.Equals(unexpected) != true,
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
					new PropertyConstraint<TItem, TimeSpan?>(
						it,
						expected,
						(a, _) => mapper(a)?.Equals(expected) == true,
						$"have {propertyExpression} equal to {Formatter.Format(expected)}")),
				source);

		/// <summary>
		///     … is not equal to the <paramref name="unexpected" /> value.
		/// </summary>
		public AndOrResult<TItem, IThat<TItem>> NotEqualTo(
			TimeSpan? unexpected)
			=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
					new PropertyConstraint<TItem, TimeSpan?>(
						it,
						unexpected,
						(a, _) => mapper(a)?.Equals(unexpected) != true,
						$"have {propertyExpression} not equal to {Formatter.Format(unexpected)}")),
				source);
	}


	private readonly struct PropertyConstraint<TItem, TProperty>(
		string it,
		TProperty expected,
		Func<TItem, TProperty, bool> condition,
		string expectation) : IValueConstraint<TItem>
	{
		public ConstraintResult IsMetBy(TItem actual)
		{
			if (condition(actual, expected))
			{
				return new ConstraintResult.Success<TItem>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(), $"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> expectation;
	}
}

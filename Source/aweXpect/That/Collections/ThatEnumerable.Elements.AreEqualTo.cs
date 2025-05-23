using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     …are equal to the <paramref name="expected" /> value.
	/// </summary>
	public static ToleranceEqualityResult<TEnumerable, IThat<TEnumerable>, double, double>
		AreEqualTo<TEnumerable>(this Elements<double, TEnumerable> elements, double expected)
		where TEnumerable : IEnumerable<double>
	{
		IElements<TEnumerable> iElements = elements;
		ObjectEqualityWithToleranceOptions<double, double> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateDouble();
		ExpectationBuilder expectationBuilder = iElements.Subject.Get().ExpectationBuilder;
		return new ToleranceEqualityResult<TEnumerable, IThat<TEnumerable>, double, double>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new CollectionConstraint<double>(
					expectationBuilder,
					it, grammars,
					iElements.Quantifier,
					g => (g.HasAnyFlag(ExpectationGrammars.Nested, ExpectationGrammars.Plural),
							g.IsNegated()) switch
						{
							(true, false) => $"are equal to {Formatter.Format(expected)}{options}",
							(false, false) => $"is equal to {Formatter.Format(expected)}{options}",
							(true, true) => $"are not equal to {Formatter.Format(expected)}{options}",
							(false, true) => $"is not equal to {Formatter.Format(expected)}{options}",
						},
					a => options.AreConsideredEqual(a, expected),
					"were")),
			iElements.Subject,
			options);
	}

	/// <summary>
	///     …are equal to the <paramref name="expected" /> value.
	/// </summary>
	public static ToleranceEqualityResult<TEnumerable, IThat<TEnumerable>, double?, double>
		AreEqualTo<TEnumerable>(this Elements<double?, TEnumerable> elements, double? expected)
		where TEnumerable : IEnumerable<double?>
	{
		IElements<TEnumerable> iElements = elements;
		ObjectEqualityWithToleranceOptions<double?, double> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateNullableDouble();
		ExpectationBuilder expectationBuilder = iElements.Subject.Get().ExpectationBuilder;
		return new ToleranceEqualityResult<TEnumerable, IThat<TEnumerable>, double?, double>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new CollectionConstraint<double?>(
					expectationBuilder,
					it, grammars,
					iElements.Quantifier,
					g => (g.HasAnyFlag(ExpectationGrammars.Nested, ExpectationGrammars.Plural),
							g.IsNegated()) switch
						{
							(true, false) => $"are equal to {Formatter.Format(expected)}{options}",
							(false, false) => $"is equal to {Formatter.Format(expected)}{options}",
							(true, true) => $"are not equal to {Formatter.Format(expected)}{options}",
							(false, true) => $"is not equal to {Formatter.Format(expected)}{options}",
						},
					a => options.AreConsideredEqual(a, expected),
					"were")),
			iElements.Subject,
			options);
	}

	/// <summary>
	///     …are equal to the <paramref name="expected" /> value.
	/// </summary>
	public static ToleranceEqualityResult<TEnumerable, IThat<TEnumerable>, float, float>
		AreEqualTo<TEnumerable>(this Elements<float, TEnumerable> elements, float expected)
		where TEnumerable : IEnumerable<float>
	{
		IElements<TEnumerable> iElements = elements;
		ObjectEqualityWithToleranceOptions<float, float> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateFloat();
		ExpectationBuilder expectationBuilder = iElements.Subject.Get().ExpectationBuilder;
		return new ToleranceEqualityResult<TEnumerable, IThat<TEnumerable>, float, float>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new CollectionConstraint<float>(
					expectationBuilder,
					it, grammars,
					iElements.Quantifier,
					g => (g.HasAnyFlag(ExpectationGrammars.Nested, ExpectationGrammars.Plural),
							g.IsNegated()) switch
						{
							(true, false) => $"are equal to {Formatter.Format(expected)}{options}",
							(false, false) => $"is equal to {Formatter.Format(expected)}{options}",
							(true, true) => $"are not equal to {Formatter.Format(expected)}{options}",
							(false, true) => $"is not equal to {Formatter.Format(expected)}{options}",
						},
					a => options.AreConsideredEqual(a, expected),
					"were")),
			iElements.Subject,
			options);
	}

	/// <summary>
	///     …are equal to the <paramref name="expected" /> value.
	/// </summary>
	public static ToleranceEqualityResult<TEnumerable, IThat<TEnumerable>, float?, float>
		AreEqualTo<TEnumerable>(this Elements<float?, TEnumerable> elements, float? expected)
		where TEnumerable : IEnumerable<float?>
	{
		IElements<TEnumerable> iElements = elements;
		ObjectEqualityWithToleranceOptions<float?, float> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateNullableFloat();
		ExpectationBuilder expectationBuilder = iElements.Subject.Get().ExpectationBuilder;
		return new ToleranceEqualityResult<TEnumerable, IThat<TEnumerable>, float?, float>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new CollectionConstraint<float?>(
					expectationBuilder,
					it, grammars,
					iElements.Quantifier,
					g => (g.HasAnyFlag(ExpectationGrammars.Nested, ExpectationGrammars.Plural),
							g.IsNegated()) switch
						{
							(true, false) => $"are equal to {Formatter.Format(expected)}{options}",
							(false, false) => $"is equal to {Formatter.Format(expected)}{options}",
							(true, true) => $"are not equal to {Formatter.Format(expected)}{options}",
							(false, true) => $"is not equal to {Formatter.Format(expected)}{options}",
						},
					a => options.AreConsideredEqual(a, expected),
					"were")),
			iElements.Subject,
			options);
	}

	/// <summary>
	///     …are equal to the <paramref name="expected" /> value.
	/// </summary>
	public static ToleranceEqualityResult<TEnumerable, IThat<TEnumerable>, decimal, decimal>
		AreEqualTo<TEnumerable>(this Elements<decimal, TEnumerable> elements, decimal expected)
		where TEnumerable : IEnumerable<decimal>
	{
		IElements<TEnumerable> iElements = elements;
		ObjectEqualityWithToleranceOptions<decimal, decimal> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateDecimal();
		ExpectationBuilder expectationBuilder = iElements.Subject.Get().ExpectationBuilder;
		return new ToleranceEqualityResult<TEnumerable, IThat<TEnumerable>, decimal, decimal>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new CollectionConstraint<decimal>(
					expectationBuilder,
					it, grammars,
					iElements.Quantifier,
					g => (g.HasAnyFlag(ExpectationGrammars.Nested, ExpectationGrammars.Plural),
							g.IsNegated()) switch
						{
							(true, false) => $"are equal to {Formatter.Format(expected)}{options}",
							(false, false) => $"is equal to {Formatter.Format(expected)}{options}",
							(true, true) => $"are not equal to {Formatter.Format(expected)}{options}",
							(false, true) => $"is not equal to {Formatter.Format(expected)}{options}",
						},
					a => options.AreConsideredEqual(a, expected),
					"were")),
			iElements.Subject,
			options);
	}

	/// <summary>
	///     …are equal to the <paramref name="expected" /> value.
	/// </summary>
	public static ToleranceEqualityResult<TEnumerable, IThat<TEnumerable>, decimal?, decimal>
		AreEqualTo<TEnumerable>(this Elements<decimal?, TEnumerable> elements, decimal? expected)
		where TEnumerable : IEnumerable<decimal?>
	{
		IElements<TEnumerable> iElements = elements;
		ObjectEqualityWithToleranceOptions<decimal?, decimal> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateNullableDecimal();
		ExpectationBuilder expectationBuilder = iElements.Subject.Get().ExpectationBuilder;
		return new ToleranceEqualityResult<TEnumerable, IThat<TEnumerable>, decimal?, decimal>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new CollectionConstraint<decimal?>(
					expectationBuilder,
					it, grammars,
					iElements.Quantifier,
					g => (g.HasAnyFlag(ExpectationGrammars.Nested, ExpectationGrammars.Plural),
							g.IsNegated()) switch
						{
							(true, false) => $"are equal to {Formatter.Format(expected)}{options}",
							(false, false) => $"is equal to {Formatter.Format(expected)}{options}",
							(true, true) => $"are not equal to {Formatter.Format(expected)}{options}",
							(false, true) => $"is not equal to {Formatter.Format(expected)}{options}",
						},
					a => options.AreConsideredEqual(a, expected),
					"were")),
			iElements.Subject,
			options);
	}

	/// <summary>
	///     …are equal to the <paramref name="expected" /> value.
	/// </summary>
	public static ToleranceEqualityResult<TEnumerable, IThat<TEnumerable>, DateTime, TimeSpan>
		AreEqualTo<TEnumerable>(this Elements<DateTime, TEnumerable> elements, DateTime expected)
		where TEnumerable : IEnumerable<DateTime>
	{
		IElements<TEnumerable> iElements = elements;
		ObjectEqualityWithToleranceOptions<DateTime, TimeSpan> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateDateTime();
		ExpectationBuilder expectationBuilder = iElements.Subject.Get().ExpectationBuilder;
		return new ToleranceEqualityResult<TEnumerable, IThat<TEnumerable>, DateTime, TimeSpan>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new CollectionConstraint<DateTime>(
					expectationBuilder,
					it, grammars,
					iElements.Quantifier,
					g => (g.HasAnyFlag(ExpectationGrammars.Nested, ExpectationGrammars.Plural),
							g.IsNegated()) switch
						{
							(true, false) => $"are equal to {Formatter.Format(expected)}{options}",
							(false, false) => $"is equal to {Formatter.Format(expected)}{options}",
							(true, true) => $"are not equal to {Formatter.Format(expected)}{options}",
							(false, true) => $"is not equal to {Formatter.Format(expected)}{options}",
						},
					a => options.AreConsideredEqual(a, expected),
					"were")),
			iElements.Subject,
			options);
	}

	/// <summary>
	///     …are equal to the <paramref name="expected" /> value.
	/// </summary>
	public static ToleranceEqualityResult<TEnumerable, IThat<TEnumerable>, DateTime?, TimeSpan>
		AreEqualTo<TEnumerable>(this Elements<DateTime?, TEnumerable> elements, DateTime? expected)
		where TEnumerable : IEnumerable<DateTime?>
	{
		IElements<TEnumerable> iElements = elements;
		ObjectEqualityWithToleranceOptions<DateTime?, TimeSpan> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateNullableDateTime();
		ExpectationBuilder expectationBuilder = iElements.Subject.Get().ExpectationBuilder;
		return new ToleranceEqualityResult<TEnumerable, IThat<TEnumerable>, DateTime?, TimeSpan>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new CollectionConstraint<DateTime?>(
					expectationBuilder,
					it, grammars,
					iElements.Quantifier,
					g => (g.HasAnyFlag(ExpectationGrammars.Nested, ExpectationGrammars.Plural),
							g.IsNegated()) switch
						{
							(true, false) => $"are equal to {Formatter.Format(expected)}{options}",
							(false, false) => $"is equal to {Formatter.Format(expected)}{options}",
							(true, true) => $"are not equal to {Formatter.Format(expected)}{options}",
							(false, true) => $"is not equal to {Formatter.Format(expected)}{options}",
						},
					a => options.AreConsideredEqual(a, expected),
					"were")),
			iElements.Subject,
			options);
	}

	/// <summary>
	///     …are equal to the <paramref name="expected" /> value.
	/// </summary>
	public static ObjectEqualityResult<TEnumerable, IThat<TEnumerable>, TItem>
		AreEqualTo<TItem, TEnumerable>(this Elements<TItem, TEnumerable> elements, TItem expected)
		where TEnumerable : IEnumerable<TItem>
	{
		IElements<TEnumerable> iElements = elements;
		ObjectEqualityOptions<TItem> options = new();
		ExpectationBuilder expectationBuilder = iElements.Subject.Get().ExpectationBuilder;
		return new ObjectEqualityResult<TEnumerable, IThat<TEnumerable>, TItem>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new CollectionConstraint<TItem>(
					expectationBuilder,
					it, grammars,
					iElements.Quantifier,
					g => (g.HasAnyFlag(ExpectationGrammars.Nested, ExpectationGrammars.Plural),
							g.IsNegated()) switch
						{
							(true, false) => $"are equal to {Formatter.Format(expected)}{options}",
							(false, false) => $"is equal to {Formatter.Format(expected)}{options}",
							(true, true) => $"are not equal to {Formatter.Format(expected)}{options}",
							(false, true) => $"is not equal to {Formatter.Format(expected)}{options}",
						},
					a => options.AreConsideredEqual(a, expected),
					"were")),
			iElements.Subject,
			options);
	}

	/// <summary>
	///     …are equal to the <paramref name="expected" /> value.
	/// </summary>
	public static StringEqualityResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>> AreEqualTo(
		this StringElements stringElements,
		string? expected)
	{
		IStringElements iStringElements = stringElements;
		StringEqualityOptions options = new();
		ExpectationBuilder expectationBuilder = iStringElements.Subject.Get().ExpectationBuilder;
		return new StringEqualityResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new CollectionConstraint<string?>(
					expectationBuilder,
					it, grammars,
					iStringElements.Quantifier,
					g => (g.HasAnyFlag(ExpectationGrammars.Nested, ExpectationGrammars.Plural),
							g.IsNegated()) switch
						{
							(true, false) => $"are equal to {Formatter.Format(expected)}{options}",
							(false, false) => $"is equal to {Formatter.Format(expected)}{options}",
							(true, true) => $"are not equal to {Formatter.Format(expected)}{options}",
							(false, true) => $"is not equal to {Formatter.Format(expected)}{options}",
						},
					a => options.AreConsideredEqual(a, expected),
					"were")),
			iStringElements.Subject,
			options);
	}
}

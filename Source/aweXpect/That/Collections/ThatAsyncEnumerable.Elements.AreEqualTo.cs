#if NET8_0_OR_GREATER
using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatAsyncEnumerable
{
	/// <summary>
	///     …are equal to the <paramref name="expected" /> value.
	/// </summary>
	public static ToleranceEqualityResult<IAsyncEnumerable<double>, IThat<IAsyncEnumerable<double>?>, double, double>
		AreEqualTo(this Elements<double> elements, double expected)
	{
		IElements<double> iElements = elements;
		ObjectEqualityWithToleranceOptions<double, double> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateDouble();
		return new ToleranceEqualityResult<IAsyncEnumerable<double>, IThat<IAsyncEnumerable<double>?>, double, double>(
			iElements.Subject.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new CollectionConstraint<double>(
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
	public static ToleranceEqualityResult<IAsyncEnumerable<double?>, IThat<IAsyncEnumerable<double?>?>, double?, double>
		AreEqualTo(this Elements<double?> elements, double? expected)
	{
		IElements<double?> iElements = elements;
		ObjectEqualityWithToleranceOptions<double?, double> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateNullableDouble();
		return new ToleranceEqualityResult<IAsyncEnumerable<double?>, IThat<IAsyncEnumerable<double?>?>, double?,
			double>(
			iElements.Subject.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new CollectionConstraint<double?>(
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
	public static ToleranceEqualityResult<IAsyncEnumerable<float>, IThat<IAsyncEnumerable<float>?>, float, float>
		AreEqualTo(this Elements<float> elements, float expected)
	{
		IElements<float> iElements = elements;
		ObjectEqualityWithToleranceOptions<float, float> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateFloat();
		return new ToleranceEqualityResult<IAsyncEnumerable<float>, IThat<IAsyncEnumerable<float>?>, float, float>(
			iElements.Subject.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new CollectionConstraint<float>(
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
	public static ToleranceEqualityResult<IAsyncEnumerable<float?>, IThat<IAsyncEnumerable<float?>?>, float?, float>
		AreEqualTo(this Elements<float?> elements, float? expected)
	{
		IElements<float?> iElements = elements;
		ObjectEqualityWithToleranceOptions<float?, float> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateNullableFloat();
		return new ToleranceEqualityResult<IAsyncEnumerable<float?>, IThat<IAsyncEnumerable<float?>?>, float?, float>(
			iElements.Subject.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new CollectionConstraint<float?>(
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
	public static ToleranceEqualityResult<IAsyncEnumerable<decimal>, IThat<IAsyncEnumerable<decimal>?>, decimal,
			decimal>
		AreEqualTo(this Elements<decimal> elements, decimal expected)
	{
		IElements<decimal> iElements = elements;
		ObjectEqualityWithToleranceOptions<decimal, decimal> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateDecimal();
		return new ToleranceEqualityResult<IAsyncEnumerable<decimal>, IThat<IAsyncEnumerable<decimal>?>, decimal,
			decimal>(
			iElements.Subject.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new CollectionConstraint<decimal>(
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
	public static ToleranceEqualityResult<IAsyncEnumerable<decimal?>, IThat<IAsyncEnumerable<decimal?>?>, decimal?,
			decimal>
		AreEqualTo(this Elements<decimal?> elements, decimal? expected)
	{
		IElements<decimal?> iElements = elements;
		ObjectEqualityWithToleranceOptions<decimal?, decimal> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateNullableDecimal();
		return new ToleranceEqualityResult<IAsyncEnumerable<decimal?>, IThat<IAsyncEnumerable<decimal?>?>, decimal?,
			decimal>(
			iElements.Subject.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new CollectionConstraint<decimal?>(
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
	public static ToleranceEqualityResult<IAsyncEnumerable<DateTime>, IThat<IAsyncEnumerable<DateTime>?>, DateTime,
			TimeSpan>
		AreEqualTo(this Elements<DateTime> elements, DateTime expected)
	{
		IElements<DateTime> iElements = elements;
		ObjectEqualityWithToleranceOptions<DateTime, TimeSpan> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateDateTime();
		return new ToleranceEqualityResult<IAsyncEnumerable<DateTime>, IThat<IAsyncEnumerable<DateTime>?>, DateTime,
			TimeSpan>(
			iElements.Subject.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new CollectionConstraint<DateTime>(
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
	public static ToleranceEqualityResult<IAsyncEnumerable<DateTime?>, IThat<IAsyncEnumerable<DateTime?>?>, DateTime?,
			TimeSpan>
		AreEqualTo(this Elements<DateTime?> elements, DateTime? expected)
	{
		IElements<DateTime?> iElements = elements;
		ObjectEqualityWithToleranceOptions<DateTime?, TimeSpan> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateNullableDateTime();
		return new ToleranceEqualityResult<IAsyncEnumerable<DateTime?>, IThat<IAsyncEnumerable<DateTime?>?>, DateTime?,
			TimeSpan>(
			iElements.Subject.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new CollectionConstraint<DateTime?>(
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
	public static ObjectEqualityResult<IAsyncEnumerable<TItem>?, IThat<IAsyncEnumerable<TItem>?>, TItem>
		AreEqualTo<TItem>(this Elements<TItem> elements, TItem expected)
	{
		IElements<TItem> iElements = elements;
		ObjectEqualityOptions<TItem> options = new();
		return new ObjectEqualityResult<IAsyncEnumerable<TItem>?, IThat<IAsyncEnumerable<TItem>?>, TItem>(
			iElements.Subject.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new CollectionConstraint<TItem>(
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
	public static StringEqualityResult<IAsyncEnumerable<string?>, IThat<IAsyncEnumerable<string?>?>> AreEqualTo(
		this Elements elements,
		string? expected)
	{
		IElements iElements = elements;
		StringEqualityOptions options = new();
		return new StringEqualityResult<IAsyncEnumerable<string?>, IThat<IAsyncEnumerable<string?>?>>(
			iElements.Subject.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new CollectionConstraint<string?>(
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
}
#endif

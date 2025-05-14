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
	public static ToleranceEqualityResult<IEnumerable<double>, IThat<IEnumerable<double>?>, double, double>
		AreEqualTo(this Elements<double> elements, double expected)
	{
		IElements<double> iElements = elements;
		ObjectEqualityWithToleranceOptions<double, double> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		return new ToleranceEqualityResult<IEnumerable<double>, IThat<IEnumerable<double>?>, double, double>(
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
	public static ToleranceEqualityResult<IEnumerable<double?>, IThat<IEnumerable<double?>?>, double?, double>
		AreEqualTo(this Elements<double?> elements, double? expected)
	{
		IElements<double?> iElements = elements;
		ObjectEqualityWithToleranceOptions<double?, double> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		return new ToleranceEqualityResult<IEnumerable<double?>, IThat<IEnumerable<double?>?>, double?, double>(
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
	public static ToleranceEqualityResult<IEnumerable<float>, IThat<IEnumerable<float>?>, float, float>
		AreEqualTo(this Elements<float> elements, float expected)
	{
		IElements<float> iElements = elements;
		ObjectEqualityWithToleranceOptions<float, float> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		return new ToleranceEqualityResult<IEnumerable<float>, IThat<IEnumerable<float>?>, float, float>(
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
	public static ToleranceEqualityResult<IEnumerable<float?>, IThat<IEnumerable<float?>?>, float?, float>
		AreEqualTo(this Elements<float?> elements, float? expected)
	{
		IElements<float?> iElements = elements;
		ObjectEqualityWithToleranceOptions<float?, float> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		return new ToleranceEqualityResult<IEnumerable<float?>, IThat<IEnumerable<float?>?>, float?, float>(
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
	public static ToleranceEqualityResult<IEnumerable<decimal>, IThat<IEnumerable<decimal>?>, decimal, decimal>
		AreEqualTo(this Elements<decimal> elements, decimal expected)
	{
		IElements<decimal> iElements = elements;
		ObjectEqualityWithToleranceOptions<decimal, decimal> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		return new ToleranceEqualityResult<IEnumerable<decimal>, IThat<IEnumerable<decimal>?>, decimal, decimal>(
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
	public static ToleranceEqualityResult<IEnumerable<decimal?>, IThat<IEnumerable<decimal?>?>, decimal?, decimal>
		AreEqualTo(this Elements<decimal?> elements, decimal? expected)
	{
		IElements<decimal?> iElements = elements;
		ObjectEqualityWithToleranceOptions<decimal?, decimal> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		return new ToleranceEqualityResult<IEnumerable<decimal?>, IThat<IEnumerable<decimal?>?>, decimal?, decimal>(
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
	public static ToleranceEqualityResult<IEnumerable<DateTime>, IThat<IEnumerable<DateTime>?>, DateTime, TimeSpan>
		AreEqualTo(this Elements<DateTime> elements, DateTime expected)
	{
		IElements<DateTime> iElements = elements;
		ObjectEqualityWithToleranceOptions<DateTime, TimeSpan> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" within {Formatter.Format(t)}");
		return new ToleranceEqualityResult<IEnumerable<DateTime>, IThat<IEnumerable<DateTime>?>, DateTime, TimeSpan>(
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
	public static ToleranceEqualityResult<IEnumerable<DateTime?>, IThat<IEnumerable<DateTime?>?>, DateTime?, TimeSpan>
		AreEqualTo(this Elements<DateTime?> elements, DateTime? expected)
	{
		IElements<DateTime?> iElements = elements;
		ObjectEqualityWithToleranceOptions<DateTime?, TimeSpan> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" within {Formatter.Format(t)}");
		return new ToleranceEqualityResult<IEnumerable<DateTime?>, IThat<IEnumerable<DateTime?>?>, DateTime?, TimeSpan>(
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
	public static ObjectEqualityResult<IEnumerable<TItem>?, IThat<IEnumerable<TItem>?>, TItem>
		AreEqualTo<TItem>(this Elements<TItem> elements, TItem expected)
	{
		IElements<TItem> iElements = elements;
		ObjectEqualityOptions<TItem> options = new();
		return new ObjectEqualityResult<IEnumerable<TItem>?, IThat<IEnumerable<TItem>?>, TItem>(
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
	public static StringEqualityResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>> AreEqualTo(
		this Elements elements,
		string? expected)
	{
		IElements iElements = elements;
		StringEqualityOptions options = new();
		return new StringEqualityResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>(
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

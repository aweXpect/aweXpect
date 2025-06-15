﻿using System;
using System.Collections;
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
		ObjectEqualityWithToleranceOptions<double, double> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateDouble();
		ExpectationBuilder expectationBuilder = iElements.Subject.Get().ExpectationBuilder;
		return new ToleranceEqualityResult<IEnumerable<double>, IThat<IEnumerable<double>?>, double, double>(
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
	public static ToleranceEqualityResult<IEnumerable<double?>, IThat<IEnumerable<double?>?>, double?, double>
		AreEqualTo(this Elements<double?> elements, double? expected)
	{
		IElements<double?> iElements = elements;
		ObjectEqualityWithToleranceOptions<double?, double> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateNullableDouble();
		ExpectationBuilder expectationBuilder = iElements.Subject.Get().ExpectationBuilder;
		return new ToleranceEqualityResult<IEnumerable<double?>, IThat<IEnumerable<double?>?>, double?, double>(
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
	public static ToleranceEqualityResult<IEnumerable<float>, IThat<IEnumerable<float>?>, float, float>
		AreEqualTo(this Elements<float> elements, float expected)
	{
		IElements<float> iElements = elements;
		ObjectEqualityWithToleranceOptions<float, float> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateFloat();
		ExpectationBuilder expectationBuilder = iElements.Subject.Get().ExpectationBuilder;
		return new ToleranceEqualityResult<IEnumerable<float>, IThat<IEnumerable<float>?>, float, float>(
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
	public static ToleranceEqualityResult<IEnumerable<float?>, IThat<IEnumerable<float?>?>, float?, float>
		AreEqualTo(this Elements<float?> elements, float? expected)
	{
		IElements<float?> iElements = elements;
		ObjectEqualityWithToleranceOptions<float?, float> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateNullableFloat();
		ExpectationBuilder expectationBuilder = iElements.Subject.Get().ExpectationBuilder;
		return new ToleranceEqualityResult<IEnumerable<float?>, IThat<IEnumerable<float?>?>, float?, float>(
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
	public static ToleranceEqualityResult<IEnumerable<decimal>, IThat<IEnumerable<decimal>?>, decimal, decimal>
		AreEqualTo(this Elements<decimal> elements, decimal expected)
	{
		IElements<decimal> iElements = elements;
		ObjectEqualityWithToleranceOptions<decimal, decimal> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateDecimal();
		ExpectationBuilder expectationBuilder = iElements.Subject.Get().ExpectationBuilder;
		return new ToleranceEqualityResult<IEnumerable<decimal>, IThat<IEnumerable<decimal>?>, decimal, decimal>(
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
	public static ToleranceEqualityResult<IEnumerable<decimal?>, IThat<IEnumerable<decimal?>?>, decimal?, decimal>
		AreEqualTo(this Elements<decimal?> elements, decimal? expected)
	{
		IElements<decimal?> iElements = elements;
		ObjectEqualityWithToleranceOptions<decimal?, decimal> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateNullableDecimal();
		ExpectationBuilder expectationBuilder = iElements.Subject.Get().ExpectationBuilder;
		return new ToleranceEqualityResult<IEnumerable<decimal?>, IThat<IEnumerable<decimal?>?>, decimal?, decimal>(
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
	public static ToleranceEqualityResult<IEnumerable<DateTime>, IThat<IEnumerable<DateTime>?>, DateTime, TimeSpan>
		AreEqualTo(this Elements<DateTime> elements, DateTime expected)
	{
		IElements<DateTime> iElements = elements;
		ObjectEqualityWithToleranceOptions<DateTime, TimeSpan> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateDateTime();
		ExpectationBuilder expectationBuilder = iElements.Subject.Get().ExpectationBuilder;
		return new ToleranceEqualityResult<IEnumerable<DateTime>, IThat<IEnumerable<DateTime>?>, DateTime, TimeSpan>(
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
	public static ToleranceEqualityResult<IEnumerable<DateTime?>, IThat<IEnumerable<DateTime?>?>, DateTime?, TimeSpan>
		AreEqualTo(this Elements<DateTime?> elements, DateTime? expected)
	{
		IElements<DateTime?> iElements = elements;
		ObjectEqualityWithToleranceOptions<DateTime?, TimeSpan> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateNullableDateTime();
		ExpectationBuilder expectationBuilder = iElements.Subject.Get().ExpectationBuilder;
		return new ToleranceEqualityResult<IEnumerable<DateTime?>, IThat<IEnumerable<DateTime?>?>, DateTime?, TimeSpan>(
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
	public static ObjectEqualityResult<IEnumerable<TItem>?, IThat<IEnumerable<TItem>?>, TItem>
		AreEqualTo<TItem>(this Elements<TItem> elements, TItem expected)
	{
		IElements<TItem> iElements = elements;
		ObjectEqualityOptions<TItem> options = new();
		ExpectationBuilder expectationBuilder = iElements.Subject.Get().ExpectationBuilder;
		return new ObjectEqualityResult<IEnumerable<TItem>?, IThat<IEnumerable<TItem>?>, TItem>(
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
	public static ObjectEqualityResult<TEnumerable, IThat<TEnumerable>, object?>
		AreEqualTo<TEnumerable>(this ElementsForEnumerable<TEnumerable> elements, object? expected)
		where TEnumerable : IEnumerable?
	{
		IElementsForEnumerable<TEnumerable> iElements = elements;
		ObjectEqualityOptions<object?> options = new();
		ExpectationBuilder expectationBuilder = iElements.Subject.Get().ExpectationBuilder;
		return new ObjectEqualityResult<TEnumerable, IThat<TEnumerable>, object?>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new CollectionForEnumerableConstraint<TEnumerable>(
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
	public static ObjectEqualityResult<TEnumerable, IThat<TEnumerable>, object?>
		AreEqualTo<TEnumerable, TItem>(this ElementsForStructEnumerable<TEnumerable, TItem> elements, object? expected)
		where TEnumerable : struct, IEnumerable<TItem>
	{
		IElementsForStructEnumerable<TEnumerable, TItem> iElements = elements;
		ObjectEqualityOptions<object?> options = new();
		ExpectationBuilder expectationBuilder = iElements.Subject.Get().ExpectationBuilder;
		return new ObjectEqualityResult<TEnumerable, IThat<TEnumerable>, object?>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new CollectionForEnumerableConstraint<TEnumerable>(
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
		this Elements elements,
		string? expected)
	{
		IElements iElements = elements;
		StringEqualityOptions options = new();
		ExpectationBuilder expectationBuilder = iElements.Subject.Get().ExpectationBuilder;
		return new StringEqualityResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new CollectionConstraint<string?>(
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
	public static StringEqualityResult<TEnumerable, IThat<TEnumerable>> AreEqualTo<TEnumerable>(
		this ElementsForStructEnumerable<TEnumerable> elements,
		string? expected)
		where TEnumerable : struct, IEnumerable<string?>
	{
		IElementsForStructEnumerable<TEnumerable> iElements = elements;
		StringEqualityOptions options = new();
		ExpectationBuilder expectationBuilder = iElements.Subject.Get().ExpectationBuilder;
		return new StringEqualityResult<TEnumerable, IThat<TEnumerable>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new CollectionForEnumerableConstraint<TEnumerable>(
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
					a => options.AreConsideredEqual((string?)a, expected),
					"were")),
			iElements.Subject,
			options);
	}
}

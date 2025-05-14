using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that the collection matches the <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectCollectionMatchResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>
		IsEqualTo<TItem>(
			this IThat<IEnumerable<TItem>?> source,
			IEnumerable<TItem> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<TItem> options = new();
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<TItem, TItem>(it, grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					expected,
					options,
					matchOptions)),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection matches the <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectCollectionMatchWithToleranceResult<IEnumerable<double>, IThat<IEnumerable<double>?>,
			double, double>
		IsEqualTo(
			this IThat<IEnumerable<double>?> source,
			IEnumerable<double> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<double, double> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<double>, IThat<IEnumerable<double>?>,
			double, double>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<double, double>(it, grammars,
					doNotPopulateThisValue,
					expected,
					options,
					matchOptions)),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection matches the <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectCollectionMatchWithToleranceResult<IEnumerable<double?>, IThat<IEnumerable<double?>?>,
			double?, double>
		IsEqualTo(
			this IThat<IEnumerable<double?>?> source,
			IEnumerable<double?> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<double?, double> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<double?>, IThat<IEnumerable<double?>?>,
			double?, double>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<double?, double?>(it, grammars,
					doNotPopulateThisValue,
					expected,
					options,
					matchOptions)),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection matches the <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectCollectionMatchWithToleranceResult<IEnumerable<decimal>, IThat<IEnumerable<decimal>?>,
			decimal, decimal>
		IsEqualTo(
			this IThat<IEnumerable<decimal>?> source,
			IEnumerable<decimal> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<decimal, decimal> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<decimal>, IThat<IEnumerable<decimal>?>,
			decimal, decimal>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<decimal, decimal>(it, grammars,
					doNotPopulateThisValue,
					expected,
					options,
					matchOptions)),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection matches the <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectCollectionMatchWithToleranceResult<IEnumerable<decimal?>, IThat<IEnumerable<decimal?>?>,
			decimal?, decimal>
		IsEqualTo(
			this IThat<IEnumerable<decimal?>?> source,
			IEnumerable<decimal?> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<decimal?, decimal> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<decimal?>, IThat<IEnumerable<decimal?>?>,
			decimal?, decimal>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<decimal?, decimal?>(it, grammars,
					doNotPopulateThisValue,
					expected,
					options,
					matchOptions)),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection matches the <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectCollectionMatchWithToleranceResult<IEnumerable<float>, IThat<IEnumerable<float>?>,
			float, float>
		IsEqualTo(
			this IThat<IEnumerable<float>?> source,
			IEnumerable<float> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<float, float> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<float>, IThat<IEnumerable<float>?>,
			float, float>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<float, float>(it, grammars,
					doNotPopulateThisValue,
					expected,
					options,
					matchOptions)),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection matches the <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectCollectionMatchWithToleranceResult<IEnumerable<float?>, IThat<IEnumerable<float?>?>,
			float?, float>
		IsEqualTo(
			this IThat<IEnumerable<float?>?> source,
			IEnumerable<float?> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<float?, float> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<float?>, IThat<IEnumerable<float?>?>,
			float?, float>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<float?, float?>(it, grammars,
					doNotPopulateThisValue,
					expected,
					options,
					matchOptions)),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection matches the <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectCollectionMatchWithToleranceResult<IEnumerable<DateTime>, IThat<IEnumerable<DateTime>?>,
			DateTime, TimeSpan>
		IsEqualTo(
			this IThat<IEnumerable<DateTime>?> source,
			IEnumerable<DateTime> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<DateTime, TimeSpan> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" within {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<DateTime>, IThat<IEnumerable<DateTime>?>,
			DateTime, TimeSpan>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<DateTime, DateTime>(it, grammars,
					doNotPopulateThisValue,
					expected,
					options,
					matchOptions)),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection matches the <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectCollectionMatchWithToleranceResult<IEnumerable<DateTime?>, IThat<IEnumerable<DateTime?>?>,
			DateTime?, TimeSpan>
		IsEqualTo(
			this IThat<IEnumerable<DateTime?>?> source,
			IEnumerable<DateTime?> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<DateTime?, TimeSpan> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" within {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<DateTime?>, IThat<IEnumerable<DateTime?>?>,
			DateTime?, TimeSpan>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<DateTime?, DateTime?>(it, grammars,
					doNotPopulateThisValue,
					expected,
					options,
					matchOptions)),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection matches the <paramref name="expected" /> collection.
	/// </summary>
	public static StringCollectionMatchResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>
		IsEqualTo(this IThat<IEnumerable<string?>?> source,
			IEnumerable<string?> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new();
		return new StringCollectionMatchResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<string?, string?>(it, grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					expected,
					options,
					matchOptions)),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection does not match the <paramref name="unexpected" /> collection.
	/// </summary>
	public static ObjectCollectionMatchResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>
		IsNotEqualTo<TItem>(
			this IThat<IEnumerable<TItem>?> source,
			IEnumerable<TItem> unexpected,
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<TItem> options = new();
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<TItem, TItem>(it, grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					unexpected,
					options,
					matchOptions).Invert()),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection does not match the <paramref name="unexpected" /> collection.
	/// </summary>
	public static ObjectCollectionMatchWithToleranceResult<IEnumerable<double>, IThat<IEnumerable<double>?>,
			double, double>
		IsNotEqualTo(
			this IThat<IEnumerable<double>?> source,
			IEnumerable<double> unexpected,
			[CallerArgumentExpression("unexpected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<double, double> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<double>, IThat<IEnumerable<double>?>,
			double, double>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<double, double>(it, grammars,
					doNotPopulateThisValue,
					unexpected,
					options,
					matchOptions).Invert()),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection does not match the <paramref name="unexpected" /> collection.
	/// </summary>
	public static ObjectCollectionMatchWithToleranceResult<IEnumerable<double?>, IThat<IEnumerable<double?>?>,
			double?, double>
		IsNotEqualTo(
			this IThat<IEnumerable<double?>?> source,
			IEnumerable<double?> unexpected,
			[CallerArgumentExpression("unexpected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<double?, double> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<double?>, IThat<IEnumerable<double?>?>,
			double?, double>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<double?, double?>(it, grammars,
					doNotPopulateThisValue,
					unexpected,
					options,
					matchOptions).Invert()),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection does not match the <paramref name="unexpected" /> collection.
	/// </summary>
	public static ObjectCollectionMatchWithToleranceResult<IEnumerable<decimal>, IThat<IEnumerable<decimal>?>,
			decimal, decimal>
		IsNotEqualTo(
			this IThat<IEnumerable<decimal>?> source,
			IEnumerable<decimal> unexpected,
			[CallerArgumentExpression("unexpected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<decimal, decimal> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<decimal>, IThat<IEnumerable<decimal>?>,
			decimal, decimal>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<decimal, decimal>(it, grammars,
					doNotPopulateThisValue,
					unexpected,
					options,
					matchOptions).Invert()),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection does not match the <paramref name="unexpected" /> collection.
	/// </summary>
	public static ObjectCollectionMatchWithToleranceResult<IEnumerable<decimal?>, IThat<IEnumerable<decimal?>?>,
			decimal?, decimal>
		IsNotEqualTo(
			this IThat<IEnumerable<decimal?>?> source,
			IEnumerable<decimal?> unexpected,
			[CallerArgumentExpression("unexpected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<decimal?, decimal> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<decimal?>, IThat<IEnumerable<decimal?>?>,
			decimal?, decimal>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<decimal?, decimal?>(it, grammars,
					doNotPopulateThisValue,
					unexpected,
					options,
					matchOptions).Invert()),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection does not match the <paramref name="unexpected" /> collection.
	/// </summary>
	public static ObjectCollectionMatchWithToleranceResult<IEnumerable<float>, IThat<IEnumerable<float>?>,
			float, float>
		IsNotEqualTo(
			this IThat<IEnumerable<float>?> source,
			IEnumerable<float> unexpected,
			[CallerArgumentExpression("unexpected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<float, float> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<float>, IThat<IEnumerable<float>?>,
			float, float>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<float, float>(it, grammars,
					doNotPopulateThisValue,
					unexpected,
					options,
					matchOptions).Invert()),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection does not match the <paramref name="unexpected" /> collection.
	/// </summary>
	public static ObjectCollectionMatchWithToleranceResult<IEnumerable<float?>, IThat<IEnumerable<float?>?>,
			float?, float>
		IsNotEqualTo(
			this IThat<IEnumerable<float?>?> source,
			IEnumerable<float?> unexpected,
			[CallerArgumentExpression("unexpected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<float?, float> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<float?>, IThat<IEnumerable<float?>?>,
			float?, float>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<float?, float?>(it, grammars,
					doNotPopulateThisValue,
					unexpected,
					options,
					matchOptions).Invert()),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection does not match the <paramref name="unexpected" /> collection.
	/// </summary>
	public static ObjectCollectionMatchWithToleranceResult<IEnumerable<DateTime>, IThat<IEnumerable<DateTime>?>,
			DateTime, TimeSpan>
		IsNotEqualTo(
			this IThat<IEnumerable<DateTime>?> source,
			IEnumerable<DateTime> unexpected,
			[CallerArgumentExpression("unexpected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<DateTime, TimeSpan> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" within {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<DateTime>, IThat<IEnumerable<DateTime>?>,
			DateTime, TimeSpan>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<DateTime, DateTime>(it, grammars,
					doNotPopulateThisValue,
					unexpected,
					options,
					matchOptions).Invert()),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection does not match the <paramref name="unexpected" /> collection.
	/// </summary>
	public static ObjectCollectionMatchWithToleranceResult<IEnumerable<DateTime?>, IThat<IEnumerable<DateTime?>?>,
			DateTime?, TimeSpan>
		IsNotEqualTo(
			this IThat<IEnumerable<DateTime?>?> source,
			IEnumerable<DateTime?> unexpected,
			[CallerArgumentExpression("unexpected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<DateTime?, TimeSpan> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" within {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<DateTime?>, IThat<IEnumerable<DateTime?>?>,
			DateTime?, TimeSpan>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<DateTime?, DateTime?>(it, grammars,
					doNotPopulateThisValue,
					unexpected,
					options,
					matchOptions).Invert()),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection does not match the <paramref name="unexpected" /> collection.
	/// </summary>
	public static StringCollectionMatchResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>
		IsNotEqualTo(this IThat<IEnumerable<string?>?> source,
			IEnumerable<string?> unexpected,
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new();
		return new StringCollectionMatchResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<string?, string?>(it, grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					unexpected,
					options,
					matchOptions).Invert()),
			source,
			options,
			matchOptions);
	}
}

#if NET8_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatAsyncEnumerable
{
	/// <summary>
	///     Verifies that the collection matches the <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectCollectionMatchResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>
		IsEqualTo<TItem>(
			this IThat<IAsyncEnumerable<TItem>?> source,
			IEnumerable<TItem> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<TItem> options = new();
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsEqualToConstraint<TItem, TItem>(it, grammars,
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
	public static ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<double>, IThat<IAsyncEnumerable<double>?>,
			double, double>
		IsEqualTo(
			this IThat<IAsyncEnumerable<double>?> source,
			IEnumerable<double> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<double, double> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<double>, IThat<IAsyncEnumerable<double>?>,
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
	public static ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<double?>, IThat<IAsyncEnumerable<double?>?>,
			double?, double>
		IsEqualTo(
			this IThat<IAsyncEnumerable<double?>?> source,
			IEnumerable<double?> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<double?, double> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<double?>, IThat<IAsyncEnumerable<double?>?>,
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
	public static ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<decimal>, IThat<IAsyncEnumerable<decimal>?>,
			decimal, decimal>
		IsEqualTo(
			this IThat<IAsyncEnumerable<decimal>?> source,
			IEnumerable<decimal> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<decimal, decimal> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<decimal>, IThat<IAsyncEnumerable<decimal>?>,
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
	public static ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<decimal?>, IThat<IAsyncEnumerable<decimal?>?>,
			decimal?, decimal>
		IsEqualTo(
			this IThat<IAsyncEnumerable<decimal?>?> source,
			IEnumerable<decimal?> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<decimal?, decimal> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<decimal?>, IThat<IAsyncEnumerable<decimal?>?>,
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
	public static ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<float>, IThat<IAsyncEnumerable<float>?>,
			float, float>
		IsEqualTo(
			this IThat<IAsyncEnumerable<float>?> source,
			IEnumerable<float> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<float, float> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<float>, IThat<IAsyncEnumerable<float>?>,
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
	public static ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<float?>, IThat<IAsyncEnumerable<float?>?>,
			float?, float>
		IsEqualTo(
			this IThat<IAsyncEnumerable<float?>?> source,
			IEnumerable<float?> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<float?, float> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<float?>, IThat<IAsyncEnumerable<float?>?>,
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
	public static ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<DateTime>, IThat<IAsyncEnumerable<DateTime>?>,
			DateTime, TimeSpan>
		IsEqualTo(
			this IThat<IAsyncEnumerable<DateTime>?> source,
			IEnumerable<DateTime> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<DateTime, TimeSpan> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" within {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<DateTime>, IThat<IAsyncEnumerable<DateTime>?>,
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
	public static ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<DateTime?>, IThat<IAsyncEnumerable<DateTime?>?>,
			DateTime?, TimeSpan>
		IsEqualTo(
			this IThat<IAsyncEnumerable<DateTime?>?> source,
			IEnumerable<DateTime?> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<DateTime?, TimeSpan> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" within {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<DateTime?>, IThat<IAsyncEnumerable<DateTime?>?>,
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
	public static StringCollectionMatchResult<IAsyncEnumerable<string?>, IThat<IAsyncEnumerable<string?>?>>
		IsEqualTo(
			this IThat<IAsyncEnumerable<string?>?> source,
			IEnumerable<string?> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new();
		return new StringCollectionMatchResult<IAsyncEnumerable<string?>, IThat<IAsyncEnumerable<string?>?>>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsEqualToConstraint<string?, string?>(it, grammars,
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
	public static ObjectCollectionMatchResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>
		IsNotEqualTo<TItem>(
			this IThat<IAsyncEnumerable<TItem>?> source,
			IEnumerable<TItem> unexpected,
			[CallerArgumentExpression("unexpected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<TItem> options = new();
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsEqualToConstraint<TItem, TItem>(it, grammars,
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
	public static ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<double>, IThat<IAsyncEnumerable<double>?>,
			double, double>
		IsNotEqualTo(
			this IThat<IAsyncEnumerable<double>?> source,
			IEnumerable<double> unexpected,
			[CallerArgumentExpression("unexpected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<double, double> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<double>, IThat<IAsyncEnumerable<double>?>,
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
	public static ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<double?>, IThat<IAsyncEnumerable<double?>?>,
			double?, double>
		IsNotEqualTo(
			this IThat<IAsyncEnumerable<double?>?> source,
			IEnumerable<double?> unexpected,
			[CallerArgumentExpression("unexpected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<double?, double> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<double?>, IThat<IAsyncEnumerable<double?>?>,
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
	public static ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<decimal>, IThat<IAsyncEnumerable<decimal>?>,
			decimal, decimal>
		IsNotEqualTo(
			this IThat<IAsyncEnumerable<decimal>?> source,
			IEnumerable<decimal> unexpected,
			[CallerArgumentExpression("unexpected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<decimal, decimal> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<decimal>, IThat<IAsyncEnumerable<decimal>?>,
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
	public static ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<decimal?>, IThat<IAsyncEnumerable<decimal?>?>,
			decimal?, decimal>
		IsNotEqualTo(
			this IThat<IAsyncEnumerable<decimal?>?> source,
			IEnumerable<decimal?> unexpected,
			[CallerArgumentExpression("unexpected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<decimal?, decimal> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<decimal?>, IThat<IAsyncEnumerable<decimal?>?>,
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
	public static ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<float>, IThat<IAsyncEnumerable<float>?>,
			float, float>
		IsNotEqualTo(
			this IThat<IAsyncEnumerable<float>?> source,
			IEnumerable<float> unexpected,
			[CallerArgumentExpression("unexpected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<float, float> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<float>, IThat<IAsyncEnumerable<float>?>,
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
	public static ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<float?>, IThat<IAsyncEnumerable<float?>?>,
			float?, float>
		IsNotEqualTo(
			this IThat<IAsyncEnumerable<float?>?> source,
			IEnumerable<float?> unexpected,
			[CallerArgumentExpression("unexpected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<float?, float> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" ± {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<float?>, IThat<IAsyncEnumerable<float?>?>,
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
	public static ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<DateTime>, IThat<IAsyncEnumerable<DateTime>?>,
			DateTime, TimeSpan>
		IsNotEqualTo(
			this IThat<IAsyncEnumerable<DateTime>?> source,
			IEnumerable<DateTime> unexpected,
			[CallerArgumentExpression("unexpected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<DateTime, TimeSpan> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" within {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<DateTime>, IThat<IAsyncEnumerable<DateTime>?>,
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
	public static ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<DateTime?>, IThat<IAsyncEnumerable<DateTime?>?>,
			DateTime?, TimeSpan>
		IsNotEqualTo(
			this IThat<IAsyncEnumerable<DateTime?>?> source,
			IEnumerable<DateTime?> unexpected,
			[CallerArgumentExpression("unexpected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<DateTime?, TimeSpan> options = new(
			(a, e, t) => a.IsConsideredEqualTo(e, t),
			t => $" within {Formatter.Format(t)}");
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchWithToleranceResult<IAsyncEnumerable<DateTime?>, IThat<IAsyncEnumerable<DateTime?>?>,
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
	public static StringCollectionMatchResult<IAsyncEnumerable<string?>, IThat<IAsyncEnumerable<string?>?>>
		IsNotEqualTo(
			this IThat<IAsyncEnumerable<string?>?> source,
			IEnumerable<string?> unexpected,
			[CallerArgumentExpression("unexpected")] string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new();
		return new StringCollectionMatchResult<IAsyncEnumerable<string?>, IThat<IAsyncEnumerable<string?>?>>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsEqualToConstraint<string?, string?>(it, grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					unexpected,
					options,
					matchOptions).Invert()),
			source,
			options,
			matchOptions);
	}
}
#endif

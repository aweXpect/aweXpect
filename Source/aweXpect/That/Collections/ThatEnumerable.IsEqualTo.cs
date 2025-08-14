using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;
#if NET8_0_OR_GREATER
using System.Collections.Immutable;
#endif

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
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionMatchResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<TItem, TItem>(expectationBuilder, it, grammars,
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
		ObjectEqualityWithToleranceOptions<double, double> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateDouble();
		CollectionMatchOptions matchOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<double>, IThat<IEnumerable<double>?>,
			double, double>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<double, double>(expectationBuilder, it, grammars,
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
		ObjectEqualityWithToleranceOptions<double?, double> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateNullableDouble();
		CollectionMatchOptions matchOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<double?>, IThat<IEnumerable<double?>?>,
			double?, double>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<double?, double?>(expectationBuilder, it, grammars,
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
		ObjectEqualityWithToleranceOptions<decimal, decimal> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateDecimal();
		CollectionMatchOptions matchOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<decimal>, IThat<IEnumerable<decimal>?>,
			decimal, decimal>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<decimal, decimal>(expectationBuilder, it, grammars,
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
		ObjectEqualityWithToleranceOptions<decimal?, decimal> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateNullableDecimal();
		CollectionMatchOptions matchOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<decimal?>, IThat<IEnumerable<decimal?>?>,
			decimal?, decimal>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<decimal?, decimal?>(expectationBuilder, it, grammars,
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
		ObjectEqualityWithToleranceOptions<float, float> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateFloat();
		CollectionMatchOptions matchOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<float>, IThat<IEnumerable<float>?>,
			float, float>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<float, float>(expectationBuilder, it, grammars,
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
		ObjectEqualityWithToleranceOptions<float?, float> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateNullableFloat();
		CollectionMatchOptions matchOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<float?>, IThat<IEnumerable<float?>?>,
			float?, float>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<float?, float?>(expectationBuilder, it, grammars,
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
		ObjectEqualityWithToleranceOptions<DateTime, TimeSpan> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateDateTime();
		CollectionMatchOptions matchOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<DateTime>, IThat<IEnumerable<DateTime>?>,
			DateTime, TimeSpan>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<DateTime, DateTime>(expectationBuilder, it, grammars,
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
		ObjectEqualityWithToleranceOptions<DateTime?, TimeSpan> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateNullableDateTime();
		CollectionMatchOptions matchOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<DateTime?>, IThat<IEnumerable<DateTime?>?>,
			DateTime?, TimeSpan>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<DateTime?, DateTime?>(expectationBuilder, it, grammars,
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
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new StringCollectionMatchResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<string?, string?>(expectationBuilder, it, grammars,
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
	public static ObjectCollectionMatchResult<IEnumerable, IThat<IEnumerable>, TItem>
		IsEqualTo<TItem>(
			this IThat<IEnumerable> source,
			IEnumerable<TItem> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<TItem> options = new();
		CollectionMatchOptions matchOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionMatchResult<IEnumerable, IThat<IEnumerable>, TItem>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToForEnumerableConstraint<IEnumerable, TItem, TItem>(expectationBuilder, it, grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					expected,
					options,
					matchOptions)),
			source,
			options,
			matchOptions);
	}

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that the collection matches the <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectCollectionBeContainedInResult<ImmutableArray<TItem>, IThat<ImmutableArray<TItem>>, TItem>
		IsEqualTo<TItem>(
			this IThat<ImmutableArray<TItem>> source,
			IEnumerable<TItem> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<TItem> options = new();
		CollectionMatchOptions matchOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionBeContainedInResult<ImmutableArray<TItem>, IThat<ImmutableArray<TItem>>, TItem>(
			expectationBuilder.AddConstraint((it, grammars) =>
				new IsEqualToForEnumerableConstraint<ImmutableArray<TItem>, TItem, TItem>(expectationBuilder, it,
					grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					expected,
					options,
					matchOptions)),
			source,
			options,
			matchOptions);
	}
#endif

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that the collection matches the <paramref name="expected" /> collection.
	/// </summary>
	public static StringCollectionBeContainedInResult<ImmutableArray<string?>, IThat<ImmutableArray<string?>>>
		IsEqualTo(this IThat<ImmutableArray<string?>> source,
			IEnumerable<string?> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new StringCollectionBeContainedInResult<ImmutableArray<string?>, IThat<ImmutableArray<string?>>>(
			expectationBuilder.AddConstraint((it, grammars) =>
				new IsEqualToForEnumerableConstraint<ImmutableArray<string?>, string?, string?>(expectationBuilder, it,
					grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					expected,
					options,
					matchOptions)),
			source,
			options,
			matchOptions);
	}
#endif

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
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionMatchResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<TItem, TItem>(expectationBuilder, it, grammars,
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
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<double, double> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateDouble();
		CollectionMatchOptions matchOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<double>, IThat<IEnumerable<double>?>,
			double, double>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<double, double>(expectationBuilder, it, grammars,
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
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<double?, double> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateNullableDouble();
		CollectionMatchOptions matchOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<double?>, IThat<IEnumerable<double?>?>,
			double?, double>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<double?, double?>(expectationBuilder, it, grammars,
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
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<decimal, decimal> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateDecimal();
		CollectionMatchOptions matchOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<decimal>, IThat<IEnumerable<decimal>?>,
			decimal, decimal>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<decimal, decimal>(expectationBuilder, it, grammars,
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
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<decimal?, decimal> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateNullableDecimal();
		CollectionMatchOptions matchOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<decimal?>, IThat<IEnumerable<decimal?>?>,
			decimal?, decimal>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<decimal?, decimal?>(expectationBuilder, it, grammars,
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
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<float, float> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateFloat();
		CollectionMatchOptions matchOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<float>, IThat<IEnumerable<float>?>,
			float, float>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<float, float>(expectationBuilder, it, grammars,
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
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<float?, float> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateNullableFloat();
		CollectionMatchOptions matchOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<float?>, IThat<IEnumerable<float?>?>,
			float?, float>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<float?, float?>(expectationBuilder, it, grammars,
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
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<DateTime, TimeSpan> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateDateTime();
		CollectionMatchOptions matchOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<DateTime>, IThat<IEnumerable<DateTime>?>,
			DateTime, TimeSpan>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<DateTime, DateTime>(expectationBuilder, it, grammars,
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
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		ObjectEqualityWithToleranceOptions<DateTime?, TimeSpan> options =
			ObjectEqualityWithToleranceOptionsFactory.CreateNullableDateTime();
		CollectionMatchOptions matchOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionMatchWithToleranceResult<IEnumerable<DateTime?>, IThat<IEnumerable<DateTime?>?>,
			DateTime?, TimeSpan>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<DateTime?, DateTime?>(expectationBuilder, it, grammars,
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
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new StringCollectionMatchResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<string?, string?>(expectationBuilder, it, grammars,
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
	public static ObjectCollectionMatchResult<IEnumerable, IThat<IEnumerable>, TItem>
		IsNotEqualTo<TItem>(
			this IThat<IEnumerable> source,
			IEnumerable<TItem> unexpected,
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<TItem> options = new();
		CollectionMatchOptions matchOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionMatchResult<IEnumerable, IThat<IEnumerable>, TItem>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToForEnumerableConstraint<IEnumerable, TItem, TItem>(expectationBuilder, it, grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					unexpected,
					options,
					matchOptions).Invert()),
			source,
			options,
			matchOptions);
	}

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that the collection does not match the <paramref name="unexpected" /> collection.
	/// </summary>
	public static ObjectCollectionBeContainedInResult<ImmutableArray<TItem>, IThat<ImmutableArray<TItem>>, TItem>
		IsNotEqualTo<TItem>(
			this IThat<ImmutableArray<TItem>> source,
			IEnumerable<TItem> unexpected,
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<TItem> options = new();
		CollectionMatchOptions matchOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionBeContainedInResult<ImmutableArray<TItem>, IThat<ImmutableArray<TItem>>, TItem>(
			expectationBuilder.AddConstraint((it, grammars) =>
				new IsEqualToForEnumerableConstraint<ImmutableArray<TItem>, TItem, TItem>(expectationBuilder, it,
					grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					unexpected,
					options,
					matchOptions).Invert()),
			source,
			options,
			matchOptions);
	}
#endif

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that the collection does not match the <paramref name="unexpected" /> collection.
	/// </summary>
	public static StringCollectionBeContainedInResult<ImmutableArray<string?>, IThat<ImmutableArray<string?>>>
		IsNotEqualTo(this IThat<ImmutableArray<string?>> source,
			IEnumerable<string?> unexpected,
			[CallerArgumentExpression("unexpected")]
			string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new StringCollectionBeContainedInResult<ImmutableArray<string?>, IThat<ImmutableArray<string?>>>(
			expectationBuilder.AddConstraint((it, grammars) =>
				new IsEqualToForEnumerableConstraint<ImmutableArray<string?>, string?, string?>(expectationBuilder, it,
					grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					unexpected,
					options,
					matchOptions).Invert()),
			source,
			options,
			matchOptions);
	}
#endif

	/// <summary>
	///     Verifies that the collection matches a collection of <paramref name="predicates" />, where each item satisfies the corresponding predicate.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>
		IsEqualTo<TItem>(
			this IThat<IEnumerable<TItem>?> source,
			IEnumerable<Func<TItem, bool>> predicates,
			[CallerArgumentExpression("predicates")] string doNotPopulateThisValue = "")
	{
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>(
			expectationBuilder.AddConstraint((it, grammars) =>
				new IsEqualToPredicatesConstraint<TItem>(expectationBuilder, it, grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					predicates)),
			source);
	}

	/// <summary>
	///     Verifies that the collection matches a collection of <paramref name="expectations" />, where each item satisfies the corresponding expectation.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>
		IsEqualTo<TItem>(
			this IThat<IEnumerable<TItem>?> source,
			IEnumerable<Action<IThat<TItem>>> expectations,
			[CallerArgumentExpression("expectations")] string doNotPopulateThisValue = "")
	{
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>(
			expectationBuilder.AddConstraint((it, grammars) =>
				new IsEqualToExpectationsConstraint<TItem>(expectationBuilder, it, grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					expectations)),
			source);
	}

	private sealed class IsEqualToPredicatesConstraint<TItem>(
		ExpectationBuilder expectationBuilder,
		string it,
		ExpectationGrammars grammars,
		string predicatesExpression,
		IEnumerable<Func<TItem, bool>> predicates)
		: ConstraintResult(grammars),
			IContextConstraint<IEnumerable<TItem>?>
	{
		private IEnumerable<TItem>? _actual;
		private IList<Func<TItem, bool>>? _materializedPredicates;
		private IEnumerable<TItem>? _materializedEnumerable;
		private readonly List<(int Index, TItem Item, bool Result)> _results = new();

		public ConstraintResult IsMetBy(IEnumerable<TItem>? actual, IEvaluationContext context)
		{
			_actual = actual;
			if (actual is null)
			{
				Outcome = Outcome.Failure;
				return this;
			}

			_materializedEnumerable = context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			_materializedPredicates = predicates.ToList();
			
			var actualList = _materializedEnumerable.ToList();
			
			// Check if counts match
			if (actualList.Count != _materializedPredicates.Count)
			{
				Outcome = Outcome.Failure;
				expectationBuilder.AddCollectionContext(_materializedEnumerable);
				return this;
			}

			// Check each item against corresponding predicate
			for (int i = 0; i < actualList.Count; i++)
			{
				bool result = _materializedPredicates[i](actualList[i]);
				_results.Add((i, actualList[i], result));
				if (!result)
				{
					Outcome = Outcome.Failure;
					expectationBuilder.AddCollectionContext(_materializedEnumerable);
					return this;
				}
			}

			Outcome = Outcome.Success;
			return this;
		}

		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append($"matches predicates {predicatesExpression}");

		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (_actual == null)
			{
				stringBuilder.Append(it).Append(" was <null>");
			}
			else if (_materializedPredicates == null)
			{
				stringBuilder.Append(it).Append(" was not evaluated");
			}
			else
			{
				var actualList = _materializedEnumerable?.ToList() ?? new List<TItem>();
				if (actualList.Count != _materializedPredicates.Count)
				{
					stringBuilder.Append(it).Append($" had {actualList.Count} items but expected {_materializedPredicates.Count} predicates");
				}
				else
				{
					var failed = _results.Where(r => !r.Result).ToList();
					if (failed.Any())
					{
						stringBuilder.Append(it).Append($" had {failed.Count} item(s) that did not match their corresponding predicate");
						foreach (var failure in failed.Take(3))
						{
							stringBuilder.Append($"\n  - Item at index {failure.Index}: {Formatter.Format(failure.Item)}");
						}
						if (failed.Count > 3)
						{
							stringBuilder.Append($"\n  - (and {failed.Count - 3} more)");
						}
					}
				}
			}
		}

		public override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value) where TValue : default
		{
			if (_actual is TValue typedValue)
			{
				value = typedValue;
				return true;
			}

			value = default;
			return typeof(TValue).IsAssignableFrom(typeof(IEnumerable<TItem>));
		}

		public override ConstraintResult Negate()
		{
			// For predicates, negation would mean "does not match"
			// We'll invert the outcome after checking
			Outcome = Outcome switch
			{
				Outcome.Failure => Outcome.Success,
				Outcome.Success => Outcome.Failure,
				_ => Outcome,
			};
			return this;
		}
	}

	private sealed class IsEqualToExpectationsConstraint<TItem>(
		ExpectationBuilder expectationBuilder,
		string it,
		ExpectationGrammars grammars,
		string expectationsExpression,
		IEnumerable<Action<IThat<TItem>>> expectations)
		: ConstraintResult(grammars),
			IAsyncContextConstraint<IEnumerable<TItem>?>
	{
		private readonly ExpectationGrammars _grammars = grammars;
		private IEnumerable<TItem>? _actual;
		private IList<Action<IThat<TItem>>>? _materializedExpectations;
		private IEnumerable<TItem>? _materializedEnumerable;
		private readonly List<(int Index, TItem Item, string? FailureMessage)> _results = new();

		public async Task<ConstraintResult> IsMetBy(IEnumerable<TItem>? actual, IEvaluationContext context, CancellationToken cancellationToken)
		{
			_actual = actual;
			if (actual is null)
			{
				Outcome = Outcome.Failure;
				return this;
			}

			_materializedEnumerable = context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			_materializedExpectations = expectations.ToList();
			
			var actualList = _materializedEnumerable.ToList();
			
			// Check if counts match
			if (actualList.Count != _materializedExpectations.Count)
			{
				Outcome = Outcome.Failure;
				expectationBuilder.AddCollectionContext(_materializedEnumerable);
				return this;
			}

			// Check each item against corresponding expectation
			for (int i = 0; i < actualList.Count; i++)
			{
				try
				{
					var itemExpectationBuilder = new ManualExpectationBuilder<TItem>(expectationBuilder, _grammars);
					_materializedExpectations[i](new ThatSubject<TItem>(itemExpectationBuilder));
					var isMatch = await itemExpectationBuilder.IsMetBy(actualList[i], context, cancellationToken);
					if (isMatch.Outcome == Outcome.Success)
					{
						_results.Add((i, actualList[i], null));
					}
					else
					{
						_results.Add((i, actualList[i], isMatch.ToString()));
						Outcome = Outcome.Failure;
						expectationBuilder.AddCollectionContext(_materializedEnumerable);
						return this;
					}
				}
				catch (Exception ex)
				{
					_results.Add((i, actualList[i], ex.Message));
					Outcome = Outcome.Failure;
					expectationBuilder.AddCollectionContext(_materializedEnumerable);
					return this;
				}
			}

			Outcome = Outcome.Success;
			return this;
		}

		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append($"matches expectations {expectationsExpression}");

		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (_actual == null)
			{
				stringBuilder.Append(it).Append(" was <null>");
			}
			else if (_materializedExpectations == null)
			{
				stringBuilder.Append(it).Append(" was not evaluated");
			}
			else
			{
				var actualList = _materializedEnumerable?.ToList() ?? new List<TItem>();
				if (actualList.Count != _materializedExpectations.Count)
				{
					stringBuilder.Append(it).Append($" had {actualList.Count} items but expected {_materializedExpectations.Count} expectations");
				}
				else
				{
					var failed = _results.Where(r => r.FailureMessage != null).ToList();
					if (failed.Any())
					{
						stringBuilder.Append(it).Append($" had {failed.Count} item(s) that did not match their corresponding expectation");
						foreach (var failure in failed.Take(3))
						{
							stringBuilder.Append($"\n  - Item at index {failure.Index}: {Formatter.Format(failure.Item)} - {failure.FailureMessage}");
						}
						if (failed.Count > 3)
						{
							stringBuilder.Append($"\n  - (and {failed.Count - 3} more)");
						}
					}
				}
			}
		}

		public override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value) where TValue : default
		{
			if (_actual is TValue typedValue)
			{
				value = typedValue;
				return true;
			}

			value = default;
			return typeof(TValue).IsAssignableFrom(typeof(IEnumerable<TItem>));
		}

		public override ConstraintResult Negate()
		{
			// For expectations, negation would mean "does not match"
			// We'll invert the outcome after checking
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

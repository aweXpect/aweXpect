﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Core.Constraints;
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
}

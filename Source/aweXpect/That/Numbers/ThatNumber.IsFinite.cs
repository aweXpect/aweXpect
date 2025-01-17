﻿using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNumber
{
	private const string ExpectBeFinite = "be finite";
	private const string ExpectNotBeFinite = "not be finite";

	/// <summary>
	///     Verifies that the subject is seen as finite (neither <see cref="float.IsInfinity" /> nor <see cref="float.IsNaN" />
	///     ).
	/// </summary>
	public static AndOrResult<float, IExpectSubject<float>> IsFinite(
		this IExpectSubject<float> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<float>(
					it,
					float.PositiveInfinity,
					_ => ExpectBeFinite,
					(a, _) => !float.IsInfinity(a) && !float.IsNaN(a),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is seen as finite (neither <see cref="double.IsInfinity" /> nor
	///     <see cref="double.IsNaN" />).
	/// </summary>
	public static AndOrResult<double, IExpectSubject<double>> IsFinite(
		this IExpectSubject<double> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<double>(
					it,
					double.PositiveInfinity,
					_ => ExpectBeFinite,
					(a, _) => !double.IsInfinity(a) && !double.IsNaN(a),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is seen as finite (neither <see cref="float.IsInfinity" /> nor
	///     <see cref="float.IsNaN" /> nor <see langword="null" />).
	/// </summary>
	public static AndOrResult<float, IExpectSubject<float?>> IsFinite(
		this IExpectSubject<float?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<float>(
					it,
					float.PositiveInfinity,
					_ => ExpectBeFinite,
					(a, _) => a != null && !float.IsInfinity(a.Value) && !float.IsNaN(a.Value),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is seen as finite (neither <see cref="double.IsInfinity" /> nor
	///     <see cref="double.IsNaN" /> nor <see langword="null" />).
	/// </summary>
	public static AndOrResult<double, IExpectSubject<double?>> IsFinite(
		this IExpectSubject<double?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<double>(
					it,
					double.PositiveInfinity,
					_ => ExpectBeFinite,
					(a, _) => a != null && !double.IsInfinity(a.Value) && !double.IsNaN(a.Value),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is not seen as finite (either <see cref="float.IsInfinity" /> or
	///     <see cref="float.IsNaN" />).
	/// </summary>
	public static AndOrResult<float, IExpectSubject<float>> IsNotFinite(
		this IExpectSubject<float> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<float>(
					it,
					float.PositiveInfinity,
					_ => ExpectNotBeFinite,
					(a, _) => float.IsInfinity(a) || float.IsNaN(a),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is not seen as finite (either <see cref="double.IsInfinity" /> or
	///     <see cref="double.IsNaN" />).
	/// </summary>
	public static AndOrResult<double, IExpectSubject<double>> IsNotFinite(
		this IExpectSubject<double> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<double>(
					it,
					double.PositiveInfinity,
					_ => ExpectNotBeFinite,
					(a, _) => double.IsInfinity(a) || double.IsNaN(a),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is not seen as finite (either <see cref="float.IsInfinity" /> or
	///     <see cref="float.IsNaN" /> or <see langword="null" />).
	/// </summary>
	public static AndOrResult<float?, IExpectSubject<float?>> IsNotFinite(
		this IExpectSubject<float?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<float>(
					it,
					float.PositiveInfinity,
					_ => ExpectNotBeFinite,
					(a, _) => a == null || float.IsInfinity(a.Value) || float.IsNaN(a.Value),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is not seen as finite (either <see cref="double.IsInfinity" /> or
	///     <see cref="double.IsNaN" /> or <see langword="null" />).
	/// </summary>
	public static AndOrResult<double?, IExpectSubject<double?>> IsNotFinite(
		this IExpectSubject<double?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<double>(
					it,
					double.PositiveInfinity,
					_ => ExpectNotBeFinite,
					(a, _) => a == null || double.IsInfinity(a.Value) || double.IsNaN(a.Value),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);
}
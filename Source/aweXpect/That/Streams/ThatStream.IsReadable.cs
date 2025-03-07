﻿using System.IO;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatStream
{
	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> is readable.
	/// </summary>
	public static AndOrResult<Stream?, IThat<Stream?>> IsReadable(
		this IThat<Stream?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new ValueConstraint(
					"is readable",
					actual => actual?.CanRead == true,
					actual => actual == null ? $"{it} was <null>" : $"{it} was not")),
			source);

	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> is not readable.
	/// </summary>
	public static AndOrResult<Stream?, IThat<Stream?>> IsNotReadable(
		this IThat<Stream?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new ValueConstraint(
					"is not readable",
					actual => actual?.CanRead == false,
					actual => actual == null ? $"{it} was <null>" : $"{it} was")),
			source);
}

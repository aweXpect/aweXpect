using System;
using aweXpect.SourceGenerators;

namespace aweXpect;

[CreateExpectationOnNullable<Guid>("Is{Not}NullOrEmpty", "{value} is null || {value} == Guid.Empty",
	ExpectationText = "is {not} null or empty",
	Using = ["System",],
	FailOnNull = false
)]
public static partial class ThatNullableGuid;

using System;
using aweXpect.SourceGenerators;

namespace aweXpect;

[CreateExpectationOnNullable<Guid>("Is{Not}Empty", "{value} == Guid.Empty",
	ExpectationText = "is {not} empty",
	Using = ["System",]
)]
public static partial class ThatNullableGuid;

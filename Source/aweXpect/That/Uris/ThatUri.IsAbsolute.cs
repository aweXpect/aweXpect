using System;
using aweXpect.SourceGenerators;

namespace aweXpect;

[CreateExpectationOn<Uri>("Is{Not}Absolute", "{value}.IsAbsoluteUri",
	ExpectationText = "is {not} an absolute URI",
	Using = ["System",],
	Remarks = """
	          <seealso cref="Uri.IsAbsoluteUri" />
	          """
)]
public static partial class ThatUri;

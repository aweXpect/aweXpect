using System;
using aweXpect.SourceGenerators;

namespace aweXpect;

[CreateExpectationOn<Uri>("Is{Not}Loopback", "{value}.IsLoopback",
	PositiveExpectationText = "references the local host",
	NegativeExpectationText = "does not reference the local host",
	Using = ["System",],
	Remarks = """
	          <seealso cref="Uri.IsLoopback" />
	          """
)]
public static partial class ThatUri;

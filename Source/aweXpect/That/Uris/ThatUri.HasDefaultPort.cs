using System;
using aweXpect.SourceGenerators;

namespace aweXpect;

[CreateExpectationOn<Uri>("HasDefaultPort", "DoesNotHaveDefaultPort", "{value}.IsDefaultPort",
	PositiveExpectationText = "has the default port for the used scheme",
	NegativeExpectationText = "does not have the default port for the used scheme",
	Using = ["System",],
	Remarks = """
	          <seealso cref="Uri.IsDefaultPort" />
	          """
)]
public static partial class ThatUri;

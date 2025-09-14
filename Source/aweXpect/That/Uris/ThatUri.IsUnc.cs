using System;
using aweXpect.SourceGenerators;

namespace aweXpect;

[CreateExpectationOn<Uri>("Is{Not}Unc", "{value}.IsUnc",
	ExpectationText = "is {not} an UNC path",
	Using = ["System",],
	Remarks = """
	          <seealso cref="Uri.IsUnc" />
	          """
)]
public static partial class ThatUri;

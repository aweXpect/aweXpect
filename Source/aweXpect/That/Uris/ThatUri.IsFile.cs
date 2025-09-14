using System;
using aweXpect.SourceGenerators;

namespace aweXpect;

[CreateExpectationOn<Uri>("Is{Not}File", "{value}.IsFile",
	ExpectationText = "is {not} a file URI",
	Using = ["System",],
	Remarks = """
	          <seealso cref="Uri.IsFile" />
	          """
)]
public static partial class ThatUri;

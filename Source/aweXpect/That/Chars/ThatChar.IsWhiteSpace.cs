using aweXpect.SourceGenerators;

namespace aweXpect;

[CreateExpectationOn<char>("Is{Not}WhiteSpace", "char.IsWhiteSpace({value})",
	ExpectationText = "is {not} white-space",
	Remarks = """
	          This means, that the specified Unicode character is categorized as white-space.<br />
	          <seealso cref="char.IsWhiteSpace(char)" />
	          """
)]
public static partial class ThatChar;

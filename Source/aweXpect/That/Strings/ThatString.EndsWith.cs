using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatString
{
	/// <summary>
	///     Verifies that the subject ends with the <paramref name="expected" /> <see langword="string" />.
	/// </summary>
	public static StringEqualityResult<string?, IThat<string?>> EndsWith(
		this IThat<string?> source,
		string expected)
	{
		StringEqualityOptions options = new StringEqualityOptions().AsSuffix();
		return new StringEqualityResult<string?, IThat<string?>>(
			source.Get().ExpectationBuilder.AddConstraint((expectationBuilder, it, grammars) =>
				new IsEqualToConstraint(expectationBuilder, it, grammars, expected, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject does not end with the <paramref name="unexpected" /> <see langword="string" />.
	/// </summary>
	public static StringEqualityResult<string?, IThat<string?>> DoesNotEndWith(
		this IThat<string?> source,
		string unexpected)
	{
		StringEqualityOptions options = new StringEqualityOptions().AsSuffix();
		return new StringEqualityResult<string?, IThat<string?>>(
			source.Get().ExpectationBuilder.AddConstraint((expectationBuilder, it, grammars) =>
				new IsEqualToConstraint(expectationBuilder, it, grammars, unexpected, options).Invert()),
			source,
			options);
	}
}

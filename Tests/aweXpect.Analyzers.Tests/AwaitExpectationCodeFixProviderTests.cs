using System.Threading.Tasks;
using Verifier =
	aweXpect.Analyzers.Tests.Verifiers.CSharpCodeFixVerifier<aweXpect.Analyzers.AwaitExpectationAnalyzer,
		aweXpect.Analyzers.CodeFixers.AwaitExpectationCodeFixProvider>;

namespace aweXpect.Analyzers.Tests;

public class AwaitExpectationCodeFixProviderTests
{
	[Fact]
	public async Task ShouldApplyCodeFix() => await Verifier.VerifyCodeFixAsync(
		"""
		using System.Threading.Tasks;
		using aweXpect;

		public class MyClass
		{
		    public async Task MyTest()
		    {
		        var subject = true;
		        [|Expect.That(subject)|].IsTrue();
		    }
		}
		""",
		"""
		using System.Threading.Tasks;
		using aweXpect;

		public class MyClass
		{
		    public async Task MyTest()
		    {
		        var subject = true;
		        await Expect.That(subject).IsTrue();
		    }
		}
		""");

	[Fact]
	public async Task ShouldMakeContainingMethodAsync() => await Verifier.VerifyCodeFixAsync(
		"""
		using System.Threading.Tasks;
		using aweXpect;

		public class MyClass
		{
		    public void MyTest()
		    {
		        var subject = true;
		        [|Expect.That(subject)|].IsTrue();
		    }
		}
		""",
		"""
		using System.Threading.Tasks;
		using aweXpect;

		public class MyClass
		{
		    public async Task MyTest()
		    {
		        var subject = true;
		        await Expect.That(subject).IsTrue();
		    }
		}
		""");
}

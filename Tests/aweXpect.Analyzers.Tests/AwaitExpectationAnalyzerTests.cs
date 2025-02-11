using System.Threading.Tasks;
using Verifier = aweXpect.Analyzers.Tests.Verifiers.CSharpAnalyzerVerifier<aweXpect.Analyzers.AwaitExpectationAnalyzer>;

namespace aweXpect.Analyzers.Tests;

public class AwaitExpectationAnalyzerTests
{
	[Fact]
	public async Task WhenAwaited_ShouldNotBeFlagged() => await Verifier
		.VerifyAnalyzerAsync(
			"""
			using System.Threading.Tasks;
			using aweXpect;

			public class MyClass
			{
			    public async Task MyTest()
			    {
			        var subject = true;
			        await {|#0:Expect.That(subject)|}.IsTrue();
			    }
			}
			"""
		);

	[Fact]
	public async Task WhenAwaited_WithoutReturnValue_ShouldNotBeFlagged() => await Verifier
		.VerifyAnalyzerAsync(
			"""
			using System.Threading.Tasks;
			using aweXpect;

			public class MyClass
			{
			    public async Task MyTest()
			    {
			        await {|#0:Expect.That(() => {})|}.DoesNotThrow();
			    }
			}
			"""
		);

	[Fact]
	public async Task WhenNotAwaited_ShouldBeFlagged() => await Verifier
		.VerifyAnalyzerAsync(
			"""
			using System.Threading.Tasks;
			using aweXpect;

			public class MyClass
			{
			    public async Task MyTest()
			    {
			        var subject = true;
			        {|#0:Expect.That(subject)|}.IsTrue();
			    }
			}
			""",
			Verifier.Diagnostic(Rules.AwaitExpectationRule)
				.WithLocation(0)
		);

	[Fact]
	public async Task WhenNotAwaited_WithoutReturnValue_ShouldBeFlagged() => await Verifier
		.VerifyAnalyzerAsync(
			"""
			using System.Threading.Tasks;
			using aweXpect;

			public class MyClass
			{
			    public async Task MyTest()
			    {
			        {|#0:Expect.That(() => {})|}.DoesNotThrow();
			    }
			}
			""",
			Verifier.Diagnostic(Rules.AwaitExpectationRule)
				.WithLocation(0)
		);

	[Fact]
	public async Task WhenVerified_ShouldNotBeFlagged() => await Verifier
		.VerifyAnalyzerAsync(
			"""
			using System.Threading.Tasks;
			using aweXpect;
			using aweXpect.Synchronous;

			public class MyClass
			{
			    public async Task MyTest()
			    {
			        var subject = true;
			        {|#0:Expect.That(subject)|}.IsTrue().Verify();
			    }
			}
			"""
		);

	[Fact]
	public async Task WhenVerified_WithoutReturnValue_ShouldNotBeFlagged() => await Verifier
		.VerifyAnalyzerAsync(
			"""
			using System.Threading.Tasks;
			using aweXpect;
			using aweXpect.Synchronous;

			public class MyClass
			{
			    public async Task MyTest()
			    {
			        {|#0:Expect.That(() => {})|}.DoesNotThrow().Verify();
			    }
			}
			"""
		);

	[Fact]
	public async Task WhenVerifiedStatically_ShouldNotBeFlagged() => await Verifier
		.VerifyAnalyzerAsync(
			"""
			using System.Threading.Tasks;
			using aweXpect;
			using aweXpect.Synchronous;

			public class MyClass
			{
			    public async Task MyTest()
			    {
			        var subject = true;
			        Synchronously.Verify({|#0:Expect.That(subject)|}.IsTrue());
			    }
			}
			"""
		);

	[Fact]
	public async Task WhenVerifiedStatically_WithoutReturnValue_ShouldNotBeFlagged() => await Verifier
		.VerifyAnalyzerAsync(
			"""
			using System.Threading.Tasks;
			using aweXpect;
			using aweXpect.Synchronous;

			public class MyClass
			{
			    public async Task MyTest()
			    {
			        Synchronously.Verify({|#0:Expect.That(() => {})|}.DoesNotThrow());
			    }
			}
			"""
		);
}

using System.Reflection;
using aweXpect.Core.Adapters;
using aweXpect.Core.Tests.TestHelpers;
using Xunit.Abstractions;

namespace aweXpect.Core.Tests.Core.Adapters;

public sealed class TestFrameworkAdapterTests : IDisposable
{
	private readonly XunitTraceWriter _writer;

	public TestFrameworkAdapterTests(ITestOutputHelper testOutputHelper)
	{
		_writer = new XunitTraceWriter(testOutputHelper);
	}

	public void Dispose() => _writer.Dispose();

	[Fact]
	public async Task FromType_WhenNameDoesNotExist_ShouldReturnNull()
	{
		string typeName = "type-that-does-not-exist";
		Assembly assembly = typeof(TestFrameworkAdapterTests).Assembly;

		Exception? exception = MyTestFrameworkAdapter.FromTypeWrapper(typeName, assembly, "foo");

		await That(exception).IsNull();
	}

	[Fact]
	public async Task Inconclusive_ValidAssemblyName_ShouldThrowNotSupportedException()
	{
		MyTestFrameworkAdapter adapter = new(ExistingAssembly);
		_ = adapter.IsAvailable;

		await That(() => adapter.Inconclusive("foo")).Throws<NotSupportedException>()
			.WithMessage("Failed to create the inconclusive assertion type");
	}

	[Fact]
	public async Task Inconclusive_ValidAssemblyName_WithException_ShouldThrowProvidedException()
	{
		MyException exception = new("my-message");
		MyTestFrameworkAdapter adapter = new(ExistingAssembly, inconclusiveException: exception);
		_ = adapter.IsAvailable;

		await That(() => adapter.Inconclusive("foo")).Throws<MyException>()
			.WithMessage("my-message");
	}

	[Fact]
	public async Task MissingAssemblyName_ShouldNotBeAvailable()
	{
		MyTestFrameworkAdapter adapter = new(MissingAssembly);
		_ = adapter.IsAvailable;

		await That(adapter.IsAvailable).IsFalse();
	}

	[Fact]
	public async Task Skip_MissingAssemblyName_ShouldThrowNotSupportedException()
	{
		MyTestFrameworkAdapter adapter = new(MissingAssembly, skipException: new MyException());
		_ = adapter.IsAvailable;

		await That(() => adapter.Skip("foo")).Throws<NotSupportedException>()
			.WithMessage("Failed to create the skip assertion type");
	}

	[Fact]
	public async Task Skip_ValidAssemblyName_ShouldThrowNotSupportedException()
	{
		MyTestFrameworkAdapter adapter = new(ExistingAssembly);
		_ = adapter.IsAvailable;

		await That(() => adapter.Skip("foo")).Throws<NotSupportedException>()
			.WithMessage("Failed to create the skip assertion type");
	}

	[Fact]
	public async Task Skip_ValidAssemblyName_WithException_ShouldThrowProvidedException()
	{
		MyException exception = new("my-message");
		MyTestFrameworkAdapter adapter = new(ExistingAssembly, skipException: exception);
		_ = adapter.IsAvailable;

		await That(() => adapter.Skip("foo")).Throws<MyException>()
			.WithMessage("my-message");
	}

	[Fact]
	public async Task Throw_MissingAssemblyName_ShouldThrowNotSupportedException()
	{
		MyTestFrameworkAdapter adapter = new(MissingAssembly, new MyException());
		_ = adapter.IsAvailable;

		await That(() => adapter.Fail("foo")).Throws<NotSupportedException>()
			.WithMessage("Failed to create the fail assertion type");
	}

	[Fact]
	public async Task Throw_ValidAssemblyName_ShouldThrowNotSupportedException()
	{
		MyTestFrameworkAdapter adapter = new(ExistingAssembly);
		_ = adapter.IsAvailable;

		await That(() => adapter.Fail("foo")).Throws<NotSupportedException>()
			.WithMessage("Failed to create the fail assertion type");
	}

	[Fact]
	public async Task Throw_ValidAssemblyName_WithException_ShouldThrowProvidedException()
	{
		MyException exception = new("my-message");
		MyTestFrameworkAdapter adapter = new(ExistingAssembly, exception);
		_ = adapter.IsAvailable;

		await That(() => adapter.Fail("foo")).Throws<MyException>()
			.WithMessage("my-message");
	}

	[Fact]
	public async Task ValidAssemblyName_ShouldBeAvailable()
	{
		MyTestFrameworkAdapter adapter = new(ExistingAssembly);
		_ = adapter.IsAvailable;

		await That(adapter.IsAvailable).IsTrue();
	}

	private const string ExistingAssembly = "aweXpect.Core.Tests";
	private const string MissingAssembly = "this-assembly-does-not-exist";

	private sealed class MyTestFrameworkAdapter : TestFrameworkAdapter
	{
		public MyTestFrameworkAdapter() : this(MissingAssembly)
		{
			// An empty constructor is required in order to avoid an exception in the ambient initialization "DetectFramework" method!
		}

		public MyTestFrameworkAdapter(string assemblyName,
			Exception? failException = null,
			Exception? skipException = null,
			Exception? inconclusiveException = null)
			: base(assemblyName,
				(_, _) => failException,
				(_, _) => skipException,
				(_, _) => inconclusiveException)
		{
		}

		public static Exception? FromTypeWrapper(string typeName, Assembly assembly, string message)
			=> FromType(typeName, assembly, message);
	}
}

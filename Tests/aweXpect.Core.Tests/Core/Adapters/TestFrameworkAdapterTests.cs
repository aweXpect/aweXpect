using System.Reflection;
using aweXpect.Adapters;
using aweXpect.Core.Tests.TestHelpers;

namespace aweXpect.Core.Tests.Core.Adapters;

public sealed class TestFrameworkAdapterTests
{
	[Fact]
	public async Task FromType_WhenNameDoesNotExist_ShouldReturnNull()
	{
		string typeName = "type-that-does-not-exist";
		Assembly assembly = typeof(TestFrameworkAdapterTests).Assembly;

		Exception? exception = MyTestFrameworkAdapter.FromType(typeName, assembly, "foo");

		await That(exception).IsNull();
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
		MyTestFrameworkAdapter adapter = new(MissingAssembly);
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
		MyTestFrameworkAdapter adapter = new(MissingAssembly);
		_ = adapter.IsAvailable;

		await That(() => adapter.Throw("foo")).Throws<NotSupportedException>()
			.WithMessage("Failed to create the fail assertion type");
	}

	[Fact]
	public async Task Throw_ValidAssemblyName_ShouldThrowNotSupportedException()
	{
		MyTestFrameworkAdapter adapter = new(ExistingAssembly);
		_ = adapter.IsAvailable;

		await That(() => adapter.Throw("foo")).Throws<NotSupportedException>()
			.WithMessage("Failed to create the fail assertion type");
	}

	[Fact]
	public async Task Throw_ValidAssemblyName_WithException_ShouldThrowProvidedException()
	{
		MyException exception = new("my-message");
		MyTestFrameworkAdapter adapter = new(ExistingAssembly, exception);
		_ = adapter.IsAvailable;

		await That(() => adapter.Throw("foo")).Throws<MyException>()
			.WithMessage("my-message");
	}

	[Fact]
	public async Task ValidAssemblyName_ShouldBeAvailable()
	{
		MyTestFrameworkAdapter adapter = new(ExistingAssembly);
		_ = adapter.IsAvailable;

		await That(adapter.IsAvailable).IsTrue();
	}

	public static string ExistingAssembly { get; } = typeof(TestFrameworkAdapterTests).Assembly.FullName[..20];
	public static string MissingAssembly { get; } = "this-assembly-does-not-exist";

	private sealed class MyTestFrameworkAdapter : TestFrameworkAdapter
	{
		public MyTestFrameworkAdapter() : this(MissingAssembly)
		{
			// An empty constructor is required in order to avoid an exception in the ambient initialization "DetectFramework" method!
		}

		public MyTestFrameworkAdapter(string assemblyName,
			Exception? failException = null,
			Exception? skipException = null)
			: base(assemblyName,
				(_, _) => failException,
				(_, _) => skipException)
		{
		}

		public static Exception? FromType(string typeName, Assembly assembly, string message)
			=> TestFrameworkAdapter.FromType(typeName, assembly, message);
	}
}

using System.Diagnostics.CodeAnalysis;

namespace Testably.Expectations.Core.Adapters;

/// <summary>
///     Represents an adapter to a particular test framework such as xUnit, nUnit, etc.
/// </summary>
internal interface ITestFrameworkAdapter
{
	/// <summary>
	///     Gets a value indicating whether the corresponding test framework is currently available.
	/// </summary>
	bool IsAvailable { get; }

	/// <summary>
	///     Throws a framework-specific exception to indicate a skipped unit test.
	/// </summary>
	[DoesNotReturn]
	void Skip(string message);

	/// <summary>
	///     Throws a framework-specific exception to indicate a failing unit test.
	/// </summary>
	[DoesNotReturn]
	void Throw(string message);
}

using System;
using System.Collections.Generic;

namespace aweXpect.Customization;

/// <summary>
///     Customizes the reflection settings.
/// </summary>
public interface ICustomizeReflection
{
	/// <summary>
	///     The assembly namespace prefixes that are excluded during reflection.
	/// </summary>
	/// <remarks>
	///     Defaults to<br />
	///     - mscorlib<br />
	///     - System<br />
	///     - Microsoft<br />
	///     - JetBrains<br />
	///     - xunit<br />
	///     - Castle<br />
	///     - DynamicProxyGenAssembly2
	/// </remarks>
	string[] ExcludedAssemblyPrefixes { get; }

	/// <summary>
	///     Specifies the assembly prefixes that should be excluded.
	/// </summary>
	/// <returns>An object, that will revert the excluded assemblies to the default value upon disposal.</returns>
	/// <remarks>The <see cref="Action{T}" /> allows changing the excluded assembly prefixes.</remarks>
	IDisposable ExcludeAssemblies(Action<List<string>> excludedAssemblyPrefixes);
}

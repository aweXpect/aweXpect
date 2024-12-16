using System;
using System.Collections.Generic;
using System.Threading;

namespace aweXpect.Customization;

public partial class Customize : ICustomizeReflection
{
	private static readonly string[] ExcludedAssemblyPrefixesDefaultValue =
	[
		"mscorlib",
		"System",
		"Microsoft",
		"JetBrains",
		"xunit",
		"Castle",
		"DynamicProxyGenAssembly2"
	];

	private readonly AsyncLocal<string[]?> _excludedAssemblyPrefixes = new();

	/// <summary>
	///     Customizes the reflection settings.
	/// </summary>
	public static ICustomizeReflection Reflection => Instance;

	/// <inheritdoc />
	string[] ICustomizeReflection.ExcludedAssemblyPrefixes
		=> _excludedAssemblyPrefixes.Value ?? ExcludedAssemblyPrefixesDefaultValue;

	/// <inheritdoc />
	IDisposable ICustomizeReflection.ExcludeAssemblies(Action<List<string>> excludedAssemblyPrefixes)
	{
		List<string> list = new(_excludedAssemblyPrefixes.Value ?? ExcludedAssemblyPrefixesDefaultValue);
		excludedAssemblyPrefixes(list);
		_excludedAssemblyPrefixes.Value = list.ToArray();
		return new ActionDisposable(() => _excludedAssemblyPrefixes.Value = null);
	}
}

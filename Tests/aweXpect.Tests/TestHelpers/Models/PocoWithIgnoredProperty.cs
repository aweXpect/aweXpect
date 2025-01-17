#if NET8_0_OR_GREATER
using System.Text.Json.Serialization;

namespace aweXpect.Tests.TestHelpers.Models;

public class PocoWithIgnoredProperty
{
	public int Id { get; set; }

	[JsonIgnore] public string? Name { get; set; }
}
#endif

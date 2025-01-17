#if NET8_0_OR_GREATER
using System.Text.Json.Serialization;

namespace aweXpect.Tests.Models;

public class PocoWithPrivateConstructorWithJsonConstructorAttribute
{
	[JsonConstructor]
	private PocoWithPrivateConstructorWithJsonConstructorAttribute() { }

	public int Id { get; init; }

	public static PocoWithPrivateConstructorWithJsonConstructorAttribute Create(int id) => new()
	{
		Id = id
	};
}

#endif

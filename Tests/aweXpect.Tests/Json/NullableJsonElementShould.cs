﻿#if NET8_0_OR_GREATER
using System.Text.Json;

namespace aweXpect.Tests.Json;

public sealed partial class NullableJsonElementShould
{
	public static JsonElement? FromString(string value)
	{
		using JsonDocument document = JsonDocument.Parse(value);
		return document.RootElement.Clone();
	}
}
#endif

// Copyright (c) 2024 by Tom Longhurst
// https://github.com/thomhurst/TUnit

using System;

namespace aweXpect.Equivalency;

internal record ComparisonFailure
{
	public object? Actual { get; set; }
	public object? Expected { get; set; }
	public string[] NestedMemberNames { get; set; } = Array.Empty<string>();
	public MemberType Type { get; set; }
}

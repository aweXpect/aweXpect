﻿// Copyright (c) 2024 by Tom Longhurst
// https://github.com/thomhurst/TUnit

namespace aweXpect.Equivalency;

internal record CompareOptions
{
	public string[] MembersToIgnore { get; set; } = [];
}

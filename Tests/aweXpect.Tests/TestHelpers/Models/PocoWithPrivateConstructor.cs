﻿namespace aweXpect.Tests.TestHelpers.Models;

public class PocoWithPrivateConstructor
{
	private PocoWithPrivateConstructor() { }

	public int Id { get; set; }

	public static PocoWithPrivateConstructor Create(int id) => new()
	{
		Id = id
	};
}
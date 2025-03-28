﻿using System.Runtime.CompilerServices;

namespace aweXpect.Tests;

public class HResultException : Exception
{
	public HResultException(int hResult,
		[CallerMemberName] string message = "",
		Exception? innerException = null)
		: base(message, innerException)
	{
		HResult = hResult;
	}
}

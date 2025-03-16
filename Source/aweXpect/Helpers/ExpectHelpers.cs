﻿using System;
using aweXpect.Core;

namespace aweXpect.Helpers;

internal static class ExpectHelpers
{
	public static IExpectThat<T> Get<T>(this IThat<T> subject)
	{
		if (subject is IExpectThat<T> expectThat)
		{
			return expectThat;
		}

		throw new NotSupportedException("IThat<T> must also implement IExpectThat<T>");
	}
}

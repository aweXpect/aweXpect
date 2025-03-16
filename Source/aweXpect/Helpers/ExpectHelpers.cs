using System;
using aweXpect.Core;

namespace aweXpect.Helpers;

internal static class ExpectHelpers
{
	public static IThatIs<T> Get<T>(this IThat<T> subject)
	{
		if (subject is IThatIs<T> thatIs)
		{
			return thatIs;
		}

		if (subject is IThatVerb<T> thatVerb)
		{
			return new ThatSubject<T>(thatVerb.ExpectationBuilder);
		}

		throw new NotSupportedException("IThat<T> must also implement IThatIs<T>");
	}

	public static IThatHas<T> ThatHas<T>(this IThat<T> subject)
	{
		if (subject is IThatHas<T> thatHas)
		{
			return thatHas;
		}

		if (subject is IThatVerb<T> thatVerb)
		{
			return new ThatSubject<T>(thatVerb.ExpectationBuilder);
		}

		throw new NotSupportedException("IThat<T> must also implement IThatHas<T>");
	}

	public static IThat<T> ExpectSubject<T>(this IThatIs<T> thatIs)
	{
		if (thatIs is IThat<T> expectSubject)
		{
			return expectSubject;
		}

		return new ThatSubject<T>(thatIs.ExpectationBuilder);
	}

	public static IThat<T> ExpectSubject<T>(this IThatHas<T> thatHas)
	{
		if (thatHas is IThat<T> expectSubject)
		{
			return expectSubject;
		}

		return new ThatSubject<T>(thatHas.ExpectationBuilder);
	}
}

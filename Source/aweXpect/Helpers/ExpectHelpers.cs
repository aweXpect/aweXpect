using aweXpect.Core;

namespace aweXpect.Helpers;

internal static class ExpectHelpers
{
	public static IThatIs<T> ThatIs<T>(this IExpectSubject<T> subject)
	{
		if (subject is IThatIs<T> thatIs)
		{
			return thatIs;
		}

		return new That.Subject<T>(subject.Should(That.WithoutAction).ExpectationBuilder);
	}
	
	public static IThatHas<T> ThatHas<T>(this IExpectSubject<T> subject)
	{
		if (subject is IThatHas<T> thatHas)
		{
			return thatHas;
		}

		return new That.Subject<T>(subject.Should(That.WithoutAction).ExpectationBuilder);
	}
	
	public static IExpectSubject<T> ExpectSubject<T>(this IThatIs<T> thatIs)
	{
		if (thatIs is IExpectSubject<T> expectSubject)
		{
			return expectSubject;
		}

		return new That.Subject<T>(thatIs.ExpectationBuilder);
	}
	
	public static IExpectSubject<T> Expect<T>(this IThatHas<T> thatHas)
	{
		if (thatHas is IExpectSubject<T> expectSubject)
		{
			return expectSubject;
		}

		return new That.Subject<T>(thatHas.ExpectationBuilder);
	}
}

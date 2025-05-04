using System;
using aweXpect.Customization;

namespace aweXpect.Core.Helpers;

internal static class ExceptionHelpers
{
	public static TException LogTrace<TException>(this TException exception)
		where TException : Exception
	{
		Customize.aweXpect.TraceWriter.Value?.WriteException(exception);
		return exception;
	}
}

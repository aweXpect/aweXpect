using System;
using System.Collections.Generic;

namespace aweXpect.Helpers;

internal static class ExceptionHelpers
{
	public static string FormatForMessage(this Exception exception)
	{
		string message = Formatter.Format(exception.GetType()).PrependAOrAn();
		if (!string.IsNullOrEmpty(exception.Message))
		{
			message += ":" + Environment.NewLine + exception.Message.Indent();
		}

		return message;
	}

	public static IEnumerable<Exception> GetInnerExceptions(this Exception? actual)
	{
		switch (actual)
		{
			case AggregateException aggregateException:
				{
					foreach (Exception innerException in aggregateException.InnerExceptions)
					{
						yield return innerException;
						foreach (Exception inner in GetInnerExceptions(innerException))
						{
							yield return inner;
						}
					}

					break;
				}
			default:
				{
					if (actual?.InnerException is not null)
					{
						yield return actual.InnerException;
						foreach (Exception inner in GetInnerExceptions(actual.InnerException))
						{
							yield return inner;
						}
					}

					break;
				}
		}
	}
}

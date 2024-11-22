using System;
using System.Collections.Generic;

namespace Testably.Expectations.Core.Helpers;

internal static class ExceptionHelpers
{
	public static string FormatForMessage(this Exception exception)
	{
		string message = exception.GetType().Name.PrependAOrAn();
		if (!string.IsNullOrEmpty(exception.Message))
		{
			message += ":" + Environment.NewLine + exception.Message.Indent();
		}

		return message;
	}

	public static IEnumerable<Exception> GetInnerExpectations(this Exception? actual)
	{
		switch (actual)
		{
			case null:
				yield break;
			case AggregateException aggregateException:
				{
					foreach (Exception innerException in aggregateException.InnerExceptions)
					{
						yield return innerException;
						foreach (Exception inner in GetInnerExpectations(innerException))
						{
							yield return inner;
						}
					}

					break;
				}
			default:
				{
					if (actual.InnerException != null)
					{
						yield return actual.InnerException;
						foreach (Exception inner in GetInnerExpectations(actual.InnerException))
						{
							yield return inner;
						}
					}

					break;
				}
		}
	}
}

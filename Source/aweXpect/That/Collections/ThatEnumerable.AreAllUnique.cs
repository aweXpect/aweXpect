using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Customization;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Start expectations on the current enumerable of <typeparamref name="TItem" /> values.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>> AreAllUnique<TItem>(
		this IExpectSubject<IEnumerable<TItem>> subject)
	{
		throw new NotImplementedException();
	}
}

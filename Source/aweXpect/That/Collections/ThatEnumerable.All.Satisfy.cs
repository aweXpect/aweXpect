using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	public partial class ThatAll<TItem>
	{
		public AndOrResult<IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>> Satisfy(Func<TItem, bool> predicate)
			=> throw new NotImplementedException();
	}
}

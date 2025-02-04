#if NET8_0_OR_GREATER
using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatAsyncEnumerable
{
	public partial class Elements<TItem>
	{
		/// <summary>
		///     …comply with the <paramref name="expectations" />.
		/// </summary>
		public ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>
			ComplyWith(Action<IThat<TItem>> expectations)
		{
			ObjectEqualityOptions options = new();
			return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint(it
					=> new AsyncCollectionConstraint<TItem>(it, _quantifier, expectations)),
				_subject,
				options);
		}
	}
}
#endif

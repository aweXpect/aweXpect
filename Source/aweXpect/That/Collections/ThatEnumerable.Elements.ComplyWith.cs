using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	public partial class Elements<TItem>
	{
		/// <summary>
		///     …comply with the <paramref name="expectations" />.
		/// </summary>
		public ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>
			ComplyWith(Action<IThat<TItem>> expectations)
		{
			ObjectEqualityOptions options = new();
			return new ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint(it
					=> new SyncCollectionConstraint<TItem>(it, _quantifier, expectations)),
				_subject,
				options);
		}
	}
}

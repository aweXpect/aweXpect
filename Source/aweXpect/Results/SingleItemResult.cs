using System;
using System.Diagnostics;
using System.Threading.Tasks;
using aweXpect.Core;

namespace aweXpect.Results;

/// <summary>
///     An <see cref="ExpectationResult" /> when an exception was thrown.
/// </summary>
public class SingleItemResult<TCollection, TItem>
	: ExpectationResult<TItem, SingleItemResult<TCollection, TItem>>
{
	private readonly ExpectationBuilder _expectationBuilder;
	private readonly Func<TCollection, TItem?> _memberAccessor;

	internal SingleItemResult(ExpectationBuilder expectationBuilder, Func<TCollection, TItem?> memberAccessor)
		: base(expectationBuilder)
	{
		_expectationBuilder = expectationBuilder;
		_memberAccessor = memberAccessor;
	}

	/// <summary>
	///     Further expectations on the single <typeparamref name="TItem" />
	/// </summary>
	public IExpectSubject<TItem> Which
		=> new ThatSubject<TItem>(_expectationBuilder.ForWhich(_memberAccessor, " which should "));

	public class Async : ExpectationResult<TItem, Async>
	{
		private readonly ExpectationBuilder _expectationBuilder;
		private readonly Func<TCollection, Task<TItem?>> _asyncMemberAccessor;

		internal Async(ExpectationBuilder expectationBuilder, Func<TCollection, Task<TItem?>> asyncMemberAccessor)
			: base(expectationBuilder)
		{
			_expectationBuilder = expectationBuilder;
			_asyncMemberAccessor = asyncMemberAccessor;
		}

		/// <summary>
		///     Further expectations on the single item.
		/// </summary>
		public IExpectSubject<TItem> Which
			=> new ThatSubject<TItem>(_expectationBuilder.ForWhich(_asyncMemberAccessor, " which should "));
	}

	[DebuggerDisplay("Expect.ThatSubject<{typeof(T)}>: {ExpectationBuilder}")]
	private readonly struct ThatSubject<T>(ExpectationBuilder expectationBuilder)
		: IExpectSubject<T>, IThat<T>
	{
		public ExpectationBuilder ExpectationBuilder { get; } = expectationBuilder;

		/// <inheritdoc />
		public IThat<T> Should(Action<ExpectationBuilder> builderOptions)
		{
			builderOptions.Invoke(ExpectationBuilder);
			return this;
		}
	}
}

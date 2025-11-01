using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     The result for verifying that a collection contains a single item.
/// </summary>
/// <remarks>
///     <seealso cref="ExpectationResult{TType,TSelf}" />
/// </remarks>
public class SingleItemResult<TCollection, TItem>
	: ExpectationResult<TItem, SingleItemResult<TCollection, TItem>>,
		IOptionsProvider<PredicateOptions<TItem>>
{
	private readonly ExpectationBuilder _expectationBuilder;
	private readonly Func<TCollection, TItem?> _memberAccessor;
	private readonly PredicateOptions<TItem> _options;

	internal SingleItemResult(ExpectationBuilder expectationBuilder,
		PredicateOptions<TItem> options,
		Func<TCollection, TItem?> memberAccessor)
		: base(expectationBuilder)
	{
		_expectationBuilder = expectationBuilder;
		_options = options;
		_memberAccessor = memberAccessor;
	}

	/// <summary>
	///     Further expectations on the single <typeparamref name="TItem" />
	/// </summary>
	public IThat<TItem> Which
		=> new ThatSubject<TItem>(_expectationBuilder.ForWhich(_memberAccessor, " which "));

	/// <inheritdoc cref="IOptionsProvider{TOptions}.Options" />
	PredicateOptions<TItem> IOptionsProvider<PredicateOptions<TItem>>.Options => _options;

	/// <summary>
	///     …that satisfies the <paramref name="predicate" />.
	/// </summary>
	public SingleItemResult<TCollection, TItem> Matching(Func<TItem, bool> predicate,
		[CallerArgumentExpression("predicate")]
		string doNotPopulateThisValue = "")
	{
		predicate.ThrowIfNull();
		_options.SetPredicate(predicate,
			$" matching {doNotPopulateThisValue}");
		return this;
	}

	/// <summary>
	///     …of type <typeparamref name="T" />.
	/// </summary>
	public SingleItemResult<TCollection, T> Matching<T>()
	{
		_options.SetPredicate(item => item is T,
			$" of type {Formatter.Format(typeof(T))}");
		return Cast<T>(x => (T)(object)x!);
	}

	/// <summary>
	///     …of type <typeparamref name="T" /> that satisfies the <paramref name="predicate" />.
	/// </summary>
	public SingleItemResult<TCollection, T> Matching<T>(Func<T, bool> predicate,
		[CallerArgumentExpression("predicate")]
		string doNotPopulateThisValue = "")
	{
		predicate.ThrowIfNull();
		_options.SetPredicate(item => item is T typed && predicate(typed),
			$" of type {Formatter.Format(typeof(T))} matching {doNotPopulateThisValue}");
		return Cast<T>(x => (T)(object)x!);
	}

	private SingleItemResult<TCollection, T> Cast<T>(Func<TItem?, T> memberAccessor)
		=> new(_expectationBuilder, new PredicateOptions<T>(), x => memberAccessor(_memberAccessor(x)));

	/// <summary>
	///     An <see cref="ExpectationResult" /> for a single item from an asynchronous collection.
	/// </summary>
	public class Async : ExpectationResult<TItem, Async>,
		IOptionsProvider<PredicateOptions<TItem>>
	{
		private readonly Func<TCollection, Task<TItem?>> _asyncMemberAccessor;
		private readonly ExpectationBuilder _expectationBuilder;
		private readonly PredicateOptions<TItem> _options;

		internal Async(ExpectationBuilder expectationBuilder,
			PredicateOptions<TItem> options,
			Func<TCollection, Task<TItem?>> asyncMemberAccessor)
			: base(expectationBuilder)
		{
			_expectationBuilder = expectationBuilder;
			_options = options;
			_asyncMemberAccessor = asyncMemberAccessor;
		}

		/// <summary>
		///     Further expectations on the single item.
		/// </summary>
		public IThat<TItem> Which
			=> new ThatSubject<TItem>(_expectationBuilder.ForWhich(_asyncMemberAccessor, " which "));

		/// <inheritdoc cref="IOptionsProvider{TOptions}.Options" />
		PredicateOptions<TItem> IOptionsProvider<PredicateOptions<TItem>>.Options => _options;

		/// <summary>
		///     …that satisfies the <paramref name="predicate" />.
		/// </summary>
		public Async Matching(Func<TItem, bool> predicate,
			[CallerArgumentExpression("predicate")]
			string doNotPopulateThisValue = "")
		{
			predicate.ThrowIfNull();
			_options.SetPredicate(predicate,
				$" matching {doNotPopulateThisValue}");
			return this;
		}

		/// <summary>
		///     …of type <typeparamref name="T" />.
		/// </summary>
		public SingleItemResult<TCollection, T>.Async Matching<T>()
		{
			_options.SetPredicate(item => item is T,
				$" of type {Formatter.Format(typeof(T))}");
			return Cast<T>(x => (T)(object)x!);
		}

		/// <summary>
		///     …of type <typeparamref name="T" /> that satisfies the <paramref name="predicate" />.
		/// </summary>
		public SingleItemResult<TCollection, T>.Async Matching<T>(Func<T, bool> predicate,
			[CallerArgumentExpression("predicate")]
			string doNotPopulateThisValue = "")
		{
			predicate.ThrowIfNull();
			_options.SetPredicate(item => item is T typed && predicate(typed),
				$" of type {Formatter.Format(typeof(T))} matching {doNotPopulateThisValue}");
			return Cast<T>(x => (T)(object)x!);
		}

		private SingleItemResult<TCollection, T>.Async Cast<T>(Func<TItem?, T> memberAccessor)
			=> new(_expectationBuilder, new PredicateOptions<T>(),
				async x => memberAccessor(await _asyncMemberAccessor(x)));
	}
}

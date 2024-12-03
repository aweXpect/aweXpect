using System.Collections.Generic;
using System.Linq;
using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect.Options;

/// <summary>
///     Options for matching a collection.
/// </summary>
public class CollectionMatchOptions
{
	private bool _ignoringDuplicates;
	private bool _inAnyOrder;

	/// <summary>
	///     Ignores the order in the subject and expected values.
	/// </summary>
	public void InAnyOrder() => _inAnyOrder = true;

	/// <summary>
	///     Ignores duplicates in both collections.
	/// </summary>
	public void IgnoringDuplicates() => _ignoringDuplicates = true;

	/// <summary>
	///     Get the collection matcher for the <paramref name="expected" /> enumerable.
	/// </summary>
	public ICollectionMatcher<T, T2> GetCollectionMatcher<T, T2>(IEnumerable<T> expected)
		where T : T2
		=> (_inAnyOrder, _ignoringDuplicates) switch
		{
			(true, true) => new AnyOrderIgnoreDuplicatesCollectionMatcher<T, T2>(expected),
			(true, false) => new AnyOrderCollectionMatcher<T, T2>(expected),
			(false, true) => new SameOrderIgnoreDuplicatesCollectionMatcher<T, T2>(expected),
			(false, false) => new SameOrderCollectionMatcher<T, T2>(expected)
		};

	/// <inheritdoc />
	public override string ToString()
		=> (_inAnyOrder, _ignoringDuplicates) switch
		{
			(true, true) => " in any order ignoring duplicates",
			(true, false) => " in any order",
			(false, true) => " ignoring duplicates",
			(false, false) => ""
		};

	private sealed class AnyOrderCollectionMatcher<T, T2>(IEnumerable<T> expected) : ICollectionMatcher<T, T2>
		where T : T2
	{
		private readonly List<T> expectedList = expected.ToList();
		private int _index;

		public string? Verify(string it, T value, IOptionsEquality<T2> options)
		{
			if (expectedList.All(e => !options.AreConsideredEqual(value, e)))
			{
				return
					$"{it} contained item {Formatter.Format(value)} at index {_index++} that was not expected";
			}

			expectedList.Remove(value);
			_index++;
			return null;
		}

		public string? VerifyComplete(string it, IOptionsEquality<T2> options)
		{
			if (expectedList.Any())
			{
				return
					$"{it} lacked {expectedList.Count} of {expectedList.Count + _index} expected items: {Formatter.Format(expectedList,
						expectedList.Count > 1 ? FormattingOptions.MultipleLines : FormattingOptions.SingleLine)}";
			}

			return null;
		}

		public void Dispose() => _index = -1;
	}

	private sealed class AnyOrderIgnoreDuplicatesCollectionMatcher<T, T2>(IEnumerable<T> expected) : ICollectionMatcher<T, T2>
		where T : T2
	{
		private readonly HashSet<T> _uniqueItems = new();
		private readonly List<T> expectedList = expected.ToList();
		private int _index;

		public string? Verify(string it, T value, IOptionsEquality<T2> options)
		{
			if (_uniqueItems.Contains(value))
			{
				return null;
			}

			if (expectedList.All(e => !options.AreConsideredEqual(value, e)))
			{
				_uniqueItems.Add(value);
				return
					$"{it} contained item {Formatter.Format(value)} at index {_index++} that was not expected";
			}

			_uniqueItems.Add(value);
			_index++;
			return null;
		}

		public string? VerifyComplete(string it, IOptionsEquality<T2> options)
		{
			List<T> missingItems = expectedList.Except(_uniqueItems).Distinct().ToList();
			if (missingItems.Any())
			{
				return
					$"{it} lacked {missingItems.Count} of {missingItems.Count + _index} expected items: {Formatter.Format(missingItems,
						expectedList.Count > 1 ? FormattingOptions.MultipleLines : FormattingOptions.SingleLine)}";
			}

			return null;
		}

		public void Dispose() => _index = -1;
	}

	private sealed class SameOrderCollectionMatcher<T, T2>(IEnumerable<T> expected) : ICollectionMatcher<T, T2>
		where T : T2
	{
		private readonly IEnumerator<T> _expectedEnumerator = MaterializingEnumerable<T>.Wrap(expected).GetEnumerator();
		private int _index;

		public string? Verify(string it, T value, IOptionsEquality<T2> options)
		{
			if (!_expectedEnumerator.MoveNext())
			{
				return $"{it} contained item {Formatter.Format(value)} at index {_index++} that was not expected";
			}

			if (!options.AreConsideredEqual(value, _expectedEnumerator.Current))
			{
				return
					$"{it} contained item {Formatter.Format(value)} at index {_index++} instead of {Formatter.Format(_expectedEnumerator.Current)}";
			}

			_index++;
			return null;
		}

		public string? VerifyComplete(string it, IOptionsEquality<T2> options)
		{
			if (_expectedEnumerator.MoveNext())
			{
				List<T> missingItems =
				[
					_expectedEnumerator.Current
				];
				while (_expectedEnumerator.MoveNext())
				{
					missingItems.Add(_expectedEnumerator.Current);
				}

				return
					$"{it} lacked {missingItems.Count} of {missingItems.Count + _index} expected items: {Formatter.Format(missingItems,
						missingItems.Count > 1 ? FormattingOptions.MultipleLines : FormattingOptions.SingleLine)}";
			}

			return null;
		}

		public void Dispose()
		{
			_index = -1;
			_expectedEnumerator.Dispose();
		}
	}

	private sealed class SameOrderIgnoreDuplicatesCollectionMatcher<T, T2>(IEnumerable<T> expected)
		: ICollectionMatcher<T, T2>
		where T : T2
	{
		private readonly IEnumerator<T> _expectedEnumerator = MaterializingEnumerable<T>.Wrap(expected).GetEnumerator();
		private readonly HashSet<T> _uniqueItems = new();
		private int _index;

		public string? Verify(string it, T value, IOptionsEquality<T2> options)
		{
			if (_uniqueItems.Contains(value))
			{
				return null;
			}

			while (_expectedEnumerator.MoveNext())
			{
				if (!_uniqueItems.Contains(_expectedEnumerator.Current))
				{
					if (!options.AreConsideredEqual(value, _expectedEnumerator.Current))
					{
						_uniqueItems.Add(_expectedEnumerator.Current);
						return
							$"{it} contained item {Formatter.Format(value)} at index {_index++} instead of {Formatter.Format(_expectedEnumerator.Current)}";
					}

					break;
				}
			}

			_uniqueItems.Add(value);
			_index++;
			return null;
		}

		public string? VerifyComplete(string it, IOptionsEquality<T2> options)
		{
			List<T> missingItems = new();
			while (_expectedEnumerator.MoveNext())
			{
				T value = _expectedEnumerator.Current;
				if (_uniqueItems.Add(value))
				{
					missingItems.Add(value);
				}
			}

			if (missingItems.Any())
			{
				return
					$"{it} lacked {missingItems.Count} of {missingItems.Count + _index} expected items: {Formatter.Format(missingItems,
						missingItems.Count > 1 ? FormattingOptions.MultipleLines : FormattingOptions.SingleLine)}";
			}

			return null;
		}

		public void Dispose()
		{
			_uniqueItems.Clear();
			_index = -1;
			_expectedEnumerator.Dispose();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace aweXpect.Tests.ThatTests.Collections;

public partial class EnumerableShould
{
	public static IEnumerable<int> ToEnumerable(int[] items)
	{
		foreach (int item in items)
		{
			yield return item;
		}
	}

	public static IEnumerable<string> ToEnumerable(string[] items)
	{
		foreach (string item in items)
		{
			yield return item;
		}
	}

	/// <summary>
	///     Returns an <see cref="IEnumerable{T}" /> with incrementing numbers, starting with 0, which cancels the
	///     <paramref name="cancellationTokenSource" /> after <paramref name="cancelAfter" /> iteration.
	/// </summary>
	private static IEnumerable<int> GetCancellingEnumerable(
		int cancelAfter,
		CancellationTokenSource cancellationTokenSource,
		int limit = 10_000)
	{
		int index = 0;
		while (index < limit)
		{
			if (index == cancelAfter)
			{
				cancellationTokenSource.Cancel();
			}

			yield return index++;
		}
	}

	public sealed class ThrowWhenIteratingTwiceEnumerable : IEnumerable<int>
	{
		private bool _isEnumerated;

		#region IEnumerable<int> Members

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public IEnumerator<int> GetEnumerator()
		{
			if (_isEnumerated)
			{
				Fail.Test("The enumerable was enumerated twice!");
			}

			_isEnumerated = true;
			yield return 1;
		}

		#endregion
	}

	public class MyClass(int value = 0)
	{
		public InnerClass? Inner { get; set; }
		public int Value { get; set; } = value;
	}

	public class InnerClass
	{
		public IEnumerable<string>? Collection { get; set; }

		public InnerClass? Inner { get; set; }
		public string? Value { get; set; }
	}
}

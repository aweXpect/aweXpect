#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace aweXpect.Tests;

public partial class ThatImmutableArray
{
	public static ImmutableArray<int> ToSubject(int[] items)
		=> [..items,];

	public static ImmutableArray<string> ToSubject(string[] items)
		=> [..items,];

	public static ImmutableArray<T> ToSubject<T>(IEnumerable<int> items, Func<int, T> mapper)
		=> [..items.Select(mapper),];

	public static ImmutableArray<T> ToSubject<T>(IEnumerable<string> items, Func<string, T> mapper)
		=> [..items.Select(mapper),];

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
#endif

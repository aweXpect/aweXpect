using System.Collections.Generic;
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace aweXpect.Tests.Objects;

public sealed partial class ObjectShould
{
	private class MyBaseClass
	{
		public int Value { get; set; }
	}

	private sealed class MyClass : MyBaseClass;

	private sealed class OtherClass;

	private sealed class InnerClass
	{
		public IEnumerable<string>? Collection { get; set; }

		public InnerClass? Inner { get; set; }
		public string? Value { get; set; }
	}

	private sealed class OuterClass
	{
		public InnerClass? Inner { get; set; }
		public string? Value { get; set; }
	}
}

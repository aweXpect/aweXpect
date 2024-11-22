using System.Collections.Generic;

namespace aweXpect.Tests.ThatTests.Collections;

public partial class CollectionShould
{
	public class MyClass
	{
		public InnerClass? Inner { get; set; }
		public string? Value { get; set; }
	}

	public class InnerClass
	{
		public IEnumerable<string>? Collection { get; set; }

		public InnerClass? Inner { get; set; }
		public string? Value { get; set; }
	}
}

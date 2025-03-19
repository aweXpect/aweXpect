// ReSharper disable UnusedMember.Local
// ReSharper disable ClassNeverInstantiated.Local

namespace aweXpect.Core.Tests.Core.Constraints;

public partial class ConstraintResultTests
{
	private class MyBaseClass
	{
		public int? Value { get; set; }
	}

	private sealed class MyDerivedClass : MyBaseClass
	{
		public int? OtherValue { get; set; }
	}
}

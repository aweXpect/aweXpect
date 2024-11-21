namespace aweXpect.Tests;

public class UnitTest1
{
	[Fact]
	public void Test1()
	{
		Class1 sut = new();
		
		int result = sut.Double(1);

		Assert.Equal(2, result);
	}
	[Fact]
	public void Test2()
	{
		Class1 sut = new();
		
		int result = sut.SomethingComplex(1);

		Assert.Equal(1, result);
	}
}

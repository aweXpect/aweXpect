namespace aweXpect.Discovery.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
	    Class3 sut = new();
		
	    int result = sut.Double(1);

	    Assert.Equal(2, result);
    }
}

namespace aweXpect;

public class Class1
{
	public int Double(int value)
	{
		return value * 2;
	}
	
	public int Triple(int value)
	{
		return value * 3;
	}
	
	public int SomethingComplex(int value)
	{
		for (int i = 0; i < 100; i++)
		{
			value += i;
		}

		value -= 1;
		value += 1;
		value -= 1;
		value += 1;
		value -= 1;
		value += 1;
		value -= 1;
		value += 1;
		value -= 1;
		value += 1;

		value -= 1;
		value += 1;
		value -= 1;
		value += 1;
		value -= 1;
		value += 1;
		value -= 1;
		value += 1;
		value -= 1;
		value += 1;

		for (int i = 99; i >= 0; i--)
		{
			value -= i;
		}

		return value;
	}
}

﻿namespace aweXpect.Tests;

public sealed partial class ThatEnum
{
	// ReSharper disable once ClassNeverInstantiated.Global
	public sealed partial class Nullable
	{
		public enum EnumLong : long
		{
			Int64Max = long.MaxValue,
			Int64LessOne = long.MaxValue - 1,
		}

		public enum EnumULong : ulong
		{
			Int64Max = long.MaxValue,
			UInt64LessOne = ulong.MaxValue - 1,
			UInt64Max = ulong.MaxValue,
		}

		[Flags]
		public enum MyColors
		{
			Blue = 1 << 0,
			Green = 1 << 1,
			Yellow = 1 << 2,
			Red = 1 << 3,
		}

		public enum MyNumbers
		{
			One = 1,
			Two = 2,
			Three = 3,
		}
	}
}

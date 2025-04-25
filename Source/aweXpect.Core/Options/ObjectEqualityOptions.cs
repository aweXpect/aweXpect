using System;
using System.Collections.Generic;
using aweXpect.Core;

namespace aweXpect.Options;

internal static class ObjectEqualityOptions
{
	internal static readonly IObjectMatchType EqualsMatch = new EqualsMatchType();

	private sealed class EqualsMatchType : IObjectMatchType
	{
		/// <inheritdoc cref="object.ToString()" />
		public override string ToString() => "";

		#region IEquality Members

		/// <inheritdoc cref="IObjectMatchType.AreConsideredEqual{TSubject, TExpected}(TSubject, TExpected)" />
		public bool AreConsideredEqual<TActual, TExpected>(TActual actual, TExpected expected)
		{
			if (actual is null && expected is null)
			{
				return true;
			}

			if (actual is null || expected is null)
			{
				return false;
			}

			if (expected is TActual castedExpected &&
			    EqualityComparer<TActual>.Default.Equals(actual, castedExpected))
			{
				return true;
			}

			if (typeof(TActual) == typeof(object) &&
			    AreNumericsEqual(actual, expected))
			{
				return true;
			}

			return Equals(actual, expected);
		}

		private bool AreNumericsEqual(object actual, object expected)
		{
			Type expectedType = expected.GetType();
			Type actualType = actual.GetType();

			return actualType != expectedType
			       && IsNumericType(actual)
			       && IsNumericType(expected)
			       && IsEqualWhenConverted(actual, expected, expectedType)
			       && IsEqualWhenConverted(expected, actual, actualType);

			bool IsNumericType(object obj)
			{
				return obj is
#if NET8_0_OR_GREATER
					Int128 or
					UInt128 or
					nint or
					nuint or
					Half or
#endif
					int or
					long or
					float or
					double or
					decimal or
					sbyte or
					byte or
					short or
					ushort or
					uint or
					ulong;
			}
		}

		private static bool IsEqualWhenConverted(object source, object target, Type targetType)
		{
			try
			{
#if NET8_0_OR_GREATER
				dynamic sourceNumber = source;
				object? convertedNumber = null;
				if (targetType == typeof(int))
				{
					convertedNumber = (int)sourceNumber;
				}
				else if (targetType == typeof(long))
				{
					convertedNumber = (long)sourceNumber;
				}
				else if (targetType == typeof(float))
				{
					convertedNumber = (float)sourceNumber;
				}
				else if (targetType == typeof(double))
				{
					convertedNumber = (double)sourceNumber;
				}
				else if (targetType == typeof(decimal))
				{
					convertedNumber = (decimal)sourceNumber;
				}
				else if (targetType == typeof(sbyte))
				{
					convertedNumber = (sbyte)sourceNumber;
				}
				else if (targetType == typeof(byte))
				{
					convertedNumber = (byte)sourceNumber;
				}
				else if (targetType == typeof(short))
				{
					convertedNumber = (short)sourceNumber;
				}
				else if (targetType == typeof(ushort))
				{
					convertedNumber = (ushort)sourceNumber;
				}
				else if (targetType == typeof(uint))
				{
					convertedNumber = (uint)sourceNumber;
				}
				else if (targetType == typeof(ulong))
				{
					convertedNumber = (ulong)sourceNumber;
				}
				else if (targetType == typeof(Int128))
				{
					convertedNumber = (Int128)sourceNumber;
				}
				else if (targetType == typeof(UInt128))
				{
					convertedNumber = (UInt128)sourceNumber;
				}
				else if (targetType == typeof(Half))
				{
					convertedNumber = (Half)sourceNumber;
				}
#else
					object? convertedNumber =
 Convert.ChangeType(source, targetType, System.Globalization.CultureInfo.InvariantCulture);
#endif
				return target.Equals(convertedNumber);
			}
			catch
			{
				return false;
			}
		}

		/// <inheritdoc cref="IObjectMatchType.GetExpectation(string, ExpectationGrammars)" />
		public string GetExpectation(string expected, ExpectationGrammars grammars)
			=> $"is {(grammars.IsNegated() ? "not " : "")}equal to {expected}";

		/// <inheritdoc cref="IObjectMatchType.GetExtendedFailure(string, ExpectationGrammars, object?, object?)" />
		public string GetExtendedFailure(string it, ExpectationGrammars grammars, object? actual, object? expected)
			=> $"{it} was {Formatter.Format(actual, FormattingOptions.Indented())}";

		#endregion
	}
}

/// <summary>
///     Checks equality of objects.
/// </summary>
public partial class ObjectEqualityOptions<TSubject> : IOptionsEquality<TSubject>
{
	private IObjectMatchType _matchType = ObjectEqualityOptions.EqualsMatch;

	/// <inheritdoc />
	public bool AreConsideredEqual<TExpected>(TSubject? actual, TExpected? expected)
		=> _matchType.AreConsideredEqual(actual, expected);

	/// <summary>
	///     Specifies a new <see cref="IStringMatchType" /> to use for matching two strings.
	/// </summary>
	public void SetMatchType(IObjectMatchType matchType) => _matchType = matchType;

	/// <summary>
	///     Get an extended failure text.
	/// </summary>
	public string GetExtendedFailure(string it, ExpectationGrammars grammars, object? actual, object? expected)
		=> _matchType.GetExtendedFailure(it, grammars, actual, expected);

	/// <summary>
	///     Returns the expectation string, e.g. <c>be equal to {expectedExpression}</c>.
	/// </summary>
	public string GetExpectation(string expectedExpression, ExpectationGrammars grammars)
		=> _matchType.GetExpectation(expectedExpression, grammars);

	/// <inheritdoc />
	public override string? ToString() => _matchType.ToString();
}

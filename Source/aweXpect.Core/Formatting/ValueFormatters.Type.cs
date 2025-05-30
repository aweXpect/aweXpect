using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using aweXpect.Core.Helpers;

namespace aweXpect.Formatting;

public static partial class ValueFormatters
{
	private static readonly Dictionary<Type, string> Aliases = new()
	{
		{
			typeof(byte), "byte"
		},
		{
			typeof(sbyte), "sbyte"
		},
		{
			typeof(short), "short"
		},
		{
			typeof(ushort), "ushort"
		},
		{
			typeof(int), "int"
		},
		{
			typeof(uint), "uint"
		},
		{
			typeof(long), "long"
		},
		{
			typeof(ulong), "ulong"
		},
		{
			typeof(float), "float"
		},
		{
			typeof(double), "double"
		},
		{
			typeof(decimal), "decimal"
		},
		{
			typeof(object), "object"
		},
		{
			typeof(bool), "bool"
		},
		{
			typeof(char), "char"
		},
		{
			typeof(string), "string"
		},
		{
			typeof(void), "void"
		},
		{
			typeof(nint), "nint"
		},
		{
			typeof(nuint), "nuint"
		},
	};

	/// <summary>
	///     Returns the formatted <paramref name="value" /> according to the <paramref name="options" />.
	/// </summary>
	public static string Format(
		this ValueFormatter formatter,
		Type? value,
		FormattingOptions? options = null)
	{
		if (value == null)
		{
			return ValueFormatter.NullString;
		}

		StringBuilder stringBuilder = new();
		FormatType(value, stringBuilder);
		return stringBuilder.ToString();
	}

	/// <summary>
	///     Appends the formatted <paramref name="value" /> according to the <paramref name="options" />
	///     to the <paramref name="stringBuilder" />
	/// </summary>
	public static void Format(
		this ValueFormatter formatter,
		StringBuilder stringBuilder,
		Type? value,
		FormattingOptions? options = null)
	{
		if (value == null)
		{
			stringBuilder.Append(ValueFormatter.NullString);
			return;
		}

		FormatType(value, stringBuilder);
	}

#pragma warning disable S3776 // https://rules.sonarsource.com/csharp/RSPEC-3776
	private static void FormatType(
		Type value,
		StringBuilder stringBuilder)
	{
		if (value.IsArray)
		{
			FormatType(value.GetElementType()!, stringBuilder);
			stringBuilder.Append("[]");
		}
		else if (TryFindPrimitiveAlias(value, out string? alias))
		{
			stringBuilder.Append(alias);
		}
		else
		{
			if (value.IsNested && value.DeclaringType is not null)
			{
				FormatType(value.DeclaringType, stringBuilder);
				stringBuilder.Append('.');
			}

			if (value.IsGenericType)
			{
				Type genericTypeDefinition = value.GetGenericTypeDefinition();
				stringBuilder.Append(genericTypeDefinition.Name.SubstringUntilFirst('`'));
				stringBuilder.Append('<');
				bool isFirstArgument = true;
				foreach (Type argument in value.GetGenericArguments())
				{
					if (!isFirstArgument)
					{
						stringBuilder.Append(", ");
					}

					isFirstArgument = false;
					if (!argument.ContainsGenericParameters)
					{
						FormatType(argument, stringBuilder);
					}
				}

				stringBuilder.Append('>');
			}
			else
			{
				stringBuilder.Append(value.Name);
			}
		}
	}
#pragma warning restore S3776

	private static bool TryFindPrimitiveAlias(Type value, [NotNullWhen(true)] out string? alias)
	{
		if (Aliases.TryGetValue(value, out string? typeAlias))
		{
			alias = typeAlias;
			return true;
		}

		Type? underlyingType = Nullable.GetUnderlyingType(value);

		if (underlyingType != null &&
		    Aliases.TryGetValue(underlyingType, out string? underlyingAlias))
		{
			alias = $"{underlyingAlias}?";
			return true;
		}

		alias = null;
		return false;
	}
}

using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace aweXpect.Core.Tests.Formatting;

public partial class ValueFormatters
{
	public sealed class TypeTests
	{
		[Fact]
		public async Task ShouldSupportArraySyntax()
		{
			Type value = typeof(int[]);
			string expectedResult = "int[]";
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).Should().Be(expectedResult);
			await That(objectResult).Should().Be(expectedResult);
			await That(sb.ToString()).Should().Be(expectedResult);
		}

		[Fact]
		public async Task ShouldSupportArraySyntaxWithComplexObjects()
		{
			Type value = typeof(TypeTests[]);
			string expectedResult = $"{nameof(TypeTests)}[]";
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).Should().Be(expectedResult);
			await That(objectResult).Should().Be(expectedResult);
			await That(sb.ToString()).Should().Be(expectedResult);
		}

		[Fact]
		public async Task ShouldSupportGenericTypeDefinitions()
		{
			Type value = typeof(IEnumerable<int>);
			string expectedResult = "IEnumerable<int>";
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).Should().Be(expectedResult);
			await That(objectResult).Should().Be(expectedResult);
			await That(sb.ToString()).Should().Be(expectedResult);
		}

		[Fact]
		public async Task ShouldSupportNestedGenericTypeDefinitions()
		{
			Type value = typeof(Expression<Func<TypeTests[], bool>>);
			string expectedResult = $"Expression<Func<{nameof(TypeTests)}[], bool>>";
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).Should().Be(expectedResult);
			await That(objectResult).Should().Be(expectedResult);
			await That(sb.ToString()).Should().Be(expectedResult);
		}

		[Theory]
		[MemberData(nameof(SimpleTypes))]
		public async Task SimpleTypes_ShouldUseSimpleNames(Type value, string expectedResult)
		{
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).Should().Be(expectedResult);
			await That(objectResult).Should().Be(expectedResult);
			await That(objectResult).Should().Be(expectedResult);
			await That(sb.ToString()).Should().Be(expectedResult);
		}

		[Fact]
		public async Task Types_ShouldOnlyIncludeTheName()
		{
			Type value = typeof(TypeTests);
			string expectedResult = nameof(TypeTests);
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).Should().Be(expectedResult);
			await That(objectResult).Should().Be(expectedResult);
			await That(sb.ToString()).Should().Be(expectedResult);
		}

		[Fact]
		public async Task WhenNull_ShouldUseDefaultNullString()
		{
			Type? value = null;
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).Should().Be(ValueFormatter.NullString);
			await That(objectResult).Should().Be(ValueFormatter.NullString);
			await That(sb.ToString()).Should().Be(ValueFormatter.NullString);
		}

		public static TheoryData<Type, string> SimpleTypes
			=> new()
			{
				{
					typeof(int), "int"
				},
				{
					typeof(int?), "int?"
				},
				{
					typeof(uint), "uint"
				},
				{
					typeof(uint?), "uint?"
				},
				{
					typeof(nint), "nint"
				},
				{
					typeof(nint?), "nint?"
				},
				{
					typeof(nuint), "nuint"
				},
				{
					typeof(nuint?), "nuint?"
				},
				{
					typeof(byte), "byte"
				},
				{
					typeof(byte?), "byte?"
				},
				{
					typeof(sbyte), "sbyte"
				},
				{
					typeof(sbyte?), "sbyte?"
				},
				{
					typeof(short), "short"
				},
				{
					typeof(short?), "short?"
				},
				{
					typeof(ushort), "ushort"
				},
				{
					typeof(ushort?), "ushort?"
				},
				{
					typeof(long), "long"
				},
				{
					typeof(long?), "long?"
				},
				{
					typeof(ulong), "ulong"
				},
				{
					typeof(ulong?), "ulong?"
				},
				{
					typeof(float), "float"
				},
				{
					typeof(float?), "float?"
				},
				{
					typeof(double), "double"
				},
				{
					typeof(double?), "double?"
				},
				{
					typeof(decimal), "decimal"
				},
				{
					typeof(decimal?), "decimal?"
				},
				{
					typeof(string), "string"
				},
				{
					typeof(object), "object"
				},
				{
					typeof(bool), "bool"
				},
				{
					typeof(bool?), "bool?"
				},
				{
					typeof(char), "char"
				},
				{
					typeof(char?), "char?"
				},
				{
					typeof(void), "void"
				}
			};
	}
}

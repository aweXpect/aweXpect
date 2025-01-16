#if NET8_0_OR_GREATER
using System.Text.Json;
using aweXpect.Tests.TestHelpers.Models;

namespace aweXpect.Tests;

public sealed partial class ThatObject
{
	public sealed partial class Is
	{
		public sealed class JsonSerializable
		{
			public sealed class ObjectTests
			{
				[Fact]
				public async Task WhenSubjectHasAnIgnoredProperty_ShouldFail()
				{
					PocoWithIgnoredProperty subject = new()
					{
						Id = 2,
						Name = "foo"
					};

					async Task Act()
						=> await That(subject).Is().JsonSerializable();

					await That(Act).Does().Throw<XunitException>()
						.WithMessage("""
						             Expected subject to
						             be serializable as JSON,
						             but Property Name did not match:
						               Expected: "foo"
						               Received: <null>
						             """);
				}

				[Fact]
				public async Task WhenSubjectHasAnIgnoredProperty_WhenPropertyIsIgnored_ShouldSucceed()
				{
					PocoWithIgnoredProperty subject = new()
					{
						Id = 2,
						Name = "foo"
					};

					async Task Act()
						=> await That(subject).Is().JsonSerializable(o => o.IgnoringMember("Name"));

					await That(Act).Does().NotThrow();
				}

				[Fact]
				public async Task WhenSubjectHasAPrivateConstructor_ShouldFail()
				{
					PocoWithPrivateConstructor subject = PocoWithPrivateConstructor.Create(42);

					async Task Act()
						=> await That(subject).Is().JsonSerializable();

					await That(Act).Does().Throw<XunitException>()
						.WithMessage("""
						             Expected subject to
						             be serializable as JSON,
						             but it could not be deserialized: Deserialization of types without a parameterless constructor, a singular parameterized constructor, or a parameterized constructor annotated with 'JsonConstructorAttribute' is not supported. Type 'aweXpect.Tests.TestHelpers.Models.PocoWithPrivateConstructor'*
						             """).AsWildcard();
				}

				[Fact]
				public async Task WhenSubjectHasAPrivateConstructorWithJsonConstructorAttribute_ShouldSucceed()
				{
					PocoWithPrivateConstructorWithJsonConstructorAttribute subject =
						PocoWithPrivateConstructorWithJsonConstructorAttribute.Create(42);

					async Task Act()
						=> await That(subject).Is().JsonSerializable();

					await That(Act).Does().NotThrow();
				}

				[Fact]
				public async Task WhenSubjectHasNoDefaultConstructor_ShouldFail()
				{
					PocoWithoutDefaultConstructor subject = new(12);

					async Task Act()
						=> await That(subject).Is().JsonSerializable();

					await That(Act).Does().Throw<XunitException>()
						.WithMessage("""
						             Expected subject to
						             be serializable as JSON,
						             but it could not be deserialized: Each parameter in the deserialization constructor on type 'aweXpect.Tests.TestHelpers.Models.PocoWithoutDefaultConstructor' must bind to an object property or field on deserialization. Each parameter name must match with a property or field on the object*
						             """).AsWildcard();
				}

				[Theory]
				[InlineData(true)]
				[InlineData(false)]
				public async Task WhenSubjectHasNoDefaultFieldConstructor_ShouldFailUnlessIncludeFieldsIsSet(
					bool includeFields)
				{
					PocoWithoutDefaultFieldConstructor subject = new(12);

					async Task Act()
						=> await That(subject).Is().JsonSerializable(new JsonSerializerOptions
						{
							IncludeFields = includeFields
						});

					await That(Act).Does().Throw<XunitException>().OnlyIf(!includeFields)
						.WithMessage("""
						             Expected subject to
						             be serializable as JSON,
						             but it could not be deserialized: Each parameter in the deserialization constructor on type 'aweXpect.Tests.TestHelpers.Models.PocoWithoutDefaultFieldConstructor' must bind to an object property or field on deserialization. Each parameter name must match with a property or field on the object. Fields are only considered when 'JsonSerializerOptions.IncludeFields' is enabled*
						             """).AsWildcard();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					object? subject = null;

					async Task Act()
						=> await That(subject).Is().JsonSerializable();

					await That(Act).Does().Throw<XunitException>()
						.WithMessage("""
						             Expected subject to
						             be serializable as JSON,
						             but it was <null>
						             """);
				}

				[Fact]
				public async Task WhenSubjectIsPoco_ShouldSucceed()
				{
					SimplePocoWithPrimitiveTypes subject = new()
					{
						Id = 1,
						GlobalId = Guid.NewGuid(),
						Name = "foo",
						DateOfBirth = DateTime.Today,
						Height = new decimal(4.3),
						Weight = 5.6,
						ShoeSize = 7.8f,
						IsActive = true,
						Image = [8, 9, 10, 11],
						Category = 'a'
					};

					async Task Act()
						=> await That(subject).Is().JsonSerializable();

					await That(Act).Does().NotThrow();
				}
			}

			public sealed class GenericTests
			{
				[Fact]
				public async Task WhenSubjectHasAnIgnoredProperty_ShouldFail()
				{
					PocoWithIgnoredProperty subject = new()
					{
						Id = 2,
						Name = "foo"
					};

					async Task Act()
						=> await That(subject).Is().JsonSerializable<PocoWithIgnoredProperty>();

					await That(Act).Does().Throw<XunitException>()
						.WithMessage("""
						             Expected subject to
						             be serializable as PocoWithIgnoredProperty JSON,
						             but Property Name did not match:
						               Expected: "foo"
						               Received: <null>
						             """);
				}

				[Fact]
				public async Task WhenSubjectHasAnIgnoredProperty_WhenPropertyIsIgnored_ShouldSucceed()
				{
					PocoWithIgnoredProperty subject = new()
					{
						Id = 2,
						Name = "foo"
					};

					async Task Act()
						=> await That(subject).Is()
							.JsonSerializable<PocoWithIgnoredProperty>(o => o.IgnoringMember("Name"));

					await That(Act).Does().NotThrow();
				}

				[Fact]
				public async Task WhenSubjectHasAPrivateConstructor_ShouldFail()
				{
					PocoWithPrivateConstructor subject = PocoWithPrivateConstructor.Create(42);

					async Task Act()
						=> await That(subject).Is().JsonSerializable<PocoWithPrivateConstructor>();

					await That(Act).Does().Throw<XunitException>()
						.WithMessage("""
						             Expected subject to
						             be serializable as PocoWithPrivateConstructor JSON,
						             but it could not be deserialized: Deserialization of types without a parameterless constructor, a singular parameterized constructor, or a parameterized constructor annotated with 'JsonConstructorAttribute' is not supported. Type 'aweXpect.Tests.TestHelpers.Models.PocoWithPrivateConstructor'*
						             """).AsWildcard();
				}

				[Fact]
				public async Task WhenSubjectHasAPrivateConstructorWithJsonConstructorAttribute_ShouldSucceed()
				{
					PocoWithPrivateConstructorWithJsonConstructorAttribute subject =
						PocoWithPrivateConstructorWithJsonConstructorAttribute.Create(42);

					async Task Act()
						=> await That(subject).Is()
							.JsonSerializable<PocoWithPrivateConstructorWithJsonConstructorAttribute>();

					await That(Act).Does().NotThrow();
				}

				[Fact]
				public async Task WhenSubjectHasNoDefaultConstructor_ShouldFail()
				{
					PocoWithoutDefaultConstructor subject = new(12);

					async Task Act()
						=> await That(subject).Is().JsonSerializable<PocoWithoutDefaultConstructor>();

					await That(Act).Does().Throw<XunitException>()
						.WithMessage("""
						             Expected subject to
						             be serializable as PocoWithoutDefaultConstructor JSON,
						             but it could not be deserialized: Each parameter in the deserialization constructor on type 'aweXpect.Tests.TestHelpers.Models.PocoWithoutDefaultConstructor' must bind to an object property or field on deserialization. Each parameter name must match with a property or field on the object*
						             """).AsWildcard();
				}

				[Theory]
				[InlineData(true)]
				[InlineData(false)]
				public async Task WhenSubjectHasNoDefaultFieldConstructor_ShouldFailUnlessIncludeFieldsIsSet(
					bool includeFields)
				{
					PocoWithoutDefaultFieldConstructor subject = new(12);

					async Task Act()
						=> await That(subject).Is().JsonSerializable<PocoWithoutDefaultFieldConstructor>(
							new JsonSerializerOptions
							{
								IncludeFields = includeFields
							});

					await That(Act).Does().Throw<XunitException>().OnlyIf(!includeFields)
						.WithMessage("""
						             Expected subject to
						             be serializable as PocoWithoutDefaultFieldConstructor JSON,
						             but it could not be deserialized: Each parameter in the deserialization constructor on type 'aweXpect.Tests.TestHelpers.Models.PocoWithoutDefaultFieldConstructor' must bind to an object property or field on deserialization. Each parameter name must match with a property or field on the object. Fields are only considered when 'JsonSerializerOptions.IncludeFields' is enabled*
						             """).AsWildcard();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					object? subject = null;

					async Task Act()
						=> await That(subject).Is().JsonSerializable<SimplePocoWithPrimitiveTypes>();

					await That(Act).Does().Throw<XunitException>()
						.WithMessage("""
						             Expected subject to
						             be serializable as SimplePocoWithPrimitiveTypes JSON,
						             but it was <null>
						             """);
				}

				[Fact]
				public async Task WhenTypeDoesNotMatch_ShouldFail()
				{
					SimplePocoWithPrimitiveTypes subject = new()
					{
						Id = 1,
						GlobalId = Guid.NewGuid(),
						Name = "foo",
						DateOfBirth = DateTime.Today,
						Height = new decimal(4.3),
						Weight = 5.6,
						ShoeSize = 7.8f,
						IsActive = true,
						Image = [8, 9, 10, 11],
						Category = 'a'
					};

					async Task Act()
						=> await That(subject).Is().JsonSerializable<PocoWithIgnoredProperty>();

					await That(Act).Does().Throw<XunitException>()
						.WithMessage("""
						             Expected subject to
						             be serializable as PocoWithIgnoredProperty JSON,
						             but it was not assignable to PocoWithIgnoredProperty
						             """);
				}

				[Fact]
				public async Task WhenTypeMatches_ShouldSucceed()
				{
					SimplePocoWithPrimitiveTypes subject = new()
					{
						Id = 1,
						GlobalId = Guid.NewGuid(),
						Name = "foo",
						DateOfBirth = DateTime.Today,
						Height = new decimal(4.3),
						Weight = 5.6,
						ShoeSize = 7.8f,
						IsActive = true,
						Image = [8, 9, 10, 11],
						Category = 'a'
					};

					async Task Act()
						=> await That(subject).Is().JsonSerializable<SimplePocoWithPrimitiveTypes>();

					await That(Act).Does().NotThrow();
				}
			}
		}
	}
}
#endif

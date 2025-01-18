#if NET8_0_OR_GREATER
using System;
using System.Text.Json;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Equivalency;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatObject
{
	/// <summary>
	///     Verifies that the subject can be serialized as JSON.
	/// </summary>
	public static AndOrResult<object?, IExpectSubject<object?>> JsonSerializable(
		this IThatIs<object?> source,
		Func<EquivalencyOptions, EquivalencyOptions>? equivalencyOptions = null)
		=> new(
			source.ExpectationBuilder.AddConstraint(it
				=> new BeJsonSerializableConstraint<object>(it, new JsonSerializerOptions(),
					EquivalencyOptions.FromCallback(equivalencyOptions))),
			source.ExpectSubject());

	/// <summary>
	///     Verifies that the subject can be serialized as JSON.
	/// </summary>
	public static AndOrResult<object?, IExpectSubject<object?>> JsonSerializable(
		this IThatIs<object?> source,
		JsonSerializerOptions serializerOptions,
		Func<EquivalencyOptions, EquivalencyOptions>? equivalencyOptions = null)
		=> new(
			source.ExpectationBuilder.AddConstraint(it
				=> new BeJsonSerializableConstraint<object>(it, serializerOptions,
					EquivalencyOptions.FromCallback(equivalencyOptions))),
			source.ExpectSubject());

	/// <summary>
	///     Verifies that the subject can be serialized as JSON of type <typeparamref name="T" />.
	/// </summary>
	public static AndOrResult<object?, IExpectSubject<object?>> JsonSerializable<T>(
		this IThatIs<object?> source,
		Func<EquivalencyOptions, EquivalencyOptions>? equivalencyOptions = null)
		=> new(
			source.ExpectationBuilder.AddConstraint(it
				=> new BeJsonSerializableConstraint<T>(it, new JsonSerializerOptions(),
					EquivalencyOptions.FromCallback(equivalencyOptions))),
			source.ExpectSubject());

	/// <summary>
	///     Verifies that the subject can be serialized as JSON of type <typeparamref name="T" />.
	/// </summary>
	public static AndOrResult<object?, IExpectSubject<object?>> JsonSerializable<T>(
		this IThatIs<object?> source,
		JsonSerializerOptions serializerOptions,
		Func<EquivalencyOptions, EquivalencyOptions>? equivalencyOptions = null)
		=> new(
			source.ExpectationBuilder.AddConstraint(it
				=> new BeJsonSerializableConstraint<T>(it, serializerOptions,
					EquivalencyOptions.FromCallback(equivalencyOptions))),
			source.ExpectSubject());

	private readonly struct BeJsonSerializableConstraint<T>(
		string it,
		JsonSerializerOptions serializerOptions,
		EquivalencyOptions options)
		: IValueConstraint<object?>
	{
		public ConstraintResult IsMetBy(object? actual)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure<T?>(default, ToString(), $"{it} was <null>");
			}

			if (actual is not T typedSubject)
			{
				return new ConstraintResult.Failure<T?>(default, ToString(),
					$"{it} was not assignable to {Formatter.Format(typeof(T))}");
			}

			object? deserializedObject;
			try
			{
				string serializedObject = JsonSerializer.Serialize(actual, serializerOptions);
				deserializedObject = JsonSerializer.Deserialize(serializedObject, actual.GetType(), serializerOptions);
			}
			catch (Exception e)
			{
				return new ConstraintResult.Failure<T?>(typedSubject, ToString(),
					$"{it} could not be deserialized: {e.Message}");
			}

			EquivalencyComparer equivalencyComparer = new(options);
			if (equivalencyComparer.AreConsideredEqual(deserializedObject, actual))
			{
				return new ConstraintResult.Success<T?>(typedSubject, ToString());
			}

			return new ConstraintResult.Failure<T?>(typedSubject, ToString(),
				equivalencyComparer.GetExtendedFailure(it, actual, deserializedObject));
		}

		public override string ToString()
			=> (typeof(T) == typeof(object)) switch
			{
				true => "be serializable as JSON",
				false => $"be serializable as {Formatter.Format(typeof(T))} JSON"
			};
	}
}
#endif

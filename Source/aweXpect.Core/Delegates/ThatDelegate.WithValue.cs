using System.Diagnostics.CodeAnalysis;
using System.Text;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.Sources;
using aweXpect.Results;

namespace aweXpect.Delegates;

public abstract partial class ThatDelegate
{
	/// <summary>
	///     A delegate with value of type <typeparamref name="T" />.
	/// </summary>
	public sealed class WithValue<T>(ExpectationBuilder expectationBuilder)
		: ThatDelegate(expectationBuilder),
			IThat<WithValue<T>>,
			IThatDoes<WithValue<T>>,
			IThatHas<WithValue<T>>,
			IThatIs<WithValue<T>>
	{
		/// <summary>
		///     Verifies that the delegate does not throw any exception.
		/// </summary>
		public ExpectationResult<T> DoesNotThrow()
			=> new(ExpectationBuilder.AddConstraint((it, grammars) =>
				new DoesNotThrowConstraint(it, grammars)));

		private sealed class DoesNotThrowConstraint(string it, ExpectationGrammars grammars)
			: ConstraintResult(grammars),
				IValueConstraint<DelegateValue<T>>
		{
			private DelegateValue<T>? _actual;
			private bool _isNegated;

			/// <inheritdoc />
			public ConstraintResult IsMetBy(DelegateValue<T> value)
			{
				_actual = value;
				if (value.IsNull)
				{
					Outcome = Outcome.Failure;
					return this;
				}

				Outcome = _isNegated == value.Exception is null ? Outcome.Failure : Outcome.Success;
				return this;
			}

			public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
			{
				if (!_isNegated)
				{
					stringBuilder.Append("does not throw any exception");
				}
				else
				{
					stringBuilder.Append("throws an exception");
				}
			}

			public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
			{
				if (_actual?.IsNull != false)
				{
					stringBuilder.Append(it).Append(" was <null>");
				}
				else
				{
					switch (_isNegated)
					{
						case true when _actual.Exception is null:
							stringBuilder.Append(it).Append(" did not throw any exception");
							break;
						case false when _actual.Exception is not null:
							stringBuilder.Append(it).Append(" did throw ");
							stringBuilder.Append(FormatForMessage(_actual.Exception));
							break;
					}
				}
			}

			public override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value) where TValue : default
			{
				if (_actual is { Value: TValue typedValue, })
				{
					value = typedValue;
					return true;
				}

				value = default;
				return typeof(TValue).IsAssignableFrom(typeof(T));
			}

			public override ConstraintResult Negate()
			{
				_isNegated = !_isNegated;
				return this;
			}
		}
	}
}

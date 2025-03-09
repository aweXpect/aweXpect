using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.Helpers;

namespace aweXpect.Results;

/// <summary>
///     Base class for expectation results.
/// </summary>
/// <remarks>
///     Create instances by using the static methods on the <see cref="Expect" /> class.
/// </remarks>
[StackTraceHidden]
public abstract class Expectation
{
	/// <summary>
	///     <i>Not supported!</i><br />
	///     <see cref="object.Equals(object?)" /> is not supported. Did you mean <c>IsEqualTo</c> instead?
	/// </summary>
	/// <remarks>
	///     Consider adding support for <see cref="EditorBrowsableAttribute" /> to hide this method from code suggestions.
	/// </remarks>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override bool Equals(object? obj)
		=> throw new NotSupportedException("Equals is not supported. Did you mean Is() instead?");

	/// <summary>
	///     <i>Not supported!</i><br />
	///     <see cref="object.GetHashCode()" /> is not supported.
	/// </summary>
	/// <remarks>
	///     Consider adding support for <see cref="EditorBrowsableAttribute" /> to hide this method from code suggestions.
	/// </remarks>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override int GetHashCode()
		=> throw new NotSupportedException("GetHashCode is not supported.");

	/// <summary>
	///     <i>Not supported!</i><br />
	///     <see cref="object.GetType()" /> is not supported.
	/// </summary>
	/// <remarks>
	///     Consider adding support for <see cref="EditorBrowsableAttribute" /> to hide this method from code suggestions.
	/// </remarks>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new Type GetType()
		=> base.GetType();

	/// <summary>
	///     <i>Not supported!</i><br />
	///     <see cref="object.ToString()" /> is not supported.
	/// </summary>
	/// <remarks>
	///     Consider adding support for <see cref="EditorBrowsableAttribute" /> to hide this method from code suggestions.
	/// </remarks>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override string? ToString()
		=> base.ToString();

	internal abstract Task<Result> GetResult(int index);

	internal abstract IEnumerable<ResultContext> GetContexts(int index);

	internal struct Result(int index, string subjectLine, ConstraintResult result)
	{
		public int Index { get; } = index;
		public string SubjectLine { get; } = subjectLine;
		public ConstraintResult ConstraintResult { get; } = result;
	}

	/// <summary>
	///     Combination of multiple expectations.
	/// </summary>
	public abstract class Combination : Expectation
	{
		private readonly Expectation[] _expectations;

		/// <summary>
		///     Combination of multiple expectations.
		/// </summary>
		protected Combination(Expectation[] expectations)
		{
			if (expectations.Length == 0)
			{
				throw new ArgumentException("You must provide at least one expectation.",
					nameof(expectations));
			}

			_expectations = expectations;
		}

		/// <summary>
		///     By awaiting the result, the expectations are verified.
		///     <para />
		///     Will throw an exception, when the expectations are not met.
		/// </summary>
		public TaskAwaiter GetAwaiter()
		{
			Task result = GetResultOrThrow();
			return result.GetAwaiter();
		}

		/// <summary>
		///     Returns the subject line of the <see cref="Expectation.Combination" />.
		/// </summary>
		protected abstract string GetSubjectLine();

		/// <summary>
		///     Specifies, if the combination should be treated as
		///     <see cref="Outcome.Success" /> or <see cref="Outcome.Failure" />
		/// </summary>
		protected abstract Outcome CheckOutcome(Outcome? previous, Outcome current);

		/// <inheritdoc />
		internal override async Task<Result> GetResult(int index)
		{
			StringBuilder expectationTexts = new();
			StringBuilder failureTexts = new();
			Outcome? outcome = null;
			foreach (Expectation? expectation in _expectations)
			{
				Result result = await expectation.GetResult(index);
				outcome = CheckOutcome(outcome, result.ConstraintResult.Outcome);
				index = result.Index;
				if (expectationTexts.Length > 0)
				{
					expectationTexts.AppendLine();
				}

				if (expectation is Combination)
				{
					expectationTexts.Append("  ").Append(result.SubjectLine).AppendLine().Append("  ");
					result.ConstraintResult.AppendExpectation(expectationTexts, "  ");
				}
				else
				{
					expectationTexts.Append(result.SubjectLine).Append(' ');
					result.ConstraintResult.AppendExpectation(expectationTexts, "      ");
				}

				if (result.ConstraintResult.Outcome == Outcome.Failure)
				{
					if (failureTexts.Length > 0)
					{
						failureTexts.AppendLine();
					}

					if (expectation is Combination)
					{
						failureTexts.Append("  ");
						result.ConstraintResult.AppendResult(failureTexts, "  ");
					}
					else
					{
						failureTexts.Append(" [").Append(index.ToString("00")).Append("] ");
						result.ConstraintResult.AppendResult(failureTexts, "      ");
					}
				}
			}

			if (outcome != Outcome.Success)
			{
				return new Result(index, GetSubjectLine(),
					new CombinationResult(Outcome.Failure, expectationTexts.ToString(), failureTexts.ToString()));
			}

			return new Result(index, GetSubjectLine(),
				new CombinationResult(Outcome.Success, expectationTexts.ToString()));
		}

		internal override IEnumerable<ResultContext> GetContexts(int index)
		{
			List<ResultContext> combinedContexts = new();
			foreach (Expectation expectation in _expectations)
			{
				if (expectation is Combination)
				{
					combinedContexts.AddRange(expectation.GetContexts(index));
				}
				else
				{
					foreach (ResultContext context in expectation.GetContexts(++index))
					{
						context.Title = $"[{index:00}] {context.Title}";
						combinedContexts.Add(context);
					}
				}
			}

			return combinedContexts;
		}

		private async Task GetResultOrThrow(CancellationToken cancellationToken = default)
		{
			Result result = await GetResult(0);
			if (result.ConstraintResult.Outcome == Outcome.Success)
			{
				return;
			}

			StringBuilder sb = new();
			sb.AppendLine(GetSubjectLine());
			result.ConstraintResult.AppendExpectation(sb);
			sb.AppendLine();
			sb.AppendLine("but");
			result.ConstraintResult.AppendResult(sb);
			foreach (ResultContext context in GetContexts(0))
			{
				string? content = await context.GetContent(cancellationToken);
				if (content is null)
				{
					continue;
				}

				sb.AppendLine().AppendLine();
				sb.Append(context.Title).Append(':').AppendLine();
				sb.Append(content);
			}

			Fail.Test(sb.ToString());
		}

		private sealed class CombinationResult : ConstraintResult
		{
			private readonly string _expectationTexts;
			private readonly string? _failureTexts;

			public CombinationResult(Outcome outcome, string expectationTexts, string? failureTexts = null) : base(
				ExpectationGrammars.None)
			{
				_expectationTexts = expectationTexts;
				_failureTexts = failureTexts;
				Outcome = outcome;
			}

			public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
				=> stringBuilder.Append(_expectationTexts.Indent(indentation, false));

			public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
			{
				if (_failureTexts != null)
				{
					stringBuilder.Append(_failureTexts.Indent(indentation, false));
				}
			}

			public override ConstraintResult Negate() => this;
		}

		/// <summary>
		///     All <paramref name="expectations" /> must be met.
		/// </summary>
		public class All(Expectation[] expectations) : Combination(expectations)
		{
			/// <inheritdoc />
			protected override string GetSubjectLine()
				=> "Expected all of the following to succeed:";

			/// <inheritdoc />
			protected override Outcome CheckOutcome(Outcome? previous, Outcome current)
				=> previous switch
				{
					Outcome.Failure => Outcome.Failure,
					_ => current,
				};
		}

		/// <summary>
		///     Any of the <paramref name="expectations" /> must be met.
		/// </summary>
		public class Any(Expectation[] expectations) : Combination(expectations)
		{
			/// <inheritdoc />
			protected override string GetSubjectLine()
				=> "Expected any of the following to succeed:";

			/// <inheritdoc />
			protected override Outcome CheckOutcome(Outcome? previous, Outcome current)
				=> previous switch
				{
					Outcome.Success => Outcome.Success,
					_ => current,
				};
		}
	}
}

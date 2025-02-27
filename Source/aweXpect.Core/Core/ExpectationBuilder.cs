using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core.Constraints;
using aweXpect.Core.Helpers;
using aweXpect.Core.Nodes;
using aweXpect.Core.Sources;
using aweXpect.Core.TimeSystem;
using aweXpect.Customization;

namespace aweXpect.Core;

/// <summary>
///     The builder for collecting all expectations.
/// </summary>
public abstract class ExpectationBuilder
{
	private const string DefaultCurrentSubject = "it";

	private CancellationToken? _cancellationToken;

	/// <summary>
	///     The current name for the subject (defaults to <see cref="DefaultCurrentSubject" />).
	/// </summary>
	private string _it = DefaultCurrentSubject;

	private Node _node = new ExpectationNode();
	private TimeSpan? _timeout;

	private ITimeSystem? _timeSystem;

	private Node? _whichNode;

	/// <summary>
	///     Initializes the <see cref="ExpectationBuilder" /> with the <paramref name="subjectExpression" />
	///     for the statement builder.
	/// </summary>
	protected ExpectationBuilder(string subjectExpression)
	{
		Subject = subjectExpression;
	}

	/// <summary>
	///     The expected grammatic form of the expectation text.
	/// </summary>
	public ExpectationGrammars ExpectationGrammars { get; private set; } = ExpectationGrammars.None;

	internal string Subject { get; }

	/// <summary>
	///     Adds the <see cref="IValueConstraint{TValue}" /> from the <paramref name="constraintBuilder" /> which verifies the
	///     underlying value.
	/// </summary>
	/// <remarks>
	///     The parameter passed to the <paramref name="constraintBuilder" /> is the current name for the subject (mostly
	///     "it").
	/// </remarks>
	public ExpectationBuilder AddConstraint<TValue>(
		Func<string, ExpectationGrammars, IValueConstraint<TValue>> constraintBuilder)
	{
		_node.AddConstraint(constraintBuilder(_it, ExpectationGrammars));
		return this;
	}

	/// <summary>
	///     Adds the <see cref="IContextConstraint{TValue}" /> from the <paramref name="constraintBuilder" /> which verifies
	///     the underlying value.
	/// </summary>
	/// <remarks>
	///     The parameter passed to the <paramref name="constraintBuilder" /> is the current name for the subject (mostly
	///     "it").
	/// </remarks>
	public ExpectationBuilder AddConstraint<TValue>(
		Func<string, ExpectationGrammars, IContextConstraint<TValue>> constraintBuilder)
	{
		_node.AddConstraint(constraintBuilder(_it, ExpectationGrammars));
		return this;
	}

	/// <summary>
	///     Adds the <see cref="IAsyncConstraint{TValue}" /> from the <paramref name="constraintBuilder" /> which verifies the
	///     underlying value.
	/// </summary>
	/// <remarks>
	///     The parameter passed to the <paramref name="constraintBuilder" /> is the current name for the subject (mostly
	///     "it").
	/// </remarks>
	public ExpectationBuilder AddConstraint<TValue>(
		Func<string, ExpectationGrammars, IAsyncConstraint<TValue>> constraintBuilder)
	{
		_node.AddConstraint(constraintBuilder(_it, ExpectationGrammars));
		return this;
	}

	/// <summary>
	///     Adds the <see cref="IAsyncContextConstraint{TValue}" /> from the <paramref name="constraintBuilder" /> which
	///     verifies the underlying value.
	/// </summary>
	/// <remarks>
	///     The parameter passed to the <paramref name="constraintBuilder" /> is the current name for the subject (mostly
	///     "it").
	/// </remarks>
	public ExpectationBuilder AddConstraint<TValue>(
		Func<string, ExpectationGrammars, IAsyncContextConstraint<TValue>> constraintBuilder)
	{
		_node.AddConstraint(constraintBuilder(_it, ExpectationGrammars));
		return this;
	}

	/// <summary>
	///     Specifies a constraint that applies to the member selected
	///     by the <paramref name="memberAccessor" />.
	/// </summary>
	public MemberExpectationBuilder<TSource, TTarget> ForMember<TSource, TTarget>(
		MemberAccessor<TSource, TTarget> memberAccessor,
		Action<MemberAccessor, StringBuilder>? expectationTextGenerator = null,
		bool replaceIt = true) =>
		new((expectationBuilderCallback, expectationGrammar, sourceConstraintCallback, contexts) =>
		{
			if (sourceConstraintCallback is not null)
			{
				IValueConstraint<TSource> constraint = sourceConstraintCallback.Invoke(_it);
				_node.AddConstraint(constraint);
			}

			Node root = _node;
			_node = _node.AddMapping(memberAccessor, expectationTextGenerator, contexts) ?? _node;
			if (replaceIt)
			{
				_it = memberAccessor.ToString().Trim();
			}

			ExpectationGrammars previousGrammars = ExpectationGrammars;
			ExpectationGrammars = expectationGrammar;
			expectationBuilderCallback.Invoke(this);
			ExpectationGrammars = previousGrammars;
			_node = root;
			if (replaceIt)
			{
				_it = DefaultCurrentSubject;
			}

			return this;
		});

	/// <summary>
	///     Specifies a constraint that applies to the member selected asynchronously
	///     by the <paramref name="memberAccessor" />.
	/// </summary>
	public MemberExpectationBuilder<TSource, TTarget> ForAsyncMember<TSource, TTarget>(
		MemberAccessor<TSource, Task<TTarget?>> memberAccessor,
		Action<MemberAccessor, StringBuilder>? expectationTextGenerator = null,
		bool replaceIt = true) =>
		new((expectationBuilderCallback, expectationGrammar, sourceConstraintCallback, context) =>
		{
			if (sourceConstraintCallback is not null)
			{
				IValueConstraint<TSource> constraint = sourceConstraintCallback.Invoke(_it);
				_node.AddConstraint(constraint);
			}

			Node root = _node;
			_node = _node.AddAsyncMapping(memberAccessor, expectationTextGenerator, context) ?? _node;
			if (replaceIt)
			{
				_it = memberAccessor.ToString().Trim();
			}

			ExpectationGrammars previousGrammars = ExpectationGrammars;
			ExpectationGrammars = expectationGrammar;
			expectationBuilderCallback.Invoke(this);
			ExpectationGrammars = previousGrammars;
			_node = root;
			if (replaceIt)
			{
				_it = DefaultCurrentSubject;
			}

			return this;
		});

	/// <inheritdoc />
	public override string? ToString()
		=> _node.ToString();

	/// <summary>
	///     Adds a <paramref name="cancellationToken" /> to be used by the constraints.
	/// </summary>
	public void WithCancellation(CancellationToken cancellationToken)
		=> _cancellationToken = cancellationToken;

	/// <summary>
	///     Adds a <paramref name="timeout" /> to be used by the constraints.
	/// </summary>
	public void WithTimeout(TimeSpan timeout)
		=> _timeout = timeout;

	/// <summary>
	///     Adds a <paramref name="reason" /> to the current expectation constraint.
	/// </summary>
	internal void AddReason(string reason)
	{
		BecauseReason becauseReason = new(reason);
		_node.SetReason(becauseReason);
	}

	/// <summary>
	///     Supports chaining for subsequent expectation constraints with the <paramref name="textSeparator" />.
	/// </summary>
	public ExpectationBuilder And(string textSeparator = " and ")
	{
		if (_node is AndNode andNode)
		{
			andNode.AddNode(new ExpectationNode(), textSeparator);
		}
		else if (_node is OrNode orNode)
		{
			AndNode newNode = new(orNode.Current);
			newNode.AddNode(new ExpectationNode(), textSeparator);
			orNode.Current = newNode;
		}
		else
		{
			AndNode newNode = new(_node);
			newNode.AddNode(new ExpectationNode(), textSeparator);
			_node = newNode;
		}

		return this;
	}

	/// <summary>
	///     Specifies a mapping to add expectations on the member from the <paramref name="memberAccessor" />.
	/// </summary>
	public ExpectationBuilder ForWhich<TSource, TTarget>(
		Func<TSource, TTarget?> memberAccessor,
		string? separator = null,
		string? replaceIt = null,
		ExpectationGrammars? expectationGrammar = null)
	{
		Node? parentNode = null;
		if (_node is not ExpectationNode e || !e.IsEmpty())
		{
			parentNode = _node;
			_node = new ExpectationNode();
		}

		if (replaceIt != null)
		{
			_it = replaceIt;
		}

		if (expectationGrammar != null)
		{
			ExpectationGrammars = expectationGrammar.Value;
		}

		_whichNode = new WhichNode<TSource, TTarget>(parentNode, memberAccessor, separator);
		return this;
	}

	/// <summary>
	///     Specifies a mapping to add expectations on the member from the <paramref name="asyncMemberAccessor" />.
	/// </summary>
	public ExpectationBuilder ForWhich<TSource, TTarget>(
		Func<TSource, Task<TTarget?>> asyncMemberAccessor,
		string? separator = null)
	{
		Node? parentNode = null;
		if (_node is not ExpectationNode e || !e.IsEmpty())
		{
			parentNode = _node;
			_node = new ExpectationNode();
		}

		_whichNode = new WhichNode<TSource, TTarget>(parentNode, asyncMemberAccessor, separator);
		return this;
	}

	/// <summary>
	///     Creates the exception message from the <paramref name="failure" />.
	/// </summary>
	internal static string FromFailure(string subject, ConstraintResult failure)
	{
		StringBuilder sb = new();
		sb.Append("Expected that ").Append(subject).AppendLine();
		failure.AppendExpectation(sb);
		sb.AppendLine(",");
		sb.Append("but ");
		failure.AppendResult(sb);
		foreach (ConstraintResult.Context context in failure.GetContexts()
			         .Distinct(ConstraintResult.Context.Comparer))
		{
			sb.AppendLine().AppendLine();
			sb.Append(context.Title).Append(':').AppendLine();
			sb.Append(context.Content);
		}

		return sb.ToString();
	}

	internal Node GetRootNode()
	{
		if (_whichNode != null)
		{
			_whichNode.AddNode(_node);
			_node = _whichNode;
			_whichNode = null;
		}

		return _node;
	}

	internal Task<ConstraintResult> IsMet()
	{
		EvaluationContext.EvaluationContext context = new();
		ITimeSystem timeSystem = _timeSystem ?? RealTimeSystem.Instance;
		TestCancellation? testCancellation = Customize.aweXpect.Settings().TestCancellation.Get();
		_cancellationToken ??= testCancellation?.CancellationTokenFactory?.Invoke() ?? CancellationToken.None;
		return IsMet(GetRootNode(), context, timeSystem, _timeout ?? testCancellation?.Timeout,
			_cancellationToken.Value);
	}

	internal abstract Task<ConstraintResult> IsMet(Node rootNode,
		EvaluationContext.EvaluationContext context,
		ITimeSystem timeSystem,
		TimeSpan? timeout,
		CancellationToken cancellationToken);

	internal void Or(string textSeparator = " or ")
	{
		if (_node is OrNode orNode)
		{
			orNode.AddNode(new ExpectationNode(), textSeparator);
			return;
		}

		OrNode newNode = new(_node);
		newNode.AddNode(new ExpectationNode(), textSeparator);
		_node = newNode;
	}

	/// <summary>
	///     Specifies a <see cref="ITimeSystem" /> to use for the expectation.
	/// </summary>
	internal void UseTimeSystem(ITimeSystem timeSystem)
		=> _timeSystem = timeSystem;

	/// <summary>
	///     Helper class to specify constraints on the selected <typeparamref name="TMember" />.
	/// </summary>
	public class MemberExpectationBuilder<TSource, TMember>
	{
		private readonly Func<
				Action<ExpectationBuilder>,
				ExpectationGrammars,
				Func<string, IValueConstraint<TSource>>?,
				Func<TSource?, Task<ConstraintResult.Context?[]>>?,
				ExpectationBuilder>
			_callback;

		private Func<string, IValueConstraint<TSource>>? _sourceConstraintBuilder;
		private Func<TSource?, Task<ConstraintResult.Context?[]>>? _contexts;

		internal MemberExpectationBuilder(Func<
				Action<ExpectationBuilder>,
				ExpectationGrammars,
				Func<string, IValueConstraint<TSource>>?,
				Func<TSource?, Task<ConstraintResult.Context?[]>>?,
				ExpectationBuilder>
			callback)
		{
			_callback = callback;
		}

		/// <summary>
		///     Add expectations for the current <typeparamref name="TMember" />.
		/// </summary>
		public ExpectationBuilder AddExpectations(
			Action<ExpectationBuilder> expectation,
			ExpectationGrammars expectationGrammars = ExpectationGrammars.None)
			=> _callback(expectation, expectationGrammars, _sourceConstraintBuilder, _contexts);

		/// <summary>
		///     Add a validation constraint for the current <typeparamref name="TSource" />.
		/// </summary>
		/// <remarks>
		///     The parameter passed to the <paramref name="constraintBuilder" /> is the current name for the subject (mostly
		///     "it").
		/// </remarks>
		public MemberExpectationBuilder<TSource, TMember> Validate(
			Func<string, IValueConstraint<TSource>> constraintBuilder)
		{
			_sourceConstraintBuilder = constraintBuilder;
			return this;
		}

		/// <summary>
		///     Add a <paramref name="context"/> for the current <typeparamref name="TSource" />.
		/// </summary>
		public MemberExpectationBuilder<TSource, TMember> AddContext(
			Func<TSource?, Task<ConstraintResult.Context>> context)
		{
			return AddContexts(async s => [await context(s),]);
		}

		/// <summary>
		///     Add a <paramref name="contexts"/> for the current <typeparamref name="TSource" />.
		/// </summary>
		public MemberExpectationBuilder<TSource, TMember> AddContexts(
			Func<TSource?, Task<ConstraintResult.Context?[]>> contexts)
		{
			_contexts = contexts;
			return this;
		}
	}
}

internal class ExpectationBuilder<TValue> : ExpectationBuilder
{
	/// <summary>
	///     The subject.
	/// </summary>
	private readonly IValueSource<TValue> _subjectSource;

	internal ExpectationBuilder(
		IValueSource<TValue> subjectSource,
		string subjectExpression)
		: base(subjectExpression)
	{
		_subjectSource = subjectSource;
	}

	/// <inheritdoc />
	internal override async Task<ConstraintResult> IsMet(Node rootNode,
		EvaluationContext.EvaluationContext context,
		ITimeSystem timeSystem,
		TimeSpan? timeout,
		CancellationToken cancellationToken)
	{
		if (timeout != null)
		{
			using CancellationTokenSource timeoutCts = CancellationTokenSource
				.CreateLinkedTokenSource(cancellationToken);
			timeoutCts.CancelAfter(timeout.Value);
			CancellationToken token = timeoutCts.Token;
			TValue dataWithTimeout = await _subjectSource.GetValue(timeSystem, token);
			return await rootNode.IsMetBy(dataWithTimeout, context, token);
		}

		TValue data = await _subjectSource.GetValue(timeSystem, cancellationToken);
		return await rootNode.IsMetBy(data, context, cancellationToken);
	}
}

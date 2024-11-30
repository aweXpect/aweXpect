using System;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core.Constraints;
using aweXpect.Core.Helpers;
using aweXpect.Core.Nodes;
using aweXpect.Core.Sources;
using aweXpect.Core.TimeSystem;

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
		Func<string, IValueConstraint<TValue>> constraintBuilder)
	{
		_node.AddConstraint(constraintBuilder(_it));
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
		Func<string, IContextConstraint<TValue>> constraintBuilder)
	{
		_node.AddConstraint(constraintBuilder(_it));
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
		Func<string, IAsyncConstraint<TValue>> constraintBuilder)
	{
		_node.AddConstraint(constraintBuilder(_it));
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
		Func<string, IAsyncContextConstraint<TValue>> constraintBuilder)
	{
		_node.AddConstraint(constraintBuilder(_it));
		return this;
	}

	/// <summary>
	///     Specifies a constraint that applies to the member selected
	///     by the <paramref name="memberAccessor" />.
	/// </summary>
	public MemberExpectationBuilder<TSource, TTarget> ForMember<TSource, TTarget>(
		MemberAccessor<TSource, TTarget?> memberAccessor,
		Func<MemberAccessor, string, string>? expectationTextGenerator = null,
		bool replaceIt = true) =>
		new((a, s, c) =>
		{
			if (s is not null)
			{
				IValueConstraint<TSource> constraint = s.Invoke(_it);
				And(" ");
				_node.AddConstraint(constraint);
			}

			Node root = _node;
			_node = _node.AddMapping(memberAccessor, expectationTextGenerator) ?? _node;
			if (replaceIt)
			{
				_it = memberAccessor.ToString().Trim();
			}

			if (c is not null)
			{
				_node.AddConstraint(c);
			}

			a.Invoke(this);
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
	///     Specifies a global mapping for the value to test.
	/// </summary>
	/// <remarks>
	///     Intended for mapping the <see cref="DelegateValue{TValue}" /> to an exception.
	/// </remarks>
	public ExpectationBuilder ForWhich<TSource, TTarget>(
		Func<TSource, TTarget?> whichAccessor)
	{
		_whichNode = new WhichNode<TSource, TTarget>(whichAccessor);
		return this;
	}

	/// <summary>
	///     Creates the exception message from the <paramref name="failure" />.
	/// </summary>
	internal static string FromFailure(string subject, ConstraintResult.Failure failure)
		=> $"""
		    Expected {subject} to
		    {failure.ExpectationText},
		    but {failure.ResultText}
		    """;

	internal Task<ConstraintResult> IsMet()
	{
		EvaluationContext.EvaluationContext context = new();
		return IsMet(GetRootNode(), context, _timeSystem ?? RealTimeSystem.Instance,
			_cancellationToken ?? CancellationToken.None);
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

	internal abstract Task<ConstraintResult> IsMet(
		Node rootNode,
		EvaluationContext.EvaluationContext context,
		ITimeSystem timeSystem,
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
		private readonly Func<Action<ExpectationBuilder>,
				Func<string, IValueConstraint<TSource>>?,
				IValueConstraint<TMember>?,
				ExpectationBuilder>
			_callback;

		private readonly IValueConstraint<TMember>? _constraint = null;

		private Func<string, IValueConstraint<TSource>>? _sourceConstraintBuilder;

		internal MemberExpectationBuilder(Func<Action<ExpectationBuilder>,
				Func<string, IValueConstraint<TSource>>?,
				IValueConstraint<TMember>?,
				ExpectationBuilder>
			callback)
		{
			_callback = callback;
		}

		/// <summary>
		///     Add expectations for the current <typeparamref name="TMember" />.
		/// </summary>
		public ExpectationBuilder AddExpectations(Action<ExpectationBuilder> expectation)
			=> _callback(expectation, _sourceConstraintBuilder, _constraint);

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
	internal override async Task<ConstraintResult> IsMet(
		Node rootNode,
		EvaluationContext.EvaluationContext context,
		ITimeSystem timeSystem,
		CancellationToken cancellationToken)
	{
		TValue? data = await _subjectSource.GetValue(timeSystem, cancellationToken);
		return await rootNode.IsMetBy(data, context, cancellationToken);
	}
}

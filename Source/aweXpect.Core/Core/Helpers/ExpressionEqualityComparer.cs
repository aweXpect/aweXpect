using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace aweXpect.Core.Helpers;

[ExcludeFromCodeCoverage]
internal class ExpressionEqualityComparer<TSource, TTarget> : IEqualityComparer<Expression<Func<TSource, TTarget>>>
{
	private readonly ExpressionComparison _comparison = new();

	public bool Equals(Expression<Func<TSource, TTarget>>? a, Expression<Func<TSource, TTarget>>? b)
		=> _comparison.Compare(a, b);

	public int GetHashCode(Expression<Func<TSource, TTarget>>? expression)
		=> new HashCodeCalculation().GetHashCode(expression);

	private sealed class HashCodeCalculation : ExpressionVisitor
	{
		private int _runningTotal;

		public int GetHashCode(Expression? expression)
		{
			Visit(expression);
			return _runningTotal;
		}

		public override Expression? Visit(Expression? node)
		{
			if (null == node)
			{
				return null;
			}

			_runningTotal += GetHashCodeFor(node.NodeType, node.Type);
			return base.Visit(node);
		}

		protected override Expression VisitBinary(BinaryExpression node)
		{
			_runningTotal += GetHashCodeFor(node.Method, node.IsLifted, node.IsLiftedToNull);
			return base.VisitBinary(node);
		}

		protected override CatchBlock VisitCatchBlock(CatchBlock node)
		{
			_runningTotal += GetHashCodeFor(node.Test);
			return base.VisitCatchBlock(node);
		}

		protected override Expression VisitConstant(ConstantExpression node)
		{
			_runningTotal += GetHashCodeFor(node.Value);
			return base.VisitConstant(node);
		}

		protected override Expression VisitDebugInfo(DebugInfoExpression node)
		{
			_runningTotal +=
				GetHashCodeFor(node.Document, node.EndColumn, node.EndLine) +
				GetHashCodeFor(node.IsClear, node.StartLine, node.StartColumn);
			return base.VisitDebugInfo(node);
		}

		protected override Expression VisitDynamic(DynamicExpression node)
		{
			_runningTotal += GetHashCodeFor(node.DelegateType, node.Binder);
			return base.VisitDynamic(node);
		}

		protected override ElementInit VisitElementInit(ElementInit node)
		{
			_runningTotal += GetHashCodeFor(node.AddMethod);
			return base.VisitElementInit(node);
		}

		protected override Expression VisitGoto(GotoExpression node)
		{
			_runningTotal += GetHashCodeFor(node.Kind, node.Target);
			return base.VisitGoto(node);
		}

		protected override Expression VisitIndex(IndexExpression node)
		{
			_runningTotal += GetHashCodeFor(node.Indexer);
			return base.VisitIndex(node);
		}

		protected override LabelTarget? VisitLabelTarget(LabelTarget? node)
		{
			if (node is not null)
			{
				_runningTotal += GetHashCodeFor(node.Name, node.Type);
				return base.VisitLabelTarget(node);
			}

			return node;
		}

		protected override Expression VisitLambda<T>(Expression<T> node)
		{
			_runningTotal += GetHashCodeFor(node.Name, node.TailCall);
			return base.VisitLambda(node);
		}

		protected override Expression VisitListInit(ListInitExpression node)
		{
			_runningTotal += GetHashCodeFor(node.Initializers);
			return base.VisitListInit(node);
		}

		protected override Expression VisitLoop(LoopExpression node)
		{
			_runningTotal += GetHashCodeFor(node.BreakLabel, node.ContinueLabel);
			return base.VisitLoop(node);
		}

		protected override Expression VisitMember(MemberExpression node)
		{
			_runningTotal += GetHashCodeFor(node.Member);
			return base.VisitMember(node);
		}

		protected override MemberBinding VisitMemberBinding(MemberBinding node)
		{
			_runningTotal += GetHashCodeFor(node.BindingType, node.Member);
			return base.VisitMemberBinding(node);
		}

		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			_runningTotal += GetHashCodeFor(node.Method);
			return base.VisitMethodCall(node);
		}

		protected override Expression VisitNew(NewExpression node)
		{
			_runningTotal += GetHashCodeFor(node.Constructor, node.Members);
			return base.VisitNew(node);
		}

		protected override Expression VisitParameter(ParameterExpression node)
		{
			_runningTotal += GetHashCodeFor(node.IsByRef);
			return base.VisitParameter(node);
		}

		protected override Expression VisitSwitch(SwitchExpression node)
		{
			_runningTotal += GetHashCodeFor(node.Comparison);
			return base.VisitSwitch(node);
		}

		protected override Expression VisitTry(TryExpression node)
		{
			_runningTotal += GetHashCodeFor(node.Handlers);
			return base.VisitTry(node);
		}

		protected override Expression VisitTypeBinary(TypeBinaryExpression node)
		{
			_runningTotal += GetHashCodeFor(node.TypeOperand);
			return base.VisitTypeBinary(node);
		}

		protected override Expression VisitUnary(UnaryExpression node)
		{
			_runningTotal += GetHashCodeFor(node.Method, node.IsLifted, node.IsLiftedToNull);
			return base.VisitUnary(node);
		}

		private static int GetHashCodeFor<TProperty>(TProperty prop)
		{
			int hash = 17;
			if (prop is not null)
			{
				hash = (hash * 23) + prop.GetHashCode();
			}

			return hash;
		}

		private static int GetHashCodeFor<TProperty1, TProperty2>(TProperty1 prop1, TProperty2 prop2)
		{
			int hash = 17;
			if (prop1 is not null)
			{
				hash = (hash * 23) + prop1.GetHashCode();
			}

			if (prop2 is not null)
			{
				hash = (hash * 23) + prop2.GetHashCode();
			}

			return hash;
		}

		private static int GetHashCodeFor<TProperty1, TProperty2, TProperty3>(TProperty1 prop1, TProperty2 prop2,
			TProperty3 prop3)
		{
			int hash = 17;
			if (prop1 is not null)
			{
				hash = (hash * 23) + prop1.GetHashCode();
			}

			if (prop2 is not null)
			{
				hash = (hash * 23) + prop2.GetHashCode();
			}

			if (prop3 is not null)
			{
				hash = (hash * 23) + prop3.GetHashCode();
			}

			return hash;
		}
	}

	private sealed class ExpressionComparison : ExpressionVisitor
	{
		private bool _areEqual = true;
		private Expression? _current;
		private Queue<Expression> _tracked = [];

		public bool Compare(Expression? a, Expression? b)
		{
			_tracked = new Queue<Expression>(new ExpressionEnumeration(b));

			Visit(a);

			return _areEqual;
		}

		[return: NotNullIfNotNull(nameof(node))]
		public override Expression? Visit(Expression? node)
		{
			if (!_areEqual)
			{
				return node;
			}

			if (node == null || _tracked.Count == 0)
			{
				_areEqual = false;
				return node;
			}

			Expression peeked = _tracked.Peek();
			if (peeked.NodeType != node.NodeType || peeked.Type != node.Type)
			{
				_areEqual = false;
				return node;
			}

			_current = _tracked.Dequeue();
			return base.Visit(node);
		}

		protected override Expression VisitBinary(BinaryExpression node)
		{
			if (_current is BinaryExpression other)
			{
				_areEqual &= AreEqual(node, other, b => b.Method, b => b.IsLifted, b => b.IsLiftedToNull);
				return _areEqual ? base.VisitBinary(node) : node;
			}

			return node;
		}

		protected override Expression VisitConstant(ConstantExpression node)
		{
			if (_current is ConstantExpression other)
			{
				_areEqual &= AreEqual(node, other, x => x.Value);
				return _areEqual ? base.VisitConstant(node) : node;
			}

			return node;
		}

		protected override Expression VisitDebugInfo(DebugInfoExpression node)
		{
			if (_current is DebugInfoExpression other)
			{
				_areEqual &= AreEqual(node, other, x => x.Document, x => x.EndColumn, x => x.EndLine) &&
				             AreEqual(node, other, x => x.IsClear, x => x.StartLine, x => x.StartColumn);
				return _areEqual ? base.VisitDebugInfo(node) : node;
			}

			return node;
		}

		protected override Expression VisitDynamic(DynamicExpression node)
		{
			if (_current is DynamicExpression other)
			{
				_areEqual &= AreEqual(node, other, x => x.DelegateType, x => x.Binder);
				return _areEqual ? base.VisitDynamic(node) : node;
			}

			return node;
		}

		protected override Expression VisitGoto(GotoExpression node)
		{
			if (_current is GotoExpression other)
			{
				_areEqual &= AreEqual(node, other, x => x.Kind, x => x.Target);
				return _areEqual ? base.VisitGoto(node) : node;
			}

			return node;
		}

		protected override Expression VisitIndex(IndexExpression node)
		{
			if (_current is IndexExpression other)
			{
				_areEqual &= AreEqual(node, other, x => x.Indexer);
				return _areEqual ? base.VisitIndex(node) : node;
			}

			return node;
		}

		protected override Expression VisitLabel(LabelExpression node)
		{
			if (_current is LabelExpression other)
			{
				_areEqual &= AreEqual(node, other, x => x.Target);
				return _areEqual ? base.VisitLabel(node) : node;
			}

			return node;
		}

		protected override Expression VisitLambda<T>(Expression<T> node)
		{
			if (_current is LambdaExpression other)
			{
				_areEqual &= AreEqual(node, other, x => x.Name, x => x.TailCall);
				return _areEqual ? base.VisitLambda(node) : node;
			}

			return node;
		}

		protected override Expression VisitListInit(ListInitExpression node)
		{
			if (_current is ListInitExpression other)
			{
				_areEqual &= AreEqual(node, other, x => x.Initializers);
				return _areEqual ? base.VisitListInit(node) : node;
			}

			return node;
		}

		protected override Expression VisitLoop(LoopExpression node)
		{
			if (_current is LoopExpression other)
			{
				_areEqual &= AreEqual(node, other, x => x.BreakLabel, x => x.ContinueLabel);
				return _areEqual ? base.VisitLoop(node) : node;
			}

			return node;
		}

		protected override Expression VisitMember(MemberExpression node)
		{
			if (_current is MemberExpression other)
			{
				_areEqual &= AreEqual(node, other, x => x.Member);
				return _areEqual ? base.VisitMember(node) : node;
			}

			return node;
		}

		protected override Expression VisitMemberInit(MemberInitExpression node)
		{
			if (_current is MemberInitExpression other)
			{
				_areEqual &= AreEqual(node, other, x => x.Bindings);
				return _areEqual ? base.VisitMemberInit(node) : node;
			}

			return node;
		}

		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			if (_current is MethodCallExpression other)
			{
				_areEqual &= AreEqual(node, other, x => x.Method);
				return _areEqual ? base.VisitMethodCall(node) : node;
			}

			return node;
		}

		protected override Expression VisitNew(NewExpression node)
		{
			if (_current is NewExpression other)
			{
				_areEqual &= AreEqual(node, other, x => x.Constructor, x => x.Members);
				return _areEqual ? base.VisitNew(node) : node;
			}

			return node;
		}

		protected override Expression VisitSwitch(SwitchExpression node)
		{
			if (_current is SwitchExpression other)
			{
				_areEqual &= AreEqual(node, other, x => x.Comparison);
				return _areEqual ? base.VisitSwitch(node) : node;
			}

			return node;
		}

		protected override Expression VisitTry(TryExpression node)
		{
			if (_current is TryExpression other)
			{
				_areEqual &= AreEqual(node, other, x => x.Handlers);
				return _areEqual ? base.VisitTry(node) : node;
			}

			return node;
		}

		protected override Expression VisitTypeBinary(TypeBinaryExpression node)
		{
			if (_current is TypeBinaryExpression other)
			{
				_areEqual &= AreEqual(node, other, x => x.TypeOperand);
				return _areEqual ? base.VisitTypeBinary(node) : node;
			}

			return node;
		}

		protected override Expression VisitUnary(UnaryExpression node)
		{
			if (_current is UnaryExpression other)
			{
				_areEqual &= AreEqual(node, other, x => x.Method, x => x.IsLifted, x => x.IsLiftedToNull);
				return _areEqual ? base.VisitUnary(node) : node;
			}

			return node;
		}

		private static bool AreEqual<TExpression, TMember>(TExpression value, TExpression other,
			Func<TExpression, TMember?> reader)
			=> EqualityComparer<TMember>.Default.Equals(reader.Invoke(value), reader.Invoke(other));

		private static bool AreEqual<TExpression, TMember1, TMember2>(TExpression value, TExpression other,
			Func<TExpression, TMember1?> reader1,
			Func<TExpression, TMember2?> reader2)
			=> EqualityComparer<TMember1>.Default.Equals(reader1.Invoke(value), reader1.Invoke(other)) &&
			   EqualityComparer<TMember2>.Default.Equals(reader2.Invoke(value), reader2.Invoke(other));

		private static bool AreEqual<TExpression, TMember1, TMember2, TMember3>(TExpression value, TExpression other,
			Func<TExpression, TMember1?> reader1,
			Func<TExpression, TMember2?> reader2,
			Func<TExpression, TMember3?> reader3)
			=> EqualityComparer<TMember1>.Default.Equals(reader1.Invoke(value), reader1.Invoke(other)) &&
			   EqualityComparer<TMember2>.Default.Equals(reader2.Invoke(value), reader2.Invoke(other)) &&
			   EqualityComparer<TMember3>.Default.Equals(reader3.Invoke(value), reader3.Invoke(other));
	}

	private sealed class ExpressionEnumeration : ExpressionVisitor, IEnumerable<Expression>
	{
		private readonly List<Expression> _expressions = new();

		public ExpressionEnumeration(Expression? expression)
		{
			Visit(expression);
		}

		public IEnumerator<Expression> GetEnumerator() => _expressions.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public override Expression? Visit(Expression? node)
		{
			if (node == null)
			{
				return null;
			}

			_expressions.Add(node);
			return base.Visit(node);
		}
	}
}

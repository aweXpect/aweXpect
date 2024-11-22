using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using aweXpect.Core.Helpers;

namespace aweXpect.Core;

/// <summary>
///     The property accessor.
/// </summary>
public abstract class PropertyAccessor
{
	private readonly string _name;

	/// <summary>
	///     Creates a new property accessor.
	/// </summary>
	/// <param name="name"></param>
	protected PropertyAccessor(string name)
	{
		_name = name;
	}

	/// <inheritdoc />
	public override string ToString()
		=> _name;
}

/// <summary>
///     The property accessor from <typeparamref name="TSource" /> to <typeparamref name="TTarget" />.
/// </summary>
public class PropertyAccessor<TSource, TTarget> : PropertyAccessor
{
	private readonly Func<TSource, TTarget> _accessor;

	private PropertyAccessor(Func<TSource, TTarget> accessor, string name) :
		base(name)
	{
		_accessor = accessor;
	}

	/// <summary>
	///     Creates a property accessor from the given <paramref name="expression" />.
	/// </summary>
	public static PropertyAccessor<TSource, TTarget?> FromExpression(
		Expression<Func<TSource, TTarget?>> expression)
	{
		Func<TSource, TTarget?> compiled = expression.Compile();
		return new PropertyAccessor<TSource, TTarget?>(
			v => compiled(v),
			$"{ExpressionHelpers.GetPropertyPath(expression)} ");
	}

	/// <summary>
	///     Creates a property accessor from the given <paramref name="func" />.
	/// </summary>
	public static PropertyAccessor<TSource, TTarget?> FromFunc(
		Func<TSource, TTarget> func, string name)
		=> new(func, name);

	internal bool TryAccessProperty(TSource value,
		[NotNullWhen(true)] out TTarget? property)
	{
		property = _accessor.Invoke(value);
		return property is not null;
	}
}

using System;
using System.Linq.Expressions;
using aweXpect.Core.Helpers;

namespace aweXpect.Core;

/// <summary>
///     The member accessor.
/// </summary>
public abstract class MemberAccessor
{
	private readonly string _name;

	/// <summary>
	///     Creates a new member accessor.
	/// </summary>
	protected MemberAccessor(string name)
	{
		_name = name;
	}

	/// <inheritdoc />
	public override string ToString()
		=> _name;
}

/// <summary>
///     The member accessor from <typeparamref name="TSource" /> to <typeparamref name="TTarget" />.
/// </summary>
public class MemberAccessor<TSource, TTarget> : MemberAccessor
{
	private readonly Func<TSource, TTarget> _accessor;

	private MemberAccessor(Func<TSource, TTarget> accessor, string name) :
		base(name)
	{
		_accessor = accessor;
	}

	/// <summary>
	///     Creates a member accessor from the given <paramref name="expression" />.
	/// </summary>
	public static MemberAccessor<TSource, TTarget?> FromExpression(
		Expression<Func<TSource, TTarget?>> expression)
	{
		Func<TSource, TTarget?> compiled = expression.Compile();
		return new MemberAccessor<TSource, TTarget?>(
			v => compiled(v),
			$"{ExpressionHelpers.GetMemberPath(expression)} ");
	}

	/// <summary>
	///     Creates a member accessor from the given <paramref name="func" />.
	/// </summary>
	public static MemberAccessor<TSource, TTarget> FromFunc(
		Func<TSource, TTarget> func, string name)
		=> new(func, name);

	/// <summary>
	///     Creates a member accessor from the given <paramref name="func" />.
	/// </summary>
	public static MemberAccessor<TSource, TTarget> FromFuncAsMemberAccessor(
		Func<TSource, TTarget> func, string name)
		=> new(func, ExtractMemberPath(name));

	private static string ExtractMemberPath(string name)
	{
		// Example: "x => x.Foo" would result in ".Foo"
		int idx = name.IndexOf("=>", StringComparison.Ordinal);
		if (idx > 0)
		{
			string? prefix = name.Substring(0, idx).Trim();
			int idx2 = name.Substring(idx).IndexOf(prefix, StringComparison.Ordinal);
			if (idx2 > 0)
			{
				name = name.Substring(idx + idx2 + prefix.Length).TrimStart();
			}
		}

		return $"{name} ";
	}

	internal TTarget AccessMember(TSource value) => _accessor.Invoke(value);
}

using System;
using System.ComponentModel;

namespace Testably.Expectations.Core;

/// <summary>
///     Starting point for an expectation.
/// </summary>
public interface IExpectSubject<out T>
{
	/// <summary>
	///     <i>Not supported!</i><br />
	///     <see cref="object.Equals(object?)" /> is not supported. Did you mean <c>Be</c> instead?
	/// </summary>
	/// <remarks>
	///     Consider adding support for <see cref="EditorBrowsableAttribute" /> to hide this method from code suggestions.
	/// </remarks>
	[EditorBrowsable(EditorBrowsableState.Never)]
	bool Equals(object? obj);

	/// <summary>
	///     <i>Not supported!</i><br />
	///     <see cref="object.GetHashCode()" /> is not supported.
	/// </summary>
	/// <remarks>
	///     Consider adding support for <see cref="EditorBrowsableAttribute" /> to hide this method from code suggestions.
	/// </remarks>
	[EditorBrowsable(EditorBrowsableState.Never)]
	int GetHashCode();

	/// <summary>
	///     <i>Not supported!</i><br />
	///     <see cref="object.GetType()" /> is not supported.
	/// </summary>
	/// <remarks>
	///     Consider adding support for <see cref="EditorBrowsableAttribute" /> to hide this method from code suggestions.
	/// </remarks>
	[EditorBrowsable(EditorBrowsableState.Never)]
	Type GetType();

	/// <summary>
	///     Applies the <paramref name="builderOptions" /> to the <see cref="ExpectationBuilder" />.
	/// </summary>
	IThat<T> Should(Action<ExpectationBuilder> builderOptions);

	/// <summary>
	///     <i>Not supported!</i><br />
	///     <see cref="object.ToString()" /> is not supported.
	/// </summary>
	/// <remarks>
	///     Consider adding support for <see cref="EditorBrowsableAttribute" /> to hide this method from code suggestions.
	/// </remarks>
	[EditorBrowsable(EditorBrowsableState.Never)]
	string? ToString();
}

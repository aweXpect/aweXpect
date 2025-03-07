﻿using System;
using System.ComponentModel;

namespace aweXpect.Core;

/// <summary>
///     Base class for expectations, containing an <see cref="ExpectationBuilder" />.
/// </summary>
// ReSharper disable once UnusedTypeParameter
#pragma warning disable S2326 // 'T' is not used in the interface
public interface IThatVerb<out T>
{
	/// <summary>
	///     The expectation builder.<br />
	///     <b>This property is only intended for extensions.</b>
	/// </summary>
	/// <remarks>
	///     Consider adding support for <see cref="EditorBrowsableAttribute" /> with state set to
	///     <see cref="EditorBrowsableState.Advanced" /> to hide this method from code suggestions.
	/// </remarks>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	ExpectationBuilder ExpectationBuilder { get; }

	/// <summary>
	///     <i>Not supported!</i><br />
	///     <see cref="object.Equals(object?)" /> is not supported. Did you mean <c>IsEqualTo</c> instead?
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
	///     <i>Not supported!</i><br />
	///     <see cref="object.ToString()" /> is not supported.
	/// </summary>
	/// <remarks>
	///     Consider adding support for <see cref="EditorBrowsableAttribute" /> to hide this method from code suggestions.
	/// </remarks>
	[EditorBrowsable(EditorBrowsableState.Never)]
	string? ToString();
}
#pragma warning restore S2326 // 'T' is not used in the interface

﻿namespace aweXpect.Options;

public partial class ObjectEqualityOptions<TSubject>
{
	/// <summary>
	///     Compares the objects via <see cref="object.Equals(object, object)" />.
	/// </summary>
	public ObjectEqualityOptions<TSubject> Equals()
	{
		MatchType = ObjectEqualityOptions.EqualsMatch;
		return this;
	}
}

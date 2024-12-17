﻿using System.ComponentModel;
using aweXpect.Core;

namespace aweXpect;

public static partial class ThatGeneric
{
	/// <summary>
	///     Verifies that the subject triggers a <see cref="INotifyPropertyChanged.PropertyChanged"/> event.
	/// </summary>
	public static TriggerPropertyChangedParameterResult<T> TriggersPropertyChanged<T>(this IExpectSubject<T> subject)
		where T : INotifyPropertyChanged
	{
		IThat<T> should = subject.Should(_ => { });
		return new TriggerPropertyChangedParameterResult<T>(should, nameof(INotifyPropertyChanged.PropertyChanged));
	}
}

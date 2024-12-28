using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using aweXpect.Core;
using aweXpect.Options;
using aweXpect.Recording;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEventRecordingShould
{
	/// <summary>
	///     Verifies that the subject has triggered the <see cref="INotifyPropertyChanged.PropertyChanged" /> event
	///     for the property given by the <paramref name="propertyExpression" />
	/// </summary>
	public static EventTriggerResult<TSubject> HaveTriggeredPropertyChangedFor<TSubject, TProperty>(
		this IThat<IEventRecording<TSubject>> source,
		Expression<Func<TSubject, TProperty>> propertyExpression)
		where TSubject : INotifyPropertyChanged
	{
		MemberInfo? memberInfo =
			(((propertyExpression.Body as UnaryExpression)?.Operand ?? propertyExpression.Body) as MemberExpression)
			?.Member;
		string? propertyName = (memberInfo as PropertyInfo)?.Name;
		return HaveTriggeredPropertyChangedFor(source, propertyName);
	}

	/// <summary>
	///     Verifies that the subject has triggered the <see cref="INotifyPropertyChanged.PropertyChanged" /> event
	///     for the given <paramref name="propertyName" />
	/// </summary>
	public static EventTriggerResult<TSubject> HaveTriggeredPropertyChangedFor<TSubject>(
		this IThat<IEventRecording<TSubject>> source,
		string? propertyName)
		where TSubject : INotifyPropertyChanged
	{
		Quantifier quantifier = new();
		TriggerEventFilter filter = new();
		filter.AddPredicate(
			o => o.Length > 1 && o[1] is PropertyChangedEventArgs m && m.PropertyName == propertyName,
			$" for property {propertyName}");
		return new EventTriggerResult<TSubject>(
			source.ExpectationBuilder.AddConstraint(it
				=> new HaveTriggeredConstraint<TSubject>(it, nameof(INotifyPropertyChanged.PropertyChanged), filter,
					quantifier)),
			source,
			filter,
			quantifier);
	}

	/// <summary>
	///     Verifies that the subject has not triggered the <see cref="INotifyPropertyChanged.PropertyChanged" /> event
	///     for the property given by the <paramref name="propertyExpression" />
	/// </summary>
	public static EventTriggerResult<TSubject> NotHaveTriggeredPropertyChangedFor<TSubject, TProperty>(
		this IThat<IEventRecording<TSubject>> source,
		Expression<Func<TSubject, TProperty>> propertyExpression)
		where TSubject : INotifyPropertyChanged
	{
		MemberInfo? memberInfo =
			(((propertyExpression.Body as UnaryExpression)?.Operand ?? propertyExpression.Body) as MemberExpression)
			?.Member;
		string? propertyName = (memberInfo as PropertyInfo)?.Name;
		return NotHaveTriggeredPropertyChangedFor(source, propertyName);
	}

	/// <summary>
	///     Verifies that the subject has not triggered the <see cref="INotifyPropertyChanged.PropertyChanged" /> event
	///     for the given <paramref name="propertyName" />
	/// </summary>
	public static EventTriggerResult<TSubject> NotHaveTriggeredPropertyChangedFor<TSubject>(
		this IThat<IEventRecording<TSubject>> source,
		string? propertyName)
		where TSubject : INotifyPropertyChanged
	{
		Quantifier quantifier = new();
		quantifier.Exactly(0);
		TriggerEventFilter filter = new();
		filter.AddPredicate(
			o => o.Length > 1 && o[1] is PropertyChangedEventArgs m && m.PropertyName == propertyName,
			$" for property {propertyName}");
		return new EventTriggerResult<TSubject>(
			source.ExpectationBuilder.AddConstraint(it
				=> new HaveTriggeredConstraint<TSubject>(it, nameof(INotifyPropertyChanged.PropertyChanged), filter,
					quantifier)),
			source,
			filter,
			quantifier);
	}
}

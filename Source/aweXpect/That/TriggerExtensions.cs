using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using aweXpect.Core;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

/// <summary>
///     Extensions for common triggers.
/// </summary>
public static class TriggerExtensions
{
	/// <summary>
	///     Verifies that the subject triggers a <see cref="INotifyPropertyChanged.PropertyChanged" /> event.
	/// </summary>
	public static TriggerParameterResult<T> TriggersPropertyChanged<T>(this IExpectSubject<T> subject)
		where T : INotifyPropertyChanged
	{
		Quantifier quantifier = new();
		IThat<T> should = subject.Should(_ => { });
		return new TriggerParameterResult<T>(should.ExpectationBuilder, subject,
			nameof(INotifyPropertyChanged.PropertyChanged), quantifier);
	}

	/// <summary>
	///     Verifies that the subject triggers a <see cref="INotifyPropertyChanged.PropertyChanged" /> event.
	/// </summary>
	public static TriggerParameterResult<T> TriggersPropertyChangedFor<T>(this IExpectSubject<T> subject,
		Expression<Func<T, object>> propertyExpression)
		where T : INotifyPropertyChanged
	{
		MemberInfo? memberInfo =
			(((propertyExpression.Body as UnaryExpression)?.Operand ?? propertyExpression.Body) as MemberExpression)
			?.Member;
		string? propertyName = (memberInfo as PropertyInfo)?.Name;
		Quantifier quantifier = new();
		IThat<T> should = subject.Should(_ => { });
		return new TriggerParameterResult<T>(should.ExpectationBuilder, subject,
				nameof(INotifyPropertyChanged.PropertyChanged), quantifier)
			.WithParameter<PropertyChangedEventArgs>(
				$" for property {propertyName}",
				1,
				p => p.PropertyName == propertyName);
	}
	/// <summary>
	///     Verifies that the subject does not trigger a <see cref="INotifyPropertyChanged.PropertyChanged" /> event.
	/// </summary>
	public static TriggerParameterResult<T> DoesNotTriggerPropertyChanged<T>(this IExpectSubject<T> subject)
		where T : INotifyPropertyChanged
	{
		Quantifier quantifier = new();
		IThat<T> should = subject.Should(_ => { });
		return new TriggerParameterResult<T>(should.ExpectationBuilder, subject,
			nameof(INotifyPropertyChanged.PropertyChanged), quantifier)
			.Never();
	}

	/// <summary>
	///     Verifies that the subject does not trigger a <see cref="INotifyPropertyChanged.PropertyChanged" /> event.
	/// </summary>
	public static TriggerParameterResult<T> DoesNotTriggerPropertyChangedFor<T>(this IExpectSubject<T> subject,
		Expression<Func<T, object>> propertyExpression)
		where T : INotifyPropertyChanged
	{
		MemberInfo? memberInfo =
			(((propertyExpression.Body as UnaryExpression)?.Operand ?? propertyExpression.Body) as MemberExpression)
			?.Member;
		string? propertyName = (memberInfo as PropertyInfo)?.Name;
		Quantifier quantifier = new();
		IThat<T> should = subject.Should(_ => { });
		return new TriggerParameterResult<T>(should.ExpectationBuilder, subject,
				nameof(INotifyPropertyChanged.PropertyChanged), quantifier)
			.WithParameter<PropertyChangedEventArgs>(
				$" for property {propertyName}",
				1,
				p => p.PropertyName == propertyName)
			.Never();
	}
}

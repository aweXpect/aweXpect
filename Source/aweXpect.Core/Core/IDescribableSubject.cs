namespace aweXpect.Core;

/// <summary>
///     Allows overriding the subject string in the expectation message.
/// </summary>
public interface IDescribableSubject
{
	/// <summary>
	///     Get the description of the subject.
	/// </summary>
	string GetDescription();
}

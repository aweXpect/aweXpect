namespace aweXpect.Options;

/// <summary>
///     Options for limitations on a collection index.
/// </summary>
public class CollectionIndexOptions
{
	private int? _maximum;
	private int? _minimum;

	/// <summary>
	///     Checks if the <paramref name="index" /> is in range.
	/// </summary>
	/// <returns>
	///     <see langword="true" />, if the <paramref name="index" /> is in range, <see langword="null" />,
	///     if the <paramref name="index" /> is not in range, but could be in range for a larger index,
	///     otherwise <see langword="false" /> when the <paramref name="index" /> is not in range
	///     and will also not be in range for larger values.
	/// </returns>
	public bool? IsIndexInRange(int index)
	{
		if (_maximum.HasValue && index > _maximum)
		{
			return false;
		}

		if ((_minimum is null || index >= _minimum) &&
		    (_maximum is null || index <= _maximum))
		{
			return true;
		}

		return null;
	}

	/// <summary>
	///     Flag indicating, if only a single index is considered in range.
	/// </summary>
	public bool HasOnlySingleIndex()
		=> _maximum == _minimum && _minimum is not null;

	/// <summary>
	///     Set the checked index to be in range between <paramref name="minimum" /> and <paramref name="maximum" />.
	/// </summary>
	/// <remarks>When either parameter is set to <see langword="null" />, the corresponding range direction is unlimited.</remarks>
	public void SetIndexRange(int? minimum, int? maximum)
	{
		_minimum = minimum;
		_maximum = maximum;
	}

	/// <summary>
	///     Returns the description of the <see cref="CollectionIndexOptions" />.
	/// </summary>
	public string GetDescription()
	{
		if (_minimum is null && _maximum is null)
		{
			return "";
		}

		return _minimum == _maximum
			? $" at index {_minimum}"
			: $" with index between {_minimum} and {_maximum}";
	}
}

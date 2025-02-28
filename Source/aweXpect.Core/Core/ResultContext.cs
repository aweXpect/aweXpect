using System;
using System.Threading;
using System.Threading.Tasks;

namespace aweXpect.Core;

/// <summary>
///     A result context that is appended to a result error.
/// </summary>
public class ResultContext
{
	private readonly Func<CancellationToken, Task<string?>>? _contentFunc;

	private readonly string? _fixedContent;

	/// <summary>
	///     A result context that is appended to a result error.
	/// </summary>
	public ResultContext(string title, string? content)
	{
		Title = title;
		_fixedContent = content;
	}

	/// <summary>
	///     A result context that is appended to a result error.
	/// </summary>
	public ResultContext(string title, Func<CancellationToken, Task<string?>> asyncContent)
	{
		Title = title;
		_contentFunc = asyncContent;
	}

	/// <summary>
	///     The title of the context.
	/// </summary>
	public string Title { get; set; }

	/// <summary>
	///     The content of the context.
	/// </summary>
	public async Task<string?> GetContent(CancellationToken cancellationToken = default)
	{
		if (_fixedContent is not null)
		{
			return _fixedContent;
		}

		if (_contentFunc is not null)
		{
			return await _contentFunc(cancellationToken);
		}

		return null;
	}
}

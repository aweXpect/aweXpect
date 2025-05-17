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
	/// <remarks>The optional <paramref name="priority" /> determines the displayed order (higher values are displayed first).</remarks>
	public ResultContext(string title, string? content, int priority = 0)
	{
		Title = title;
		_fixedContent = content;
		Priority = priority;
	}

	/// <summary>
	///     A result context that is appended to a result error.
	/// </summary>
	/// <remarks>The optional <paramref name="priority" /> determines the displayed order (higher values are displayed first).</remarks>
	public ResultContext(string title, Func<CancellationToken, Task<string?>> asyncContent, int priority = 0)
	{
		Title = title;
		_contentFunc = asyncContent;
		Priority = priority;
	}

	/// <summary>
	///     A result context that is appended to a result error.
	/// </summary>
	/// <remarks>The optional <paramref name="priority" /> determines the displayed order (higher values are displayed first).</remarks>
	public ResultContext(string title, Func<string?> syncContent, int priority = 0)
	{
		Title = title;
		_contentFunc = _ => Task.FromResult(syncContent());
		Priority = priority;
	}

	/// <summary>
	///     The title of the context.
	/// </summary>
	public string Title { get; set; }

	/// <summary>
	///     The priority of the context (determines the displayed order).
	/// </summary>
	/// <remarks>The higher values are displayed first.</remarks>
	public int Priority { get; }

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

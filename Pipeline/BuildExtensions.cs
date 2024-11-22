using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Tools.SonarScanner;

namespace Build;

public static class BuildExtensions
{
	public static SonarScannerBeginSettings SetPullRequestOrBranchName(
		this SonarScannerBeginSettings settings,
		GitHubActions gitHubActions,
		GitVersion gitVersion)
	{
		if (gitHubActions?.IsPullRequest == true)
		{
			return settings
				.SetPullRequestKey(gitHubActions.PullRequestNumber.ToString())
				.SetPullRequestBranch(gitHubActions.Ref)
				.SetPullRequestBase(gitHubActions.BaseRef);
		}

		return settings.SetBranchName(gitVersion.BranchName);
	}
}

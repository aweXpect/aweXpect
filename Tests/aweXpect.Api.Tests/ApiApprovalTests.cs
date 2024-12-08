using System.Collections;
using NUnit.Framework;

namespace aweXpect.Api.Tests;

/// <summary>
///     Whenever a test fails, this means that the public API surface changed.
///     If the change was intentional, execute the <see cref="ApiAcceptance.AcceptApiChanges()" /> test to take over the
///     current public API surface. The changes will become part of the pull request and will be reviewed accordingly.
/// </summary>
public sealed class ApiApprovalTests
{
	[TestCaseSource(typeof(TargetFrameworksTheoryData))]
	public void VerifyPublicApiForAweXpect(string framework)
	{
		const string assemblyName = "aweXpect";

		string publicApi = Helper.CreatePublicApi(framework, assemblyName);
		string expectedApi = Helper.GetExpectedApi(framework, assemblyName);

		Assert.That(publicApi, Is.EqualTo(expectedApi));
	}

	[TestCaseSource(typeof(TargetFrameworksTheoryData))]
	public void VerifyPublicApiForAweXpectCore(string framework)
	{
		const string assemblyName = "aweXpect.Core";

		string publicApi = Helper.CreatePublicApi(framework, assemblyName);
		string expectedApi = Helper.GetExpectedApi(framework, assemblyName);

		Assert.That(publicApi, Is.EqualTo(expectedApi));
	}

	private sealed class TargetFrameworksTheoryData : IEnumerable
	{
		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			foreach (string targetFramework in Helper.GetTargetFrameworks())
			{
				yield return new object[] { targetFramework };
			}
		}

		#endregion
	}
}

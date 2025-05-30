﻿using System;
using System.Collections;
using System.Threading.Tasks;
using aweXpect.Customization;
using NUnit.Framework;

namespace aweXpect.Core.Api.Tests;

/// <summary>
///     Whenever a test fails, this means that the public API surface changed.
///     If the change was intentional, execute the <see cref="ApiAcceptance.AcceptApiChanges()" /> test to take over the
///     current public API surface. The changes will become part of the pull request and will be reviewed accordingly.
/// </summary>
public sealed class ApiApprovalTests : IDisposable
{
	private readonly CustomizationLifetime _settings = Customize.aweXpect
		.Formatting().MinimumNumberOfCharactersAfterStringDifference.Set(200);

	public void Dispose() => _settings.Dispose();

	[TestCaseSource(typeof(TargetFrameworksTheoryData))]
	public async Task VerifyPublicApiForAweXpectCore(string framework)
	{
		const string assemblyName = "aweXpect.Core";

		string publicApi = Helper.CreatePublicApi(framework, assemblyName);
		string expectedApi = Helper.GetExpectedApi(framework, assemblyName);

		await Expect.That(publicApi).IsEqualTo(expectedApi);
	}

	private sealed class TargetFrameworksTheoryData : IEnumerable
	{
		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			foreach (string targetFramework in Helper.GetTargetFrameworks())
			{
				yield return new object[]
				{
					targetFramework,
				};
			}
		}

		#endregion
	}
}

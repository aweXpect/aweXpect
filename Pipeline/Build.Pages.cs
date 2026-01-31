using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.IO;
using Serilog;

// ReSharper disable AllUnderscoreLocalParameterName

namespace Build;

partial class Build
{
	Target Pages => _ => _
		.Executes(async () =>
		{
			AbsolutePath mockolateDirectory = RootDirectory / "Docs" / "pages" / "docs" / "mockolate";
			mockolateDirectory.CreateOrCleanDirectory();
			await DownloadDocsPagesDirectory("Mockolate", mockolateDirectory);

			Dictionary<string, string> projects = new()
			{
				{
					"aweXpect.Json", "Json"
				},
				{
					"aweXpect.Web", "Web"
				},
				{
					"aweXpect.Reflection", "Reflection"
				},
				{
					"aweXpect.Testably", "Testably"
				},
				{
					"aweXpect.Mockolate", "Mockolate"
				},
			};
			foreach (var (project, directoryName) in projects)
			{
				await DownloadDocsPagesDirectory(project,
					RootDirectory / "Docs" / "pages" / "docs" / "extensions" / "project" / directoryName);
			}
		});

	async Task DownloadDocsPagesDirectory(string projectName, AbsolutePath baseDirectory)
	{
		Log.Information($"Store documentation from {projectName} under {baseDirectory}:");

		using HttpClient client = new();
		client.DefaultRequestHeaders.UserAgent.ParseAdd("aweXpect");
		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GithubToken);
		HttpResponseMessage response = await client.GetAsync(
			$"https://api.github.com/repos/aweXpect/{projectName}/contents/Docs/pages");

		string responseContent = await response.Content.ReadAsStringAsync();
		if (!response.IsSuccessStatusCode)
		{
			throw new InvalidOperationException(
				$"Could not find 'Docs/pages/static/js/data.js' in branch '{BenchmarkBranch}': {responseContent}");
		}

		try
		{
			HttpResponseMessage readmeResponse =
				await client.GetAsync(
					$"https://api.github.com/repos/aweXpect/{projectName}/contents/README.md");
			string readmeResponseContent = await readmeResponse.Content.ReadAsStringAsync();
			using JsonDocument readmeDocument = JsonDocument.Parse(readmeResponseContent);
			string readmeContent = Base64Decode(readmeDocument.RootElement.GetProperty("content").GetString());
			int indexOfFirstH2Header = readmeContent.IndexOf("\n##", StringComparison.Ordinal);
			if (indexOfFirstH2Header > 0)
			{
				readmeContent = readmeContent.Substring(indexOfFirstH2Header);
				Log.Information($"  Skip {indexOfFirstH2Header} characters in README.md");
			}
			else
			{
				readmeContent = string.Empty;
			}

			JsonDocument jsonDocument = JsonDocument.Parse(responseContent);
			foreach (JsonElement file in jsonDocument.RootElement.EnumerateArray())
			{
				await DownloadFileOrDirectory(client, projectName, "/", file, baseDirectory, (name, content) =>
				{
					if (name.StartsWith("00-") && content.Contains("{README}", StringComparison.OrdinalIgnoreCase))
					{
						content = content.Replace("{README}", readmeContent.Replace("Docs/pages/", "./"));
						readmeContent = string.Empty;
						Log.Information($"  Replace \"{{README}}\" with content from README.md for {name}");
					}
					return content;
				});
			}
		}
		catch (JsonException e)
		{
			Log.Error($"Could not parse JSON: {e.Message}\n{responseContent}");
		}
	}

	async Task DownloadFileOrDirectory(HttpClient client, string projectName, string subPath, JsonElement fileOrDirectory,
		AbsolutePath targetDirectory, [CanBeNull] Func<string, string, string> contentManipulator = null)
	{
		string name = fileOrDirectory.GetProperty("name").GetString()!;
		string filePath = targetDirectory / name;
		HttpResponseMessage fileResponse =
			await client.GetAsync(
				$"https://api.github.com/repos/aweXpect/{projectName}/contents/Docs/pages{subPath}{name}");
		string fileResponseContent = await fileResponse.Content.ReadAsStringAsync();
		using JsonDocument document = JsonDocument.Parse(fileResponseContent);
		if (document.RootElement.ValueKind == JsonValueKind.Array)
		{
			var subDirectory = targetDirectory / name;
			subDirectory.CreateDirectory();
			foreach (JsonElement subFileOrDirectory in document.RootElement.EnumerateArray())
			{
				await DownloadFileOrDirectory(client, projectName, subPath + name + "/", subFileOrDirectory, subDirectory);
			}
		}
		else
		{
			string content = Base64Decode(document.RootElement.GetProperty("content").GetString());
			if (contentManipulator is not null)
			{
				content = contentManipulator(name, content);
			}

			await File.WriteAllTextAsync(filePath, content);
			Log.Information($"  {name} under {filePath}");
		}
	}
}

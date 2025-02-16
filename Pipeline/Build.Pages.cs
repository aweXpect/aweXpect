using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
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
			Dictionary<string, string> projects = new()
			{
				{
					"aweXpect.Json", "Json"
				},
				{
					"aweXpect.Testably", "Testably"
				},
				{
					"aweXpect.Web", "Web"
				},
			};
			foreach (var (project, directoryName) in projects)
			{
				await DownloadDocsPagesDirectory(project, directoryName);
			}
		});


	async Task DownloadDocsPagesDirectory(string projectName, string directoryName)
	{
		AbsolutePath baseDirectory = RootDirectory / "Docs" / "pages" / "docs" / "extensions" / directoryName;
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
			JsonDocument jsonDocument = JsonDocument.Parse(responseContent);
			foreach (JsonElement file in jsonDocument.RootElement.EnumerateArray())
			{
				string name = file.GetProperty("name").GetString()!;
				string filePath = Path.Combine(baseDirectory, name);
				HttpResponseMessage fileResponse =
					await client.GetAsync(
						$"https://api.github.com/repos/aweXpect/{projectName}/contents/Docs/pages/{name}");
				string fileResponseContent = await fileResponse.Content.ReadAsStringAsync();
				using JsonDocument document = JsonDocument.Parse(fileResponseContent);
				string content = Base64Decode(document.RootElement.GetProperty("content").GetString());
				await File.WriteAllTextAsync(filePath, content);
				Log.Information($"  {name} under {filePath}");
			}
		}
		catch (JsonException e)
		{
			Log.Error($"Could not parse JSON: {e.Message}\n{responseContent}");
		}
	}
}

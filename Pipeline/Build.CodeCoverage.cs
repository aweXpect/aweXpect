using Nuke.Common;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.ReportGenerator;
using static Nuke.Common.Tools.ReportGenerator.ReportGeneratorTasks;
using static Serilog.Log;

// ReSharper disable AllUnderscoreLocalParameterName

namespace Build;

partial class Build
{
	Target CodeCoverage => _ => _
		.DependsOn(TestFrameworks)
		.DependsOn(UnitTests)
		.Executes(() =>
		{
			ReportGenerator(s => s
				.SetProcessToolPath(NuGetToolPathResolver.GetPackageExecutable("ReportGenerator", "ReportGenerator.dll",
					framework: "net8.0"))
				.SetTargetDirectory(TestResultsDirectory / "reports")
				.AddReports(TestResultsDirectory / "**/coverage.cobertura.xml")
				.AddReportTypes(ReportTypes.OpenCover)
				.AddFileFilters("-*.g.cs")
				.SetAssemblyFilters("+aweXpect*"));

			string link = TestResultsDirectory / "reports" / "index.html";
			Information($"Code coverage report: \x1b]8;;file://{link.Replace('\\', '/')}\x1b\\{link}\x1b]8;;\x1b\\");
		});
}

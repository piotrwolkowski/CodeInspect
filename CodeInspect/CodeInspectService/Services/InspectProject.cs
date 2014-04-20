using CodeInspectEntities;
using CodeInspectInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CodeInspectService.Services
{
    internal class InspectProject
    {

        internal static Task<Report> CreateReport(string projectPath, CancellationToken cancellationToken, IProgress<string> progress, ICodeInspectSettings codeInspectSettings)
        {
            TaskCompletionSource<Report> completedTask = new TaskCompletionSource<Report>();
            try
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    completedTask.TrySetCanceled();
                    return completedTask.Task;
                }

                if (projectPath.EndsWith("xml"))
                {
                    completedTask.TrySetResult(CreateReportFromXml(projectPath));
                }
                else
                {
                    return CreateReportFromProject(
                            codeInspectSettings.InspectCodePath,
                            projectPath,
                            Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), DateTime.Now.Ticks + ".xml"),
                            progress: progress);
                }
            }
            catch (Exception ex)
            {
                completedTask.TrySetException(ex);
            }

            return completedTask.Task;
        }

        /// <summary>
        /// Creates an error report using resharper command line tool.
        /// </summary>
        /// <param name="codeInspectionLocation">The location of the codeinspect.exe file.</param>
        /// <param name="solutionLocation">The location of a solution to examine.</param>
        /// <param name="xmlReportOutputLocation">The output location where the report in xml form will be created.</param>
        /// <param name="solutionWideAnalysis">The flag whether solution wide analysis should be applied.</param>
        /// <param name="treatWarningsAsErrors">The flag whether a warning should behave as an error.</param>
        /// <param name="cancellationToken">The cancellation token that allows to abort code inspection.</param>
        /// <param name="progress">The progress object that will receive messages created by codeinspect.exe</param>
        /// <returns>The task object of which result is a report object created for specified solution.</returns>
        private static Task<Report> CreateReportFromProject(
            string codeInspectionLocation,
            string solutionLocation,
            string xmlReportOutputLocation,
            bool solutionWideAnalysis = true,
            bool treatWarningsAsErrors = false,
            CancellationToken cancellationToken = new CancellationToken(),
            IProgress<string> progress = null)
        {
            Report result;
            TaskCompletionSource<Report> completedTask = new TaskCompletionSource<Report>();

            // Check if a solution file is specified.
            if (string.IsNullOrEmpty(solutionLocation))
            {
                completedTask.TrySetException(new ArgumentException("Solution location has to be specified"));
                return completedTask.Task;
            }
            // Check if the codeinspect.exe directory is specified.
            if (string.IsNullOrEmpty(codeInspectionLocation))
            {
                completedTask.TrySetException(new ArgumentException("Location of the codeinspect.exe executable has to be specified"));
                return completedTask.Task;
            }

            // Create process.
            Process codeInspectionProcess = new Process();
            codeInspectionProcess.StartInfo.CreateNoWindow = true;
            codeInspectionProcess.StartInfo.UseShellExecute = false;
            codeInspectionProcess.StartInfo.FileName = codeInspectionLocation;
            codeInspectionProcess.EnableRaisingEvents = true;

            // Build command arguments for the process.
            StringBuilder arguments = new StringBuilder();

            arguments.Append(string.Format(@"""{0}""", solutionLocation));
            arguments.Append(" ");

            if (treatWarningsAsErrors)
            {
                arguments.Append("/properties:TreatWarningsAsErrors=true");
                arguments.Append(" ");
            }

            arguments.Append(string.Format(@"/o=""{0}""", xmlReportOutputLocation));
            arguments.Append(" ");

            if (!solutionWideAnalysis)
            {
                arguments.Append("/no-swea");
                arguments.Append(" ");
            }

            codeInspectionProcess.StartInfo.Arguments = arguments.ToString();

            // Register handlers
            codeInspectionProcess.Exited += (s, e) =>
            {
                try
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        completedTask.TrySetCanceled();
                        return;
                    }

                    result = CreateReportFromXml(xmlReportOutputLocation);
                    if (result != null)
                    {
                        completedTask.TrySetResult(result);
                    }
                    codeInspectionProcess.Dispose();
                }
                catch (Exception ex)
                {
                    completedTask.TrySetException(ex);
                }
            };

            // Redirect standard output to receive messages.
            codeInspectionProcess.StartInfo.RedirectStandardOutput = true;
            codeInspectionProcess.OutputDataReceived += (s, e) =>
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    completedTask.TrySetCanceled();
                    return;
                }
                if (progress != null)
                {
                    progress.Report(e.Data);
                }
            };

            // Redirect standard error to receive messages.
            // codeinspect.exe sends most of its progress results to the standard error.
            codeInspectionProcess.StartInfo.RedirectStandardError = true;
            codeInspectionProcess.ErrorDataReceived += (s, e) =>
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    completedTask.TrySetCanceled();
                    return;
                }
                if (progress != null)
                {
                    progress.Report(e.Data);
                }
            };

            Task.Factory.StartNew(() =>
            {
                codeInspectionProcess.Start();

                cancellationToken.ThrowIfCancellationRequested();

                codeInspectionProcess.BeginOutputReadLine();
                codeInspectionProcess.BeginErrorReadLine();
                codeInspectionProcess.WaitForExit();
            }
            );
            return completedTask.Task;
        }

        /// <summary>
        /// Cretes a report object from the xml report file. The file must be an output of the resharper command line tool.
        /// </summary>
        /// <param name="xmlReportOutputLocation">The location of the xml report file.</param>
        /// <returns>The report object created from the xml report file.</returns>
        private static Report CreateReportFromXml(string xmlReportOutputLocation)
        {
            Report result = null;
            if (File.Exists(xmlReportOutputLocation))
            {
                using (StreamReader reader = new StreamReader(xmlReportOutputLocation))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Report));
                    result = (Report)serializer.Deserialize(reader);
                    result.AllIssues = new List<IssueWithDescription>(AllIssuesWithDescription(result));
                    reader.Close();
                }
            }
            return result;
        }

        private static IEnumerable<IssueWithDescription> AllIssuesWithDescription(Report report)
        {
            return report.ProjectIssues
                    .SelectMany(p => p.Issues
                        .Select(i =>
                            {
                                return new IssueWithDescription
                                (
                                    i,
                                    report.IssueTypes.Single(it => it.Id == i.TypeId),
                                    report.Information
                                );
                            }));
        }
    }
}

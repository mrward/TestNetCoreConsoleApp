using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.DotNet.ProjectModel;
using Microsoft.DotNet.InternalAbstractions;

namespace NetCoreBothConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                string projectJsonFileName = Path.GetFullPath("project.json");

                string projectDirectory = System.IO.Path.GetDirectoryName(projectJsonFileName);

                var workspace = new BuildWorkspace(ProjectReaderSettings.ReadFromEnvironment());
                var frameworkContexts = workspace.GetProjectContextCollection(projectDirectory)
                    .FrameworkOnlyContexts;

                var rids = RuntimeEnvironmentRidExtensions.GetAllCandidateRuntimeIdentifiers();

                foreach (var frameworkContext in frameworkContexts)
                {
                    var runtimeContext = workspace.GetRuntimeContext(frameworkContext, rids);

                    OutputPaths paths = runtimeContext.GetOutputPaths("Debug");
                    Console.WriteLine("TargetFramework: " + frameworkContext.Identity.TargetFramework);
                    Console.WriteLine("Executable: " + paths.RuntimeFiles.Executable);
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}

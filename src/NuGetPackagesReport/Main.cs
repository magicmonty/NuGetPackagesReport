using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using NuGetPackagesReport.Entities.NuGet;
using NuGetPackagesReport.Helpers;
using NuGetPackagesReport.NuGetService;

namespace NuGetPackagesReport
{
    public class Main
    {
        private readonly IReport _report;

        private readonly ILoader _loader;

        private readonly INuGetApiConnector _nuGetApiConnector;

        public Main()
            :this(new NuGetApiConnector(), new PackagesConfigFileLoader(), new ConfluenceReport())
        {
            //
        }

        public Main(INuGetApiConnector nuGetApiConnector, ILoader loader, IReport report)
        {
            this._nuGetApiConnector = nuGetApiConnector;
            this._loader = loader;
            this._report = report;
        }

        public void Execute(List<string> vsSolutionFolders)
        {
            Dictionary<string, List<Tuple<NuGetPackage, V2FeedPackage>>> packagesPerLocation =
                new Dictionary<string, List<Tuple<NuGetPackage, V2FeedPackage>>>();

            foreach (var vsSolutionFolder in vsSolutionFolders)
            {
                if (!Directory.Exists(vsSolutionFolder))
                {
                    Console.Error.WriteLine(string.Format("Path cannot be found '{0}'", vsSolutionFolder));
                    continue;
                }

                var nuGetPackageConfigs = _loader.LoadPackageConfigFiles(vsSolutionFolder);

                var unuquePackages = nuGetPackageConfigs.SelectMany(x => x.Packages).Distinct().ToList();

                var packagesInformation = _nuGetApiConnector.RetrieveInformationFromNuGet(unuquePackages);

                packagesPerLocation.Add(vsSolutionFolder, packagesInformation);
            }

            // Generate Summary report
            var distinctPackatesInAllLocations = packagesPerLocation.SelectMany(x => x.Value).Distinct().ToList();
            _report.Generate("Summary", distinctPackatesInAllLocations);

            // Generate report per location
            foreach (var location in packagesPerLocation.OrderBy(x => x.Key))
            {
                _report.Generate(String.Format("Project: '{0}'", location.Key), location.Value);
            }
        }
    }
}

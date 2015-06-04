using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

using NuGetPackagesReport.Entities.NuGet;

namespace NuGetPackagesReport.Helpers
{
    class PackagesConfigFileLoader : ILoader
    {
        /// <summary>
        /// Load and deserialize packages.config files
        /// </summary>
        /// <param name="basePath"></param>
        /// <returns></returns>
        public List<NuGetPackagesConfig> LoadPackageConfigFiles(string basePath)
        {
            var packagesConfigFiles = Directory.EnumerateFiles(basePath, "packages.config", SearchOption.AllDirectories);

            List<NuGetPackagesConfig> nuGetPackageConfigs = new List<NuGetPackagesConfig>();

            var nugetPackageFileSerializer = new XmlSerializer(typeof(NuGetPackagesConfig));

            foreach (var packagesConfigFile in packagesConfigFiles)
            {
                using (var stream = new FileStream(packagesConfigFile, FileMode.Open))
                {
                    var packages = nugetPackageFileSerializer.Deserialize(stream) as NuGetPackagesConfig;
                    nuGetPackageConfigs.Add(packages);
                }
            }

            return nuGetPackageConfigs;
        }
    }
}

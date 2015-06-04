using System.Collections.Generic;

using NuGetPackagesReport.Entities.NuGet;

namespace NuGetPackagesReport.Helpers
{
    public interface ILoader
    {
        List<NuGetPackagesConfig> LoadPackageConfigFiles(string basePath);
    }
}
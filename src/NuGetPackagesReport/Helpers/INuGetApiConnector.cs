using System;
using System.Collections.Generic;

using NuGetPackagesReport.Entities.NuGet;
using NuGetPackagesReport.NuGetService;

namespace NuGetPackagesReport.Helpers
{
    public interface INuGetApiConnector
    {
        List<Tuple<NuGetPackage, V2FeedPackage>> RetrieveInformationFromNuGet(List<NuGetPackage> packages);
    }
}
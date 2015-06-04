using System;
using System.Collections.Generic;

using NuGetPackagesReport.Entities.NuGet;
using NuGetPackagesReport.NuGetService;

namespace NuGetPackagesReport.Helpers
{
    public interface IReport
    {
        void Generate(string title, List<Tuple<NuGetPackage, V2FeedPackage>> packages);
    }
}
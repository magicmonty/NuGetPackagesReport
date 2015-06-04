using System;
using System.Collections.Generic;
using System.Linq;

using NuGetPackagesReport.Entities.NuGet;
using NuGetPackagesReport.NuGetService;

namespace NuGetPackagesReport.Helpers
{
    public class NuGetApiConnector : INuGetApiConnector
    {
        public List<Tuple<NuGetPackage, V2FeedPackage>> RetrieveInformationFromNuGet(List<NuGetPackage> packages)
        {
            List<Tuple<NuGetPackage, V2FeedPackage>> result = new List<Tuple<NuGetPackage, V2FeedPackage>>();

            var context = new V2FeedContext(new Uri("https://www.nuget.org/api/v2"));

            foreach (var package in packages)
            {
                var packageExistsInRemoteLocation =
                    context.Packages.Where(x => x.Id == package.Id).OrderByDescending(x => x.Version).Take(1).ToList().Any();

                V2FeedPackage v2FeedPackage = null;

                if (packageExistsInRemoteLocation)
                {
                    var nugetPackageInformations =
                        context.Packages.Where(x => (x.Id == package.Id) && (x.Version == package.Version)).ToList();

                    v2FeedPackage = nugetPackageInformations.SingleOrDefault();
                }

                result.Add(new Tuple<NuGetPackage, V2FeedPackage>(package, v2FeedPackage));
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

using NuGetPackagesReport.Entities.NuGet;
using NuGetPackagesReport.NuGetService;

namespace NuGetPackagesReport.Helpers
{
    public class ConfluenceReport : IReport
    {
        /// <summary>
        /// Generates a report using Confluence markup
        /// </summary>
        public void Generate(string title, List<Tuple<NuGetPackage, V2FeedPackage>> packages)
        {
            var packagesGroupedById = packages.GroupBy(x => x.Item1.Id);

            Func<string, string> toUnknownString = (input) => String.IsNullOrEmpty(input) ? "UNKNOWN" : input;

            Func<string, string> toConfluenceMarkdownUrl =
                (urlString) => String.IsNullOrEmpty(urlString) ? toUnknownString(urlString) : String.Format("[{0}]", urlString);

            Console.WriteLine("h3. {0}", title);
            Console.WriteLine(String.Empty);
            Console.WriteLine("||Package id||Version in use||License Names||License Url||Details Url||");
            foreach (var packageGroup in packagesGroupedById.OrderBy(x => x.Key))
            {
                var packageWithLatestVersion =
                    packageGroup.Where(x => x.Item2 != null).OrderByDescending(x => x.Item2.Version).Select(x => x.Item2).FirstOrDefault();

                string versionsInUse = String.Join("; ", packageGroup.Select(x => x.Item1.Version).Distinct().OrderBy(x => x));

                Console.WriteLine(
                    "|{0}|{1}|{2}|{3}|{4}|",
                    packageGroup.Key,
                    versionsInUse,
                    toUnknownString(packageWithLatestVersion != null ? packageWithLatestVersion.LicenseNames : null),
                    toConfluenceMarkdownUrl(packageWithLatestVersion != null ? packageWithLatestVersion.LicenseUrl : null),
                    toConfluenceMarkdownUrl(packageWithLatestVersion != null ? packageWithLatestVersion.GalleryDetailsUrl : null));
            }
            Console.WriteLine(String.Empty);
        }
    }
}

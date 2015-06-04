using System.Collections.Generic;
using System.Xml.Serialization;

namespace NuGetPackagesReport.Entities.NuGet
{
    [XmlRoot("packages")]
    public class NuGetPackagesConfig
    {
        [XmlElement("package")]
        public List<NuGetPackage> Packages { get; set; }
    }
}
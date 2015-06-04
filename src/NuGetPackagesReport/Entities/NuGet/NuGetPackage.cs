using System.Xml.Serialization;

namespace NuGetPackagesReport.Entities.NuGet
{
    public class NuGetPackage
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("version")]
        public string Version { get; set; }
    }
}
using System;
using System.Linq;

namespace NuGetPackagesReport
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!args.Any())
            {
                Console.WriteLine(@"Usage: {0} folder1 [folder2] [folderN]", System.AppDomain.CurrentDomain.FriendlyName);
                Console.WriteLine(@"Where [folder] is a folder that contains a VS solution");
                return;
            }

            new Main().Execute(args.Distinct().ToList());
        }
    }
}

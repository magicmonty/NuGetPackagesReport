NuGetPackagesReport
-------------------

Generates a report with a list of NuGet packages that are used in a VisualStudio solution.

Report format
-------------

Report is generated usgin Confluence markup language.

Report columns
--------------

- Package id
- Version in use
- License Names
- License Url
- Details Url


How To
-----

$NuGetPackagesReport.exe vsSolutionFolder1 vsSolutionFolder2 vsSolutionFolder3 > report.txt
@ECHO off
SET assemblyName=Suppliers.EF
SET testProjectName=Suppliers.EF.Tests

SET openCoverPath=..\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe
SET reportGeneratorPath=..\packages\ReportGenerator.2.4.4.0\tools\ReportGenerator.exe
SET testRunnerPath=..\packages\NUnit.ConsoleRunner.3.2.0\tools\nunit3-console.exe
SET browserPath=C:\Program Files (x86)\Google\Chrome\Application\chrome.exe

rmdir CodeCoverage /s /q
mkdir CodeCoverage
@ECHO on
"%openCoverPath%" -register:user -target:"%testRunnerPath%" -targetargs:" "bin\Debug\%testProjectName%.dll" " -filter:"+[%assemblyName%]* 
"%reportGeneratorPath%" -reports:"results.xml" -targetdir:"CodeCoverage"
start "%browserPath%" "CodeCoverage\index.htm"
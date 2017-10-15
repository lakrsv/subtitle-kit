$testProjects = "SubtitleKitLibTests"

# Get the most recent OpenCover NuGet package from the dotnet nuget packages
$nugetOpenCoverPackage = Join-Path -Path "C:\Users\lakrs" -ChildPath "\.nuget\packages\openCover"

$latestOpenCover = Join-Path -Path ((Get-ChildItem -Path $nugetOpenCoverPackage | Sort-Object Fullname -Descending)[0].FullName) -ChildPath "tools\OpenCover.Console.exe"
# Get the most recent OpenCoverToCoberturaConverter from the dotnet nuget packages
$nugetCoberturaConverterPackage = Join-Path -Path "C:\Users\lakrs" -ChildPath "\.nuget\packages\opencovertocoberturaconverter"
$latestCoberturaConverter = Join-Path -Path (Get-ChildItem -Path $nugetCoberturaConverterPackage | Sort-Object Fullname -Descending)[0].FullName -ChildPath "tools\OpenCoverToCoberturaConverter.exe"

# Get the most recent Coveralls-net package from the dotnet nuget packages
#$nugetCoverallsPackage = Join-Path -Path "C:\users\lakrs" -ChildPath "\.nuget\packages\coveralls.net"
#$latestCoveralls = Join-Path -Path ((Get-ChildItem -Path $nugetCoverallsPackage | Sort-Object Fullname -Descending)[0].FullName) -ChildPath "tools\csmacnz.Coveralls.exe"

#Get the most recent codecov-exe package for the dotnet nuget packages
$nugetCodecovPackage = Join-Path -Path "C:\Users\lakrs" -ChildPath "\.nuget\packages\codecov"
$latestCodecov = Join-Path -Path (Get-ChildItem -Path $nugetCoberturaConverterPackage | Sort-Object Fullname -Descending)[0].FullName -ChildPath "tools\codecov.exe"

If (Test-Path "$PSScriptRoot\OpenCover.coverageresults"){
	Remove-Item "$PSScriptRoot\OpenCover.coverageresults"
}

If (Test-Path "$PSScriptRoot\Cobertura.coverageresults"){
	Remove-Item "$PSScriptRoot\Cobertura.coverageresults"
}

& dotnet restore SubtitleKitLib/SubtitleKitLib.sln

$testRuns = 1;
foreach ($testProject in $testProjects){
    # Arguments for running dotnet
    $dotnetArguments = "xunit", "-xml `"`"$PSScriptRoot\testRuns_$testRuns.testresults`"`""

    "Running tests with OpenCover"
    & $latestOpenCover `
        -register `
        -target:dotnet.exe `
        -targetdir:$PSScriptRoot\$testProject `
        "-targetargs:$dotnetArguments" `
        -returntargetcode `
        -output:"$PSScriptRoot\OpenCover.coverageresults" `
        -mergeoutput `
        -oldStyle `
        -excludebyattribute:System.CodeDom.Compiler.GeneratedCodeAttribute `
        "-filter:+[SubtitleKitLib*]*+[SubtitleParsers*]*-[*SubtitleKitLibTests]*"

        $testRuns++
}

"Converting coverage reports to Cobertura format"
& $latestCoberturaConverter `
    -input:"$PSScriptRoot\OpenCover.coverageresults" `
    -output:"$PSScriptRoot\Cobertura.coverageresults" `
    "-sources:$PSScriptRoot"
	
#"Publishing test results to Coveralls.io"
#& $latestCoveralls --opencover --i "$PSScriptRoot\OpenCover.coverageresults" --useRelativePaths

"Publishing test results to Codecov"
& lastestCodecov -f "$PSScriptRoot\OpenCover.coverageresults" -f $Env:CodeCovToken
param(
    [string]$Configuration = "Debug",
    [string]$Platform = "Any CPU",
    [string]$Solution = "LADA.sln",
    [switch]$Clean,
    [switch]$Restore,
    [string]$MsBuildPath
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

function Resolve-VsWherePath {
    $candidates = @(
        (Join-Path ${env:ProgramFiles(x86)} "Microsoft Visual Studio\Installer\vswhere.exe"),
        (Join-Path $env:ProgramFiles "Microsoft Visual Studio\Installer\vswhere.exe")
    )

    foreach ($candidate in $candidates) {
        if (-not [string]::IsNullOrWhiteSpace($candidate) -and (Test-Path -LiteralPath $candidate)) {
            return (Resolve-Path -LiteralPath $candidate).Path
        }
    }

    throw "vswhere.exe not found. Install Visual Studio 2022 or Visual Studio Build Tools 2022."
}

function Resolve-MsBuildPath {
    param(
        [string]$OverridePath
    )

    if (-not [string]::IsNullOrWhiteSpace($OverridePath)) {
        if (-not (Test-Path -LiteralPath $OverridePath)) {
            throw "MSBuild path override does not exist: $OverridePath"
        }

        return (Resolve-Path -LiteralPath $OverridePath).Path
    }

    $vswherePath = Resolve-VsWherePath
    $matches = & $vswherePath -latest -products * -requires Microsoft.Component.MSBuild -find "MSBuild\**\Bin\MSBuild.exe"

    if (-not $matches) {
        throw "MSBuild.exe not found via vswhere. Install Visual Studio Build Tools with MSBuild component."
    }

    $preferred = $matches | Where-Object { $_ -match "\\MSBuild\\Current\\Bin\\MSBuild\\.exe$" } | Select-Object -First 1
    if ($preferred) {
        return $preferred
    }

    return ($matches | Select-Object -First 1)
}

function Resolve-SolutionPath {
    param(
        [string]$SolutionArg
    )

    if ([string]::IsNullOrWhiteSpace($SolutionArg)) {
        throw "Solution path cannot be empty."
    }

    $candidate = $SolutionArg
    if (-not [System.IO.Path]::IsPathRooted($candidate)) {
        $candidate = Join-Path $PSScriptRoot $candidate
    }

    $fullPath = [System.IO.Path]::GetFullPath($candidate)
    if (-not (Test-Path -LiteralPath $fullPath)) {
        throw "Solution file not found: $fullPath"
    }

    return $fullPath
}

function Resolve-Targets {
    param(
        [switch]$NeedClean,
        [switch]$NeedRestore
    )

    if ($NeedClean -and $NeedRestore) {
        return "Restore;Clean;Build"
    }

    if ($NeedRestore) {
        return "Restore;Build"
    }

    if ($NeedClean) {
        return "Clean;Build"
    }

    return "Build"
}

try {
    $solutionPath = Resolve-SolutionPath -SolutionArg $Solution
    $msbuildExe = Resolve-MsBuildPath -OverridePath $MsBuildPath
    $targets = Resolve-Targets -NeedClean:$Clean -NeedRestore:$Restore

    Write-Host "Solution : $solutionPath"
    Write-Host "MSBuild  : $msbuildExe"
    Write-Host "Target   : $targets"
    Write-Host "Config   : $Configuration"
    Write-Host "Platform : $Platform"

    $arguments = @(
        $solutionPath
        "/t:$targets"
        "/p:Configuration=$Configuration"
        "/p:Platform=$Platform"
        "/m"
    )

    & $msbuildExe @arguments
    if ($LASTEXITCODE -ne 0) {
        throw "MSBuild failed with exit code $LASTEXITCODE."
    }

    Write-Host "Build completed successfully."
}
catch {
    Write-Error $_.Exception.Message
    exit 1
}

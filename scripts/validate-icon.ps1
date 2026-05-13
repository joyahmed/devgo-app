param(
    [string]$IconPath = "assets/icon.ico"
)

$requiredSizes = @(16, 24, 32, 48, 64, 128, 256)

if (-not (Test-Path -LiteralPath $IconPath)) {
    Write-Error "Icon file not found: $IconPath"
    exit 1
}

[byte[]]$bytes = [System.IO.File]::ReadAllBytes($IconPath)

if ($bytes.Length -lt 6) {
    Write-Error "Invalid ICO file: too small."
    exit 1
}

$reserved = [BitConverter]::ToUInt16($bytes, 0)
$iconType = [BitConverter]::ToUInt16($bytes, 2)
$count = [BitConverter]::ToUInt16($bytes, 4)

if ($reserved -ne 0 -or $iconType -ne 1 -or $count -lt 1) {
    Write-Error "Invalid ICO header."
    exit 1
}

$foundSizes = New-Object 'System.Collections.Generic.HashSet[int]'

for ($i = 0; $i -lt $count; $i++) {
    $entryOffset = 6 + ($i * 16)
    if ($entryOffset + 15 -ge $bytes.Length) {
        break
    }

    $w = $bytes[$entryOffset]
    $h = $bytes[$entryOffset + 1]

    $width = if ($w -eq 0) { 256 } else { [int]$w }
    $height = if ($h -eq 0) { 256 } else { [int]$h }

    if ($width -eq $height) {
        [void]$foundSizes.Add($width)
    }
}

$missing = @($requiredSizes | Where-Object { -not $foundSizes.Contains($_) })
$sortedFound = @($foundSizes.ToArray() | Sort-Object)

Write-Host "Icon: $IconPath"
Write-Host "Found sizes: $($sortedFound -join ', ')"
Write-Host "Required sizes: $($requiredSizes -join ', ')"

if ($missing.Count -gt 0) {
    Write-Error "Missing required sizes: $($missing -join ', ')"
    exit 1
}

Write-Host "PASS: icon contains all required sizes." -ForegroundColor Green
exit 0

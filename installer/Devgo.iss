[Setup]
AppName=DevGo
AppVersion=1.0.1
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
DefaultDirName={autopf}\DevGo
DefaultGroupName=DevGo
OutputDir=output
OutputBaseFilename=DevGo-Setup
Compression=lzma
SolidCompression=yes
SetupIconFile=..\assets\icon.ico
UninstallDisplayIcon={app}\DevGo.exe

[Files]
Source: "..\bin\Release\net10.0-windows\win-x64\publish\*"; DestDir: "{app}"; Flags: recursesubdirs

[Icons]
Name: "{group}\DevGo"; Filename: "{app}\DevGo.exe"; WorkingDir: "{app}"
Name: "{commondesktop}\DevGo"; Filename: "{app}\DevGo.exe"; WorkingDir: "{app}"

[Run]
Filename: "{app}\DevGo.exe"; Description: "Launch DevGo"; WorkingDir: "{app}"; Flags: nowait postinstall skipifsilent

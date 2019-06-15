[Setup]
AppName=Light Sync
AppVersion=1.0.0.1
WizardStyle=modern
DefaultDirName={autopf}\LightSync
DefaultGroupName=LightSync
UninstallDisplayName=Light Sync
UninstallDisplayIcon={app}\LightSync.Config.exe
Compression=lzma2
SolidCompression=yes
OutputDir=..\bin\Setup\
OutputBaseFilename=LightSyncSetup
PrivilegesRequired=admin
CloseApplications=force
AppPublisher=AndrewBabbitt97

[Files]
Source: "..\bin\Release\netcoreapp3.0\publish\**"; DestDir: "{app}"; Flags: recursesubdirs ignoreversion

[Registry]
Root: HKLM; SubKey: "SOFTWARE\Microsoft\Windows\CurrentVersion\Run"; ValueType: string; ValueName: "LightSync"; ValueData: """{app}\LightSync.exe"; Flags: uninsdeletevalue

[Icons]
Name: "{group}\Light Sync"; Filename: "{app}\LightSync.Config.exe"; WorkingDir: "{app}"

[Run]
Filename: "{app}\LightSync.exe"; StatusMsg: "Starting Light Sync..."; Flags: nowait

[UninstallRun]
Filename: "{cmd}"; Parameters: "/C""taskkill /im LightSync.exe /f /t"
Filename: "{cmd}"; Parameters: "/C""taskkill /im LightSync.Config.exe /f /t"

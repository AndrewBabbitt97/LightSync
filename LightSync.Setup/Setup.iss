[Setup]
AppName=Light Sync
AppVersion=1.0.0.3
WizardStyle=modern
DefaultDirName={autopf}\Light Sync
DefaultGroupName=Light Sync
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

[Icons]
Name: "{group}\Light Sync"; Filename: "{app}\LightSync.Config.exe"; WorkingDir: "{app}"

[Run]
Filename: "{sys}\sc.exe"; Parameters: "create LightSync start=auto binpath=""{app}\LightSync.exe"""; StatusMsg: "Installing Light Sync Service..."; Flags: runhidden
Filename: "{sys}\sc.exe"; Parameters: "start LightSync"; StatusMsg: "Starting Light Sync Service..."; Flags: runhidden

[UninstallRun]
Filename: "{sys}\sc.exe"; Parameters: "stop LightSync"; StatusMsg: "Stopping Light Sync Service..."; Flags: runhidden
Filename: "{sys}\sc.exe"; Parameters: "delete LightSync"; StatusMsg: "Uninstalling Light Sync Service..."; Flags: runhidden
Filename: "{cmd}"; Parameters: "/C""taskkill /im LightSync.Config.exe /f /t"


[Setup]
AppName={code:MyAppName}
AppVerName={code:MyAppName} 8.3.3
AppPublisher=saveenr@microsoft.com
DefaultDirName={pf}\{code:MyAppShortName}
DisableDirPage=yes
DisableReadyPage=yes
DefaultGroupName={code:MyAppName}
DisableProgramGroupPage=yes
DisableFinishedPage=yes
Compression=lzma
SolidCompression=yes

[Files]
Source: "bin\**.*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\license"; Filename: "{app}\rifconv-license.doc"
Name: "{group}\readme"; Filename: "{app}\rifconv-readme.doc"
Name: "{group}\{code:MyAppName}"; Filename: "{app}\rifconverterapp.exe"; Parameters: "{code:cmdlaunchparams}";  WorkingDir: "{app}"
Name: "{group}\{cm:UninstallProgram,{code:MyAppName}}"; Filename: "{uninstallexe}"; Parameters: "/SILENT";


[Code]

function MyAppName( Default: string ): string;
begin
  Result := 'RIFConverter';
end;

function MyAppShortName( Default: string ): string;
begin
  Result := 'rifconv8.3.2';
end;

function cmdlaunchparams( Default: string ): string;
begin
  Result := ''
end;

function MyProgCheck(): Boolean;
begin
  Result := False;
end;

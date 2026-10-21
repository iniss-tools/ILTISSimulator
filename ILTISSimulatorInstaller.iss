; Instalator ILTISSimulatora. Spolocny obsah instalatorov je v ToolsCore\Installer\AppSetup.iss.
; Pred kompilaciou treba zostavit Release: dotnet build ILTISSimulator\ILTISSimulator.csproj -c Release

#define MyAppName "ILTISSimulator"
#define MyAppExeName "ILTISSimulator.exe"
#define MyAppDir SourcePath + "ILTISSimulator\bin\Release\net10.0-windows"
#define MyOutputBaseFilename "iltissimulator-setup"
; Grafikon cita GVDEditor.dll. Spustac GVDEditora a jeho nastavenia behu SDK kopiruje k referencovanemu programu,
; ILTISSimulator ich nepotrebuje.
#define MyAppExcludes ",\GVDEditor.exe,\GVDEditor.runtimeconfig.json,\GVDEditor.deps.json"
; framework ASP.NET Core prenasa referencia na GVDEditor - bez neho sa program nespusti
#define NeedAspNetCore

#include "..\ToolsCore\Installer\AppSetup.iss"

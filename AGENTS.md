# AGENTS.md

Single-project Windows-only .NET Framework 4.7.2 WinForms app (Dutch UI/identifiers), classic non-SDK `RDPKeuze.csproj`. No NuGet packages, no tests, no linters, no CI.

## Build / verify

- Build (only verification that exists; 0 warnings/0 errors, no restore step — no packages):
  `& "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" RDPKeuze.sln /p:Configuration=Debug` (also verified with `/p:Configuration=Release`).
- **Do not rely on `dotnet build RDPKeuze.sln -c Debug`.** It only succeeds when `obj\` already holds up-to-date resource outputs. It fails with `MSB3823`/`MSB3822` whenever resources for `MainFormRdp.resx` or `Properties\Resources.resx` must be regenerated: on a fresh clone, after deleting `obj\`, or after touching those two files (a designer edit to the main form rewrites its `.resx`). Cause: the .NET SDK MSBuild can't emit their non-string image resources without `System.Resources.Extensions`, which this no-NuGet project can't reference. Recovery = run the VS MSBuild command above; the errors are not your code.
- Run: `bin\Debug\RDPKeuze.exe` (GUI app; no dev server, no watch mode).

## Editing rules (easy to get wrong)

- **New files must be added by hand to `RDPKeuze.csproj`.** No globbing: `<Compile Include="Foo.cs" />`. A new form also needs `<SubType>Form</SubType>`, its `Foo.Designer.cs` with `<DependentUpon>Foo.cs</DependentUpon>`, and `Foo.resx` as `<EmbeddedResource>` with the same `DependentUpon`. Copy an existing form's block.
- `RDPKeuze.csproj` references `server.cs` while the file is `Server.cs`; keep csproj and disk names identical (case included) or builds break on case-sensitive filesystems.
- Don't hand-edit generated files: `Properties\Resources.Designer.cs`, `Properties\Settings.Designer.cs`. `.Designer.cs` for forms is designer-managed; keep the `InitializeComponent` region intact when editing by hand.
- Filename ≠ class name in several places: `MainFormRdp.cs` → `FormRdpKeuze` (main form), `Wachtwoordclass.cs` → `WachtWoordForm`. Don't rename to "fix" this.

## Runtime data (fresh-clone gotcha)

- All data files are read/written relative to the **exe directory** (`AppDomain.CurrentDomain.BaseDirectory`, exposed as `FormRdpKeuze.datapath`): `data.xml` (server list), `ww.xml` (passwords), `ronald.rdp` (RDP template), plus optional `UserNaam.txt`, `nameserver.txt`, `vncviewer.exe`. No reads use the CWD — keep it that way.
- `bin/` is gitignored, so these files only exist locally in `bin\Debug\`. A missing `ronald.rdp` is tolerated (`LeesRdpTemplate()` returns `""`); a missing `vncviewer.exe` shows a MessageBox instead of crashing. On a fresh clone the app therefore starts empty — restore those files into `bin\Debug\` for full functionality.
- Missing optional data files are swallowed by empty `catch {}` blocks in `DataRdp`/`ZoekWW` — that silent-failure style is intentional; keep reads wrapped the same way. Note `DataRdp.Lees_server_lijst()` only replaces `Server_lijst` on a successful read (it used to `Clear()` first, so a failed read + save wiped `data.xml`) — don't reintroduce that.

## Behavior worth preserving

- Passwords live in `ww.xml` obfuscated with a trivial shift cipher — `Scramble`/`Unscramble` in `WachtWoordForm` (`Wachtwoordclass.cs`), which `ZoekWW` also calls. Not encryption. Changing the algorithm breaks existing `ww.xml` files.
- Selecting a server puts the password on the clipboard and the `timerResetWWopClipbord` timer clears it; don't remove the auto-clear.
- `buttonWW_Click` in `MainFormRdp.cs`: DEBUG builds skip the master-password gate, Release builds require a hardcoded password. That's deliberate — keep both paths when touching it.
- `server._domein` is a type discriminator, not just a domain: the value `"VNC"` switches the main form into VNC mode (`SchrijfDataIntextBox`).
- Saving goes through `DataRdp.Schrijf_server_lijst()`, which sorts by `_sectie` first; `DataRdp.Server_lijst` is mutated in place by UI handlers and re-read on every `UpdateUi()`.

## Conventions

- Dutch for identifiers, comments, and UI strings; match the surrounding language.
- Discarded return values are written `_ = expr;` (e.g., `_ = Edit.ShowDialog();`).
- Everything is in namespace `RDPKeuze`; there are no sub-namespaces or projects.

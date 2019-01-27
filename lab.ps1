
# simple build
Get-ChildItem -Path "bin/" -Include *.* -File -Recurse | foreach { $_.Delete()}

csc /target:library /out:"bin/Common.dll" "Src/Common\CommonLibary.cs"

csc /target:library /out:"bin/StudentLib.dll" /reference:"bin/Common.dll" "Src/StudentLib\Calculation.cs" "Src/StudentLib\Libary.cs"

csc /target:exe /out:"bin/Program.exe" /reference:"bin/Common.dll" /reference:"bin/StudentLib.dll" "Src/Program\Program.cs"

# ildasm
ildasm "bin/Program.exe"

# key
Get-ChildItem -Path "Sn/" -Include *.* -File -Recurse | foreach { $_.Delete()}
sn /k "Sn/Common.snk"
sn /p "Sn/Common.snk" "Sn/Common.Public.snk"
sn /k "Sn/StudentLib.snk"
sn /k "Sn/Program.snk"

# netmodules

Get-ChildItem -Path "bin/" -Include *.* -File -Recurse | foreach { $_.Delete()}

csc /target:module /out:"bin/Common.netmodule" "Src/Common\CommonLibary.cs"

csc /target:module /out:"bin/StudentLib.netmodule" /addmodule:"bin/Common.netmodule" "Src/StudentLib\Calculation.cs" "Src/StudentLib\Libary.cs"

csc /target:module /out:"bin/Program.netmodule" /addmodule:"bin/Common.netmodule" /addmodule:"bin/StudentLib.netmodule" "Src/Program\Program.cs"

csc /target:exe /out:"bin/ProgramByNetModule.exe" /addmodule:"bin/Common.netmodule" /addmodule:"bin/StudentLib.netmodule" "Src/Program\Program.cs"


# signing

Get-ChildItem -Path "bin/" -Include *.* -File -Recurse | foreach { $_.Delete()}

csc /target:library /keyfile:"Sn/Common.snk" /out:"bin/Common.dll" "Src/Common\CommonLibary.cs"

csc /target:library /keyfile:"Sn/StudentLib.snk" /out:"bin/StudentLib.dll" /reference:"bin/Common.dll" "Src/StudentLib\Calculation.cs" "Src/StudentLib\Libary.cs"

csc /target:exe /keyfile:"Sn/Program.snk" /out:"bin/Program.exe"  /reference:"bin/Common.dll" /reference:"bin/StudentLib.dll" "Src/Program\Program.cs"


# delayed

Get-ChildItem -Path "bin/" -Include *.* -File -Recurse | foreach { $_.Delete()}

csc /target:library /keyfile:"Sn/Common.Public.snk" /delaysign /out:"bin/Common.dll" "Src/Common\CommonLibary.cs"

csc /target:library /keyfile:"Sn/StudentLib.snk" /out:"bin/StudentLib.dll" /reference:"bin/Common.dll" "Src/StudentLib\Calculation.cs" "Src/StudentLib\Libary.cs"

csc /target:exe /keyfile:"Sn/Program.snk" /out:"bin/Program.exe"  /reference:"bin/Common.dll" /reference:"bin/StudentLib.dll" "Src/Program\Program.cs"

sn /R "bin/Common.dll" "Sn/Common.snk"


# versions

Get-ChildItem -Path "bin/" -Include *.* -File -Recurse | foreach { $_.Delete()}

csc /target:module /out:"bin/Common.netmodule" "Src/Common\CommonLibary.cs"

csc /target:module /out:"bin/StudentLib.netmodule" /addmodule:"bin/Common.netmodule" "Src/StudentLib\Calculation.cs" "Src/StudentLib\Libary.cs"

al /target:lib /keyfile:"Sn/Common.snk" /out:"bin/Common.dll" /version:1.0.0.0 "bin/Common.netmodule"

al /target:lib /keyfile:"Sn/Common.snk"  /out:"bin/StudentLib.dll" /addmodule:"bin/Common.netmodule" /version:1.1.0.1 "bin/StudentLib.netmodule"

csc /target:exe /out:"bin/Program.exe" /keyfile:"Sn/Program.snk" /reference:"bin/Common.dll" /reference:"bin/StudentLib.dll" "Src/Program\Program.cs"

#different locations

Get-ChildItem -Path "bin/" -Include *.* -File -Recurse | foreach { $_.Delete()}

csc /target:library /keyfile:"Sn/Common.snk" /out:"bin/Common.dll" "Src/Common\CommonLibary.cs"

csc /target:library /keyfile:"Sn/StudentLib.snk" /out:"bin/StudentLib.dll" /reference:"bin/Common.dll" "Src/StudentLib\Calculation.cs" "Src/StudentLib\Libary.cs"

csc /target:exe /keyfile:"Sn/Program.snk" /out:"bin/Program.exe"  /reference:"bin/Common.dll" /reference:"bin/StudentLib.dll" "Src/Program\Program.cs"

move "bin/Common.dll" "bin/Libs/Common.dll"
move "bin/StudentLib.dll" "bin/Libs/StudentLib.dll"

copy "App.config" "bin/Program.exe.config"

# version binding (not ready)

Get-ChildItem -Path "bin/" -Include *.* -File -Recurse | foreach { $_.Delete()}

csc /target:library /keyfile:"Sn/Common.snk" /out:"bin/Common.dll" "Src/Common\Assembly.cs" "Src/Common\CommonLibary.cs"

csc /target:library /keyfile:"Sn/StudentLib.snk" /out:"bin/StudentLib.dll" /reference:"bin/Common.dll" "Src/StudentLib\Calculation.cs" "Src/StudentLib\Libary.cs"

rm "bin/Common.dll"

csc /target:library /keyfile:"Sn/Common.snk" /out:"bin/Common.dll" "Src\Common\Assembly2.cs" "Src\Common\CommonLibary.cs"

csc /target:exe /keyfile:"Sn/Program.snk" /out:"bin/Program.exe"  /reference:"bin/Common.dll" /reference:"bin/StudentLib.dll" "Src/Program\Program.cs"

copy "StudentLib.config" "bin/StudentLib.dll.config"

# gac
gacutil /i "bin/StudentLib.dll"

# get publickeytoken
sn -T "bin/Common.dll"
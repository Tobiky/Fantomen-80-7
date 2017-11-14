@echo off
cd %%~dp0
for /r %%F in (*.proto) do protoc.exe %%~nF".proto" --csharp_out=./

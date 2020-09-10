cd %~dp0/consul_1.8.3_windows_amd64
start cmd /C "title Consul && consul.exe agent -dev"

cd D:\OpenSources\skywalking\dist\apache-skywalking-apm-bin\bin
start cmd /C "startup.bat"

cd %~dp0/Surging.Services/Surging.Services.Server
start cmd /C "title Server && dotnet run"

cd %~dp0/Surging.Services/Surging.Services.Client
start cmd /C "title Client && dotnet run"

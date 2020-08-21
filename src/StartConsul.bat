cd ./consul_1.8.3_windows_amd64
start cmd /C "title Consul && consul.exe agent -dev"

cd ../Surging.Services/Surging.Services.Server
start cmd /C "title Server && dotnet run"

cd ../Surging.Services.Client
start cmd /C "title Client && dotnet run"

dotnet ef migrations add "Initial" -c rentingdbcontext -p .\Infrastructure.csproj -s ..\BO\BO.csproj

dotnet ef database update -c rentingdbcontext -p .\Infrastructure.csproj -s ..\BO\BO.csproj
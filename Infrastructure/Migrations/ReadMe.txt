
dotnet ef migrations add "Initial" -c rentingdbcontext -p .\Infrastructure.csproj -s ..\BO\BO.csproj

dotnet ef database update -c rentingdbcontext -p .\Infrastructure.csproj -s ..\BO\BO.csproj


----migration to script---
dotnet ef migrations script "20260308081253_Unit Taxes fields added" -c rentingdbcontext -p .\Infrastructure.csproj -s ..\BO\BO.csproj -o SQL.SQL
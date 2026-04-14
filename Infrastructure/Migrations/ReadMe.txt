
dotnet ef migrations add "Initial" -c rentingdbcontext -p .\Infrastructure.csproj -s ..\BO\BO.csproj

dotnet ef database update -c rentingdbcontext -p .\Infrastructure.csproj -s ..\BO\BO.csproj


----migration to script---
dotnet ef migrations script "20260308081253_Unit Taxes fields added" -c rentingdbcontext -p .\Infrastructure.csproj -s ..\BO\BO.csproj -o SQL.SQL


chat bot instructions
1. U got to BotFather
2. type /mybots 
   u will get the bot token
3. Send message to group from tlegram
4. open https://api.telegram.org/bot[bot-token]/getUpdates
   U will see the chat id of the group
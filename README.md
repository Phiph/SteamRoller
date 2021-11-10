SteamRoller
az deployment group create --name SteamRoller --resource-group SteamRoller-Prod --template-file main.bicep 
  --parameters storageAccountType=Standard_GRS


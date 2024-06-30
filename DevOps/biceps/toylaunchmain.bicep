@description('Specifies the location for resources.')
param location string = resourceGroup().location

param appServiceAppName string = 'toylaunchapp240301'

@allowed([
  'nonprod'
  'prod'
])
param environmentType string = 'nonprod'

module myModule 'modules/mod1.bicep' = {
  name: 'MyModule'
  params: {
    location: location
  }
}

module appService 'modules/appService.bicep' = {
  name: 'appService'
  params: {
    location: location
    environmentType: environmentType
    appServiceAppName: appServiceAppName
  }
  
}

resource storageAccount 'Microsoft.Storage/storageAccounts@2023-01-01' = {
  name: 'toylaunchstorage240301'
  location: location
  sku: {
    name: (environmentType == 'prod') ? 'Standard_GRS' : 'Standard_LRS'
  }
  kind: 'StorageV2'
  properties: {
    accessTier: 'Hot'
  }
}

output appServiceAppHostName string = appService.outputs.appServiceAppHostName

param location string
param name string
param appInsightsName string = 'appins-${name}'

resource law 'Microsoft.OperationalInsights/workspaces@2020-03-01-preview' = {
  name: name
  location: location
  properties: any({
    retentionInDays: 30
    features: {
      searchVersion: 1
    }
    sku: {
      name: 'PerGB2018'
    }
  })
}

resource appInsights 'Microsoft.Insights/components@2020-02-02-preview' = {
  name: appInsightsName
  location: location
  kind: 'web'
  properties: { 
    Application_Type: 'web'
    Flow_Type: 'Redfield'
    Request_Source: 'CustomDeployment'
  }
}

output clientId string = law.properties.customerId
output clientSecret string = law.listKeys().primarySharedKey
output appInsights string = appInsights.properties.InstrumentationKey

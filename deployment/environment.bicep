param name string
param location string
param lawClientId string
param lawClientSecret string
param appinsightskey string

resource env 'Microsoft.Web/kubeEnvironments@2021-02-01' = {
  name: name
  location: location
  properties: {
    type: 'managed'
    internalLoadBalancerEnabled: false
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: lawClientId
        sharedKey: lawClientSecret
      }
    }
    containerAppsConfiguration: {
      daprAIInstrumentationKey: appinsightskey
    }
  }
}
output id string = env.id

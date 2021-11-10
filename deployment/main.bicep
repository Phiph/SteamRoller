param location string = resourceGroup().location
param envName string = 'steamroller'


param minReplicas int = 0


param actorImage string = 'SteamRoller.Actors'
param actorPort int = 80


param containerRegistry string
param containerRegistryUsername string

@secure()
param containerRegistryPassword string

var actorServiceAppName = 'actor-service-app'
var pythonServiceAppName = 'python-app'
var goServiceAppName = 'go-app'

module law 'loganalytics.bicep' = {
    name: 'log-analytics-workspace'
    params: {
      location: location
      name: 'law-${envName}'
    }
}

module containerAppEnvironment 'environment.bicep' = {
  name: 'container-app-environment'
  params: {
    name: envName
    location: location
    lawClientId:law.outputs.clientId
    lawClientSecret: law.outputs.clientSecret
    appinsightskey: law.outputs.appInsights
  }
}

module redis 'redis.bicep' = {
  name: 'redis-state'
  params:{
    redisCacheName: 'state-${envName}'
  }
}


module actorService 'container-http.bicep' = {
  name: actorServiceAppName
  params: {
    location: location
    containerAppName: actorServiceAppName
    environmentId: containerAppEnvironment.outputs.id
    containerImage: actorImage
    containerPort: actorPort
    isExternalIngress: false
    minReplicas: minReplicas
    containerRegistry: containerRegistry
    containerRegistryUsername: containerRegistryUsername
    containerRegistryPassword: containerRegistryPassword
    daprComponents: [
      {
        name: 'actorstateservice'
        type: 'state.redis'
        version: 'v1'
        metadata: [
          {
            name: 'redisHost'
            value: redis.outputs.redisHost
          }
          {
            name: 'enableTLS'
            value: 'true'
          }
          {
            name: 'actorStateStore'
            value: 'true'
          }
          {
            name: 'redisPassword'
            secretRef: 'masterkey'
          }
        ]
      }
    ]
    secrets: [
      {
        name: 'docker-password'
        value: containerRegistryPassword
      }
      {
        name: 'masterkey'
        value: redis.outputs.redisKey
      }
    ]
  }
}




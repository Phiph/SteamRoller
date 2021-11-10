// Redis Params

@description('Specify the name of the Azure Redis Cache to create.')
param redisCacheName string

// @description('Specify a boolean value that indicates whether diagnostics should be saved to the specified storage account.')
// param diagnosticsEnabled bool = true

resource redis 'Microsoft.Cache/redis@2020-12-01' = {
  name: redisCacheName
  location: 'uksouth'
  tags: {
    role: 'StateCache'
  }
  properties: {
    enableNonSslPort: true
    minimumTlsVersion: '1.2'
    publicNetworkAccess: 'Enabled'
    redisVersion: ''
    sku: {
      capacity: 2
      family: 'C'
      name: 'Basic'
    }
  }
}


output redisHost string = redis.properties.hostName
output redisKey string = redis.properties.accessKeys.primaryKey

@description('Name of the App Service')
param appServiceName string

@description('Location for all resources')
param location string = 'northeurope'

@description('Environment: staging or production')
param environment string = 'production'

@description('App Service Plan name')
param appServicePlanName string = '${appServiceName}-plan'

@description('App Service Plan SKU')
param sku string = 'S1'

resource appServicePlan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: sku
    tier: 'Standard'
    size: sku
    capacity: 3
  }
}

resource appInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: '${appServiceName}-ai'
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
  }
}

resource app 'Microsoft.Web/sites@2022-03-01' = {
  name: appServiceName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true

    // SonarQube security fixes
    clientCertEnabled: false

    siteConfig: {
      netFrameworkVersion: 'v8.0'
      alwaysOn: true

      // SonarQube security fixes
      minTlsVersion: '1.2'
      ftpsState: 'Disabled'

      appSettings: [
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value: appInsights.properties.InstrumentationKey
        }
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: appInsights.properties.ConnectionString
        }
        {
          name: 'ENVIRONMENT'
          value: 'production'
        }
      ]
    }
  }
}

resource stagingSlot 'Microsoft.Web/sites/slots@2022-03-01' = if (environment == 'staging') {
  parent: app
  name: 'staging'
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true

    // 🔐 SonarQube security fixes
      clientCertEnabled: false
      
    siteConfig: {
      netFrameworkVersion: 'v8.0'
      alwaysOn: false
      appSettings: [
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value: appInsights.properties.InstrumentationKey
        }
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: appInsights.properties.ConnectionString
        }
        {
          name: 'ENVIRONMENT'
          value: 'staging'
        }
      ]
    }
  }
}

output appServiceUrl string = app.properties.defaultHostName
output appInsightsKey string = appInsights.properties.InstrumentationKey

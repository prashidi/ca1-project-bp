@description('Name of the App Service')
param appServiceName string

@description('Location for all resources')
param location string = 'northeurope'

@description('Environment: staging or production')
param environment string = 'production'

@description('App Service Plan name')
param appServicePlanName string = '${appServiceName}-plan'

@description('App Service Plan SKU')
param sku string = 'B1'


// ------------------------------------------------------------
// APP SERVICE PLAN
// ------------------------------------------------------------
resource appServicePlan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: sku
    tier: 'Basic'
    size: sku
    capacity: 5
  }
}


// ------------------------------------------------------------
// APPLICATION INSIGHTS
// ------------------------------------------------------------
resource appInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: '${appServiceName}-ai'
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
  }
}


// ------------------------------------------------------------
// APP SERVICE — PRODUCTION SLOT
// ------------------------------------------------------------
resource app 'Microsoft.Web/sites@2022-03-01' = {
  name: appServiceName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true

    siteConfig: {
      netFrameworkVersion: 'v8.0'
      alwaysOn: true

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


// ------------------------------------------------------------
// STAGING SLOT (ONLY WHEN ENV == staging)
// ------------------------------------------------------------
resource stagingSlot 'Microsoft.Web/sites/slots@2022-03-01' = if (environment == 'staging') {
  name: 'staging'
  parent: app
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true

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

// ------------------------------------------------------------
// OUTPUTS
// ------------------------------------------------------------
output appServiceUrl string = app.properties.defaultHostName
output appInsightsKey string = appInsights.properties.InstrumentationKey

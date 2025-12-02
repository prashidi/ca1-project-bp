@description('Name of the App Service')
param appServiceName string

@description('Location for all resources')
@allowed([
  'northeurope'
  'westeurope'
])
param location string = 'northeurope'

@description('Deployment environment')
@allowed([
  'staging'
  'production'
])
param environment string = 'production'

@description('Resource ID of the existing App Service Plan')
param appServicePlanId string

// =============================
// EXISTING APP SERVICE PLAN
// =============================
resource appServicePlan 'Microsoft.Web/serverfarms@2022-03-01' existing = {
  id: appServicePlanId
}

// =============================
// APPLICATION INSIGHTS
// =============================
resource appInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: '${appServiceName}-ai'
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
  }
}

// =============================
// APP SERVICE (PRODUCTION SITE)
// =============================
resource app 'Microsoft.Web/sites@2022-03-01' = {
  name: appServiceName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    siteConfig: {
      // .NET 9 as per your app config
      netFrameworkVersion: 'v9.0'
      alwaysOn: true
      appSettings: [
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value: appInsights.properties.InstrumentationKey
        }
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          // Build connection string from InstrumentationKey
          value: 'InstrumentationKey=${appInsights.properties.InstrumentationKey}'
        }
        {
          name: 'ENVIRONMENT'
          value: environment
        }
      ]
    }
  }
}

// =============================
// STAGING SLOT (ONLY WHEN environment == "staging")
// =============================
resource stagingSlot 'Microsoft.Web/sites/slots@2022-03-01' = if (environment == 'staging') {
  name: 'staging'
  parent: app
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    siteConfig: {
      netFrameworkVersion: 'v9.0'
      alwaysOn: false
      appSettings: [
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value: appInsights.properties.InstrumentationKey
        }
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: 'InstrumentationKey=${appInsights.properties.InstrumentationKey}'
        }
        {
          name: 'ENVIRONMENT'
          value: 'staging'
        }
      ]
    }
  }
}

// =============================
// OUTPUTS
// =============================
output appServiceUrl string = 'https://${app.properties.defaultHostName}'
output appInsightsKey string = appInsights.properties.InstrumentationKey

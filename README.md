# mulesoft-api-gateway-portal
Mulesoft API gateway web portal, enables you to create Anypoint exchange assets, non-mule api proxies (in Runtime Fabric -RTF), contracts, applications.
This initial version supports MuleSoft Classic gateway for non-mule proxy deployment using web UI.
Prerequisites:
  i. Anypoint platform credentials/login
  ii. Create connected app in Anypoint Access Manager with grant_type=authorization code and redirect url = https://localhost:5001/anypointcallback
  iii. Grant full access to connecte app.

In the appsettings.json file add your Anypoint platform details like organization id, business group id, domain, connected app credentials, environment id,
rtf target id and name

  "AnyPoint": {
    "MasterOrganizationId": "",
    "OrganizationId": "",
    "BusinessGroupId": "",
    "Domain": "",
    "ConnectedApp_ClientId": "",
    "ConnectedApp_ClientSecret": "",
    "DEV": {
      //replace CLIENTID with connected app's client id
      "authorization_endpoint": "https://anypoint.mulesoft.com/accounts/api/v2/oauth2/authorize?client_id=CLIENTID&response_type=code&scope=full&redirect_uri=https://localhost:5001/anypointcallback",
      "token_endpoint": "https://anypoint.mulesoft.com/accounts/api/v2/oauth2/token",
      "grant_type": "authorization_code",
      "callback_uri": "https://localhost:5001/anypointcallback",
      "env_id": "",
      "rtf_target_id": "",
      "rtf_target_name": "mulesoft-anypoint-dev"
    }
  }
  
  This web app is implemented in Blazor (Microsoft's opensource). You need Visual Studio 2022 community version/Visual Code to run locally.

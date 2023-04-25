using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;
using System;
using ControlPlane.Data.GatewayModels;
using System.Collections.Generic;
using System.Text.Json;
using ControlPlane.Data.GatewayModels.Deployments;
using static System.Net.WebRequestMethods;
using ControlPlane.Data.GatewayModels.Runtime;
using Amazon.CloudWatch;
using System.Net.Http;

namespace ControlPlane.IntegrationServices.AnyPointPlatform
{
    public class PlatformAPIService
    {

        /// <summary>
        /// From connected app with grant_type = authorization_code
        /// for web applications with user authentication
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<AnyPointTokenResponse> GetAccessToken(string code)
        {
            try
            {
                var tokenURL = Startup.AnyPoint_Token_Endpoint;
                var client = new RestClient(tokenURL);
                var request = new RestRequest();
                request.Method = Method.Post;

               
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                request.AddParameter("client_id", Startup.AnyPoint_ConnectedApp_ClientId);
                request.AddParameter("client_secret", Startup.AnyPoint_ConnectedApp_ClientSecret);
                request.AddParameter("grant_type", "authorization_code");
                request.AddParameter("redirect_uri", "https://localhost:5001/anypointcallback");
                request.AddParameter("code", code);

                RestResponse response = await client.ExecuteAsync(request);

                //Console.WriteLine(response.Content);
                if (response != null & response.Content != null)
                {
                    //logger.Info("GetAccessToken Request Completed");
                    return JsonConvert.DeserializeObject<AnyPointTokenResponse>(response.Content);
                }
                else
                {
                    AnyPointTokenResponse emptyToken = new AnyPointTokenResponse();
                    emptyToken.access_token = "EXCEPTION";
                    return emptyToken;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += " &Inner Exception:" + ex.InnerException.Message;
                }
                AnyPointTokenResponse exceptionTokenResponse = new AnyPointTokenResponse();
                exceptionTokenResponse.access_token = "EXCEPTION:" + ex.Message;
                //logger.Error("GetAccessToken Request has Failed:" + errorMessage, ex.Message.ToString());
                return exceptionTokenResponse;
            }
        }

        /// <summary>
        /// From connected app with grant_type = client_credentials 
        /// for app to app authentication
        /// </summary>
        /// <returns></returns>
        public async Task<AnyPointTokenResponse> GetICPAccessToken()
        {

            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://anypoint.mulesoft.com/accounts/api/v2/oauth2/token");
                var connectedApp_ClientID = Startup.AnyPoint_ConnectedApp_ClientId;
                var connectedApp_clientSecret = Startup.AnyPoint_ConnectedApp_ClientSecret;
                string body = "{\n    \"client_id\"     : \"CLIENT_ID\",\n    \"client_secret\" : \"CLIENT_SECRET\",\n    \"grant_type\"    : \"client_credentials\"\n}";
                body = body.Replace("CLIENT_ID", connectedApp_ClientID);
                body = body.Replace("CLIENT_SECRET", connectedApp_clientSecret);
                var content = new StringContent(body, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<AnyPointTokenResponse>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += " &Inner Exception:" + ex.InnerException.Message;
                }
                AnyPointTokenResponse exceptionTokenResponse = new AnyPointTokenResponse();
                exceptionTokenResponse.access_token = "EXCEPTION:" + ex.Message;
                return exceptionTokenResponse;
            }

        }
        public async Task<List<ExchangeAsset>> GetExchangeRestAPIAssetsAsync(string anypointToken)
        {
            List<ExchangeAsset> exchangeAssets = new List<ExchangeAsset>();

            var client = new RestClient("https://anypoint.mulesoft.com/exchange/api/v2/assets/search?organizationId=03ca87cb-d3f4-49ff-969e-b9cc24e19446&types=rest-api");
            var request = new RestRequest();
            request.Method = Method.Get;

            //Headers
            request.AddHeader("Authorization", "bearer " + anypointToken);
            request.AddHeader("Content-Type", "application/json");

            try
            {
                RestResponse exchangeAPIresponse = await client.ExecuteAsync(request);

                exchangeAssets = JsonConvert.DeserializeObject<List<ExchangeAsset>>(exchangeAPIresponse.Content);
            }
            catch (Exception ex)
            {
                var exception = ex.Message;
            }
            return exchangeAssets;
        }

        public async Task<ExchangeAssetResponse> PublishAssetToExchange(ExchangeAssetRequest exchangeModel, string anypointToken)
        {
            ExchangeAssetResponse response = null;

            var createExchangeAssetEndpoint = "https://anypoint.mulesoft.com/exchange/api/v2/organizations/03ca87cb-d3f4-49ff-969e-b9cc24e19446/assets/03ca87cb-d3f4-49ff-969e-b9cc24e19446/";

            createExchangeAssetEndpoint += exchangeModel.AssetName + "/" + exchangeModel.AssetVersion;

            var client = new RestClient(createExchangeAssetEndpoint);

            var request = new RestRequest();
            request.Method = Method.Post;

            //Headers
            request.AddHeader("Authorization", "bearer " + anypointToken);
            request.AddHeader("x-sync-publication", "true");

            //Body params - form-data
            request.AddParameter("name", exchangeModel.AssetName);
            request.AddParameter("description", exchangeModel.Description);
            request.AddParameter("properties.mainFile", exchangeModel.PropertiesMainFile);
            request.AddParameter("properties.apiVersion", exchangeModel.PropertiesAPIVersion);
            request.AddFile("files.oas.json", exchangeModel.OASFileBytes, exchangeModel.OASFileName);
            request.AddParameter("classifier", "oas");

            try
            {
                RestResponse exchangeAPIresponse = await client.ExecuteAsync(request);

                response = JsonConvert.DeserializeObject<ExchangeAssetResponse>(exchangeAPIresponse.Content);
            }
            catch (Exception ex)
            {
                var exception = ex.Message;
            }
            return response;
        }

        public async Task<CreateAPIResponse> CreateAPIManagerAPI(CreateAPIRequest apiModel, string anypointToken)
        {
            CreateAPIResponse response = null;

            var client = new RestClient("https://anypoint.mulesoft.com/apimanager/api/v1/organizations/03ca87cb-d3f4-49ff-969e-b9cc24e19446/environments/3f90ae55-d528-4162-8472-a7c5b2b62494/apis/");
            var request = new RestRequest();
            request.Method = Method.Post;

            //Headers
            request.AddHeader("Authorization", "bearer " + anypointToken);
            request.AddHeader("Content-Type", "application/json");

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            string serializedAPIModel = System.Text.Json.JsonSerializer.Serialize(apiModel, serializeOptions);

            request.AddJsonBody(serializedAPIModel);
            //request.AddParameter("application/json", body, ParameterType.RequestBody);

            try
            {
                RestResponse apiManagerAPIresponse = await client.ExecuteAsync(request);

                response = JsonConvert.DeserializeObject<CreateAPIResponse>(apiManagerAPIresponse.Content);
            }
            catch (Exception ex)
            {
                var exception = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Create API Proxy fro non-mule applications from Exchange
        /// </summary>
        /// <param name="apiModel"></param>
        /// <param name="anypointToken"></param>
        /// <returns></returns>
        public async Task<CreateAPIResponse> CreateAPIManagerProxy(CreateAPIProxyRequest apiModel, string anypointToken)
        {
            CreateAPIResponse response = null;
            //https://anypoint.mulesoft.com/apimanager/xapi/v1/organizations/:orgId/environments/:envId/apis
            var client = new RestClient("https://anypoint.mulesoft.com/apimanager/xapi/v1/organizations/03ca87cb-d3f4-49ff-969e-b9cc24e19446/environments/3f90ae55-d528-4162-8472-a7c5b2b62494/apis");
            var request = new RestRequest();
            request.Method = Method.Post;

            //Headers
            request.AddHeader("Authorization", "bearer " + anypointToken);
            request.AddHeader("Content-Type", "application/json");

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            apiModel.Technology = "mule4";

            //consumer endpoint
            //apiModel.endpointUri = "https://mulesoft-anypoint-dev-apps.corp.hpicloud.net/testgen5/router/rohscheck";

            apiModel.Deployment.EnvironmentId = Startup.AnyPoint_Environment_Id;
            apiModel.Deployment.Type = "RF";
            apiModel.Deployment.ExpectedStatus = "deployed";
            apiModel.Deployment.GatewayVersion = "4.4.0";
            apiModel.Deployment.ApplicationName = apiModel.Spec.AssetId;
            apiModel.Deployment.TargetId = Startup.AnyPoint_RTF_Target_Id;
            apiModel.Deployment.TargetName = Startup.AnyPoint_RTF_Target_Name;
            apiModel.Deployment.TargetType = "runtime-fabric";

            apiModel.Deployment.Target.TargetId = Startup.AnyPoint_RTF_Target_Id;


            apiModel.Endpoint.ProxyUri = "http://0.0.0.0:8081/" + apiModel.Spec.AssetId;
            string serializedAPIModel = System.Text.Json.JsonSerializer.Serialize(apiModel, serializeOptions);

            request.AddJsonBody(serializedAPIModel);
            //request.AddParameter("application/json", body, ParameterType.RequestBody);

            try
            {
                RestResponse apiManagerAPIresponse = await client.ExecuteAsync(request);

                response = JsonConvert.DeserializeObject<CreateAPIResponse>(apiManagerAPIresponse.Content);
            }
            catch (Exception ex)
            {
                var exception = ex.Message;
            }
            return response;
        }

        public async Task<APIDeploymentResponse> DeployAPIManagerAPI(string apiId, string apiName, string anypointToken)
        {
            APIDeploymentResponse response = null;
            string baseURI = "https://anypoint.mulesoft.com/proxies/xapi/v1/organizations/03ca87cb-d3f4-49ff-969e-b9cc24e19446/environments/3f90ae55-d528-4162-8472-a7c5b2b62494/";
            baseURI += "apis/" + apiId + "/deployments";
            var client = new RestClient(baseURI);
            var request = new RestRequest();
            request.Method = Method.Post;

            //Headers
            request.AddHeader("Authorization", "bearer " + anypointToken);
            request.AddHeader("Content-Type", "application/json");

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            APIDeployment apiModel = new APIDeployment();
            apiModel.Name = apiName;
            apiModel.Target = new Data.GatewayModels.Target();
            apiModel.Target.TargetName = Startup.AnyPoint_RTF_Target_Name;
            apiModel.Target.TargetId = Startup.AnyPoint_RTF_Target_Id;
            apiModel.Target.DeploymentSettings = new Data.GatewayModels.DeploymentSettings();
            apiModel.Target.DeploymentSettings.RuntimeVersion = "4.4.0";
            string serializedAPIModel = System.Text.Json.JsonSerializer.Serialize(apiModel, serializeOptions);

            request.AddJsonBody(serializedAPIModel);
            //request.AddParameter("application/json", body, ParameterType.RequestBody);

            try
            {
                RestResponse apiManagerAPIresponse = await client.ExecuteAsync(request);

                response = JsonConvert.DeserializeObject<APIDeploymentResponse>(apiManagerAPIresponse.Content);
            }
            catch (Exception ex)
            {
                var exception = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Creates API in API Manager and also deploys proxy application in runtime manager
        /// </summary>
        /// <param name="apiModel"></param>
        /// <param name="anypointToken"></param>
        /// <returns></returns>
        public async Task<DeploymentRequest> CreateAndDeployAPI(Data.GatewayModels.Deployments.DeploymentRequest deploymentModel, string anypointToken)
        {
            DeploymentRequest response = null;

            var anypointHost = "https://anypoint.mulesoft.com";
            var basePath = "/apimanager/api/v1";
            var envPath = "/organizations/" + Startup.AnyPoint_BusinessGroup_Id + "/environments/" + Startup.AnyPoint_Environment_Id;
            var apiEndPoint = anypointHost + basePath + envPath;

            //var client = new RestClient("https://anypoint.mulesoft.com/apimanager/api/v1/organizations/03ca87cb-d3f4-49ff-969e-b9cc24e19446/environments/3f90ae55-d528-4162-8472-a7c5b2b62494/apis/");
            var client = new RestClient(apiEndPoint + "/apis");
            var request = new RestRequest();
            request.Method = Method.Post;

            //Headers
            request.AddHeader("Authorization", "bearer " + anypointToken);
            request.AddHeader("Content-Type", "application/json");

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            string serializedAPIModel = System.Text.Json.JsonSerializer.Serialize(deploymentModel, serializeOptions);

            request.AddJsonBody(serializedAPIModel);
            //request.AddParameter("application/json", body, ParameterType.RequestBody);

            try
            {
                RestResponse apiManagerAPIresponse = await client.ExecuteAsync(request);

                response = JsonConvert.DeserializeObject<DeploymentRequest>(apiManagerAPIresponse.Content);
            }
            catch (Exception ex)
            {
                var exception = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Runtime Manager
        /// Get deployment for an existing runtime manager deployment
        /// </summary>
        /// <param name="anypointToken"></param>
        /// <returns></returns>
        public async Task<RuntimeDeployments> GetRunTimeDeployments(string anypointToken)
        {

            var anypointHost = "https://anypoint.mulesoft.com";
            var basePath = "/amc/adam/api";
            var envPath = "/organizations/" + Startup.AnyPoint_BusinessGroup_Id + "/environments/" + Startup.AnyPoint_Environment_Id;
            var deploymentsPath = "/deployments";
            var deploymentsEndPoint = anypointHost + basePath + envPath + deploymentsPath;


            RuntimeDeployments deploymentsResponse = new RuntimeDeployments();

            var client = new RestClient(deploymentsEndPoint);
            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddHeader("Authorization", "bearer " + anypointToken);

            try
            {
                RestResponse response = await client.ExecuteAsync(request);
                if (response.IsSuccessful)
                {
                    deploymentsResponse = JsonConvert.DeserializeObject<RuntimeDeployments>(response.Content);
                    Console.WriteLine(response.Content);
                }
            }
            catch (Exception ex)
            {

            }
            return deploymentsResponse;
        }

        /// <summary>
        /// Get a specific API deployment by ID
        /// </summary>
        /// <param name="anypointToken"></param>
        /// <returns></returns>
        public async Task<PatchDeployment> GetRunTimeDeploymentById(string anypointToken, string deploymentId)
        {

            var anypointHost = "https://anypoint.mulesoft.com";
            var basePath = "/amc/adam/api";
            var envPath = "/organizations/" + Startup.AnyPoint_BusinessGroup_Id + "/environments/" + Startup.AnyPoint_Environment_Id;
            var deploymentsPath = "/deployments/" + deploymentId;
            var deploymentsEndPoint = anypointHost + basePath + envPath + deploymentsPath;


            PatchDeployment deploymentsResponse = new PatchDeployment();

            var client = new RestClient(deploymentsEndPoint);
            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddHeader("Authorization", "bearer " + anypointToken);

            try
            {
                RestResponse response = await client.ExecuteAsync(request);
                if (response.IsSuccessful)
                {
                    deploymentsResponse = JsonConvert.DeserializeObject<PatchDeployment>(response.Content);
                    Console.WriteLine(response.Content);
                }
            }
            catch (Exception ex)
            {

            }
            return deploymentsResponse;
        }

        /// <summary>
        /// Runtime Manager
        /// Update ingress for the existing deployment
        /// </summary>
        /// <param name="deploymentModel"></param>
        /// <param name="anypointToken"></param>
        /// <returns></returns>
        public async Task<bool> UpdateIngress(PatchDeployment patchDeployment, string anypointToken)
        {

            var anypointHost = "https://anypoint.mulesoft.com";
            var basePath = "/amc/adam/api";
            var envPath = "/organizations/" + Startup.AnyPoint_BusinessGroup_Id + "/environments/" + Startup.AnyPoint_Environment_Id;
            var deploymentsPath = "/deployments/" + patchDeployment.id;
            var deploymentsEndPoint = anypointHost + basePath + envPath + deploymentsPath;

            var client = new RestClient(deploymentsEndPoint);
            var request = new RestRequest();
            request.Method = Method.Patch; //PATCH request

            //Headers
            request.AddHeader("Authorization", "bearer " + anypointToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-ANYPNT-ENV-ID", Startup.AnyPoint_Environment_Id);
            request.AddHeader("X-ANYPNT-ORG-ID", Startup.AnyPoint_Organization_Id);

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            string serializedDeploymentModel = System.Text.Json.JsonSerializer.Serialize(patchDeployment, serializeOptions);

            request.AddJsonBody(serializedDeploymentModel);
            //request.AddParameter("application/json", body, ParameterType.RequestBody);

            try
            {
                RestResponse deploymentUpdateResponse = await client.ExecuteAsync(request);

                return true;
            }
            catch (Exception ex)
            {
                var exception = ex.Message;
                return false;
            }
        }

        public async Task<APIManagerAPIs> GetAPIManagerAssetsAsync(string anypointToken)
        {
            APIManagerAPIs apiManagerAPIs = new APIManagerAPIs();
            var client = new RestClient("https://anypoint.mulesoft.com/apimanager/api/v1/organizations/03ca87cb-d3f4-49ff-969e-b9cc24e19446/environments/3f90ae55-d528-4162-8472-a7c5b2b62494/apis/");
            var request = new RestRequest();
            request.Method = Method.Get;

            //Headers
            request.AddHeader("Authorization", "bearer " + anypointToken);
            request.AddHeader("Content-Type", "application/json");

            try
            {
                RestResponse deployAPIresponse = await client.ExecuteAsync(request);

                if (deployAPIresponse.IsSuccessful)
                {
                    apiManagerAPIs = JsonConvert.DeserializeObject<APIManagerAPIs>(deployAPIresponse.Content);
                }
                else
                {
                    throw new Exception(deployAPIresponse.StatusDescription);
                }
            }
            catch (Exception ex)
            {
                var exception = ex.Message;
            }
            return apiManagerAPIs;
        }

        public async Task<List<ApplicationResponse>> GetApplicationsAsync(string anypointToken)
        {
            List<ApplicationResponse> applicationList = new List<ApplicationResponse>();

            var client = new RestClient("https://anypoint.mulesoft.com/exchange/api/v2/organizations/03ca87cb-d3f4-49ff-969e-b9cc24e19446/applications");
            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddHeader("Authorization", "bearer " + anypointToken);

            try
            {
                RestResponse response = await client.ExecuteAsync(request);
                if (response.IsSuccessful)
                {
                    applicationList = JsonConvert.DeserializeObject<List<ApplicationResponse>>(response.Content);
                    Console.WriteLine(response.Content);
                }
            }
            catch(Exception ex)
            {
                   
            }
            return applicationList;
        }
        public async Task<ApplicationResponse> CreateAppliationAsync(ApplicationRequest applicationModel, string anypointToken)
        {
            ApplicationResponse applicationResponse = null;

            var client = new RestClient("https://anypoint.mulesoft.com/exchange/api/v2/organizations/03ca87cb-d3f4-49ff-969e-b9cc24e19446/applications");
            var request = new RestRequest();
            request.Method = Method.Post;

            //Headers
            request.AddHeader("Authorization", "bearer " + anypointToken);
            request.AddHeader("Content-Type", "application/json");

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            string serializedAPIModel = System.Text.Json.JsonSerializer.Serialize(applicationModel, serializeOptions);

            request.AddJsonBody(serializedAPIModel);

            try
            {
                RestResponse deployAPIresponse = await client.ExecuteAsync(request);

                if (deployAPIresponse.IsSuccessful)
                {
                    applicationResponse = JsonConvert.DeserializeObject<ApplicationResponse>(deployAPIresponse.Content);
                }
                else
                {
                    throw new Exception(deployAPIresponse.StatusDescription);
                }
            }
            catch (Exception ex)
            {
                var exception = ex.Message;
            }
            return applicationResponse;
        }

        public async Task<bool> CreateAPIContractAsyc(long applicationId,Asset currentAsset, string anypointToken)
        {
            Contract apiContract = new Contract();
            apiContract.ApiId = currentAsset.Apis[0].Id.ToString();
            apiContract.InstanceType = "api";
            apiContract.AcceptedTerms = false;
            apiContract.OrganizationId = Startup.AnyPoint_BusinessGroup_Id;
            apiContract.GroupId = Startup.AnyPoint_BusinessGroup_Id;
            apiContract.AssetId = currentAsset.AssetId;
            apiContract.Version = currentAsset.Apis[0].AssetVersion;
            apiContract.VersionGroup = currentAsset.Apis[0].ProductVersion;

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            string serializedAPIContractModel = System.Text.Json.JsonSerializer.Serialize(apiContract, serializeOptions);

            var client = new RestClient("https://anypoint.mulesoft.com/exchange/api/v2/organizations/03ca87cb-d3f4-49ff-969e-b9cc24e19446/applications/" + applicationId + "/contracts");
            var request = new RestRequest();
            request.Method = Method.Post;

            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", "bearer " + anypointToken);
            request.AddHeader("content-type", "application/json");

            request.AddJsonBody(serializedAPIContractModel);

            try
            {
                RestResponse deployAPIresponse = await client.ExecuteAsync(request);

                if (deployAPIresponse.IsSuccessful)
                {
                    return true;
                    //applicationResponse = JsonConvert.DeserializeObject<ApplicationResponse>(deployAPIresponse.Content);
                }
                else
                {
                    throw new Exception(deployAPIresponse.StatusDescription);
                }
            }
            catch (Exception ex)
            {
                var exception = ex.Message;
            }
            return false;
        }
    }
}

using API_Pet.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace API_Pet.StepDefinition
{
    [Binding]
    public class PetStore
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef
        RestClient client;
        RestRequest request;
        IRestResponse response;
        JObject postResult;


        [Given(@"I will call the base uri  ""(.*)""")]
        public void GivenIWillCallTheBaseUri(string baseuri)
        {
            client = new RestClient(baseuri);

        }

        [When(@"I perform POST operation for ""(.*)"" on petstore")]
        public void WhenIPerformPOSTOperationForOnPetstore(string endpoint, Table table)
        {
            request = new RestRequest(endpoint, Method.POST);
            request.RequestFormat = DataFormat.Json;
            DateTime dt = DateTime.Now;
            var Value = table.CreateInstance<Order>();
            request.AddJsonBody(new Order()
            {
                id = Value.id,
                petId = Value.petId,
                quantity = Value.quantity,
                shipDate = dt,
                status = Value.status,
                complete = Value.complete
            });
        }


        [When(@"I perform GET operation for ""(.*)"" with orderId (.*)")]
        public void WhenIPerformGETOperationForWithOrderId(string endpoint, string orderId)
        {
            request = new RestRequest(endpoint, Method.GET);
            request.AddParameter("orderId", orderId, ParameterType.UrlSegment);
            request.RequestFormat = DataFormat.Json;

        }



        [When(@"I perform DELETE operation for""(.*)"" with orderId (.*)")]
        public void WhenIPerformDELETEOperationForWithOrderId(string endpoint, string orderId)
        {
            request = new RestRequest(endpoint, Method.DELETE);
            request.AddParameter("orderId", orderId, ParameterType.UrlSegment);
            request.RequestFormat = DataFormat.Json;
        }


        [When(@"I perform POST Operation for ""(.*)"" using body (.*), (.*), (.*), ""(.*)"", ""(.*)""")]
        public void WhenIPerformPOSTOperationForUsingBody(string endpoint, int id, int petId, int quantity, string status, string complete)
        {
            request = new RestRequest(endpoint, Method.POST);
            request.RequestFormat = DataFormat.Json;
            bool comp = bool.Parse(complete);
            DateTime dt = DateTime.Now;
            request.AddJsonBody(new Order()
            {
                id = id,
                petId = petId,
                quantity = quantity,
                shipDate = dt,
                status = status,
                complete = comp
            }

                );
            response = client.Execute(request);
            postResult = JObject.Parse(response.Content);
        }


        [When(@"I perform GET operation for ""(.*)"" by giving id (.*)")]
        public void WhenIPerformGETOperationForByGivingId(string endpoint, int orderId)
        {
            request = new RestRequest(endpoint, Method.GET);
            request.AddParameter("orderId", orderId, ParameterType.UrlSegment);
            request.RequestFormat = DataFormat.Json;
        }

        [Then(@"I should see the response created during POST operation with status code as (.*)")]
        public void ThenIShouldSeeTheResponseCreatedDuringPOSTOperationWithStatusCodeAs(int status)
        {
            response = client.Execute(request);
            JObject objs = JObject.Parse(response.Content);
            int StatusCode = (int)response.StatusCode;
            Console.WriteLine(objs);

            Assert.AreEqual(postResult.ToString(), objs.ToString(), "Post request not matched with Get Response");
            Assert.AreEqual(status, StatusCode, "Invalid Status code");
        }

        [Then(@"I should see the response with status code as (.*) ok")]
        public void ThenIShouldSeeTheResponseWithStatusCodeAsOk(int status)
        {
            response = client.Execute(request);
            JObject objs = JObject.Parse(response.Content);
            int StatusCode = (int)response.StatusCode;
            Console.WriteLine(objs);
            Assert.AreEqual(status, StatusCode, "Invalid Status code");
        }

    }



}

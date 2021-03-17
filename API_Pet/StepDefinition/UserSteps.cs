using API_Pet.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace API_Pet.StepDefinition
{
    [Binding]
    public class UserSteps
    {
        RestClient client;
        RestRequest request;
        IRestResponse response;

        [Given(@"I would call the base uri  ""(.*)""")]
        public void GivenIWouldCallTheBaseUri(string baseuri)
        {
            client = new RestClient(baseuri);
        }
        
       
        [When(@"I Perform POST request for ""(.*)""")]
        public void WhenIPerformPOSTRequestFor(string endpoint)
        {
            request = new RestRequest(endpoint, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new User()
            {
                id = 7,
                username = "tannu",
                firstName = "Tanvir",
                lastName = "Singh",
                email = "tanvir@gmail.com",
                password = "tan#09",
                phone = "9876543221",
                userStatus = 1
            });
        }


        [When(@"I perform PUT Operation for ""(.*)"" with username ""(.*)""")]
        public void WhenIPerformPUTOperationForWithUsername(string endpoint, string username, Table table)
        {
            request = new RestRequest(endpoint, Method.PUT);
            request.AddParameter("username", username, ParameterType.UrlSegment);
            request.RequestFormat = DataFormat.Json;

            var userValue = table.CreateInstance<User>();
            request.AddJsonBody(new User() 
            {  
                id = userValue.id, 
                username = userValue.username, 
                firstName = userValue.firstName,
                lastName = userValue.lastName, 
                email = userValue.email, 
                password = userValue.password, 
                phone = userValue.phone, 
                userStatus = userValue.userStatus 
            });

        }

        
        [Then(@"I should see response with status code as (.*) ok")]
        public void ThenIShouldSeeResponseWithStatusCodeAsOk(int status)
        {
            response = client.Execute(request);
            JObject objs = JObject.Parse(response.Content);
            int StatusCode = (int)response.StatusCode;
            Console.WriteLine(objs);
            Assert.AreEqual(status, StatusCode, "Invalid Status code");
        }
        [Then(@"I should see successful response with status code as (.*) ok")]
        public void ThenIShouldSeeSuccessfulResponseWithStatusCodeAsOk(int status)
        {
            response = client.Execute(request);
            JObject objs = JObject.Parse(response.Content);
            int StatusCode = (int)response.StatusCode;
            Console.WriteLine(objs);
            Assert.AreEqual(status, StatusCode, "Invalid Status code");
        }
        

    }


}


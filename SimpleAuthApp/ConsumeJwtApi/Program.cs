// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using RestSharp;

Console.WriteLine("-------------- Getting a Auth Token ---------------");

RestClient client = new RestClient("https://localhost:7064/api/");
RestRequest request1 = new RestRequest("Name/authenticate", Method.Post)
                            .AddJsonBody(new { Username = "test1", Password = "password1" });
Token response1 = JsonConvert.DeserializeObject<Token>(client.Execute<Token>(request1).Content);
var token = response1.JwtToken;
Console.WriteLine($"Token for further requests: {token}");

Console.WriteLine("-------------- An Authorized Request passing Auth Token ---------------");
RestRequest request2 = new RestRequest("Name", Method.Get);
client.AddDefaultHeader("Authorization", $"Bearer {token}");
var response2 =  client.Execute(request2).Content;
Console.WriteLine(response2);

Console.WriteLine("-------------- Refreshing the previously generated Auth Token ---------------");

RestRequest request3 = new RestRequest("Name/refresh", Method.Post)
                            .AddJsonBody(response1);
var temp = client.Execute<Token>(request3).Content;
Token response3 = JsonConvert.DeserializeObject<Token>(temp);
var newtoken = response3.JwtToken;
Console.WriteLine($"Refreshed Token: {newtoken}");
Console.ReadLine();

public class Token
{
    public string JwtToken { get; set; }
    public string RefreshToken { get; set; }
}


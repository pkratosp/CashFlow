using System.Net.Http.Json;
using System.Text.Json;
using CashFlow.Communication.requests;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;
using WebApi.Test.InlineData;
using System.Globalization;
using System.Net.Http.Headers;

namespace WebApi.Test.Login;

public class LoginTest: IClassFixture<CustomWebApplicationFactory>
{
    private const string METHOD = "api/Login";
    private readonly HttpClient _httpClient;
    private readonly string _email;
    private readonly string _password;
    private readonly string _name;

    public LoginTest(CustomWebApplicationFactory customWebApplicationFactory)
    {
        _httpClient = customWebApplicationFactory.CreateClient();
        _email = customWebApplicationFactory.GetGemail();
        _password = customWebApplicationFactory.GetPassword();
        _name = customWebApplicationFactory.GetName();
    }


    [Fact]
    public async Task Success()
    {
        var request = new RequestRegisterUserJson 
        { 
            Email = _email,
            Password = _password
        };

        var result = await _httpClient.PostAsJsonAsync(METHOD, request);

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        var ResponseBody = await result.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(ResponseBody);

        responseData.RootElement.GetProperty("name").GetString().Should().Be(_name);
        responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
    }


    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Credentails_Invalid(string cultureInfo)
    {
        var request = RequestLoginJsonBuilder.Build();

        _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(cultureInfo));

        var result = await _httpClient.PostAsJsonAsync(METHOD, request);

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessage").EnumerateArray();

        var expenctedMessage = ResourceErrorMessages.ResourceManager.GetString("EMAIL_OR_PASSWORD_INVALID", new CultureInfo(cultureInfo));

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expenctedMessage));
    }
}

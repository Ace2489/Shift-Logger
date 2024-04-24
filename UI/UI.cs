using System.Net.Http.Headers;
using InputHelpers;

namespace UI;

public class UIApp
{
    readonly GetTypesFromTerminal getTypes;
    readonly HttpClient client;
    public UIApp(string url)
    {
        getTypes = new();
        client = new()
        {
            BaseAddress = new Uri(url)
        };
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task Run()
    {
        HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
        Console.WriteLine(response.Content.);
        Console.WriteLine("Gotten");

    }
}

using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;

Console.WriteLine("Provide a text question:");
var question = Console.ReadLine();

// Call Open AI
var answer = CallOpenAI(250, question, "text-davinci-002", 0.7, 1, 0, 0);
Console.WriteLine(answer);

/// <summary>
/// Based on https://beta.openai.com/playground
/// Method used to query Open AI API, check your Parameters to specify what the response should be
/// </summary>
/// <param name="tokens">Maximum amount of tokens the API can use</param>
/// <param name="input">The question sent to Open AI</param>
/// <param name="engine">Which AI engine to use to process the requests</param>
/// <param name="temperature">Controls randomness: Result close to 0, more predictable response.</param>
/// <param name="topP">Controls diversity of options, default 1</param>
/// <param name="frequencyPenalty">Higher number = less repetitive response </param>
/// <param name="presencePenalty">Higher number = more likely to start talking about a different topic</param>
/// <returns>Response from Open AI</returns>
string CallOpenAI(int tokens, string input, string engine, double temperature, int topP, int frequencyPenalty, int presencePenalty)
{
    var openAiKey = "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";

    var apiCall = "https://api.openai.com/v1/engines/" + engine + "/completions";

    try
    {
        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), apiCall))
            {
                request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + openAiKey);
                request.Content = new StringContent("{\n  \"prompt\": \"" + input + "\",\n  \"temperature\": " +
                                                    temperature.ToString(CultureInfo.InvariantCulture) + 
                                                    ",\n  \"max_tokens\": " + tokens + 
                                                    ",\n  \"top_p\": " + topP +
                                                    ",\n  \"frequency_penalty\": " + frequencyPenalty + 
                                                    ",\n  \"presence_penalty\": " + presencePenalty + "\n}");

                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                var response = httpClient.SendAsync(request).Result;
                var json = response.Content.ReadAsStringAsync().Result;
                
                OpenAIResponse myDeserializedClass = JsonSerializer.Deserialize<OpenAIResponse>(json);

                return myDeserializedClass.choices[0].text;
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

    return null;
}
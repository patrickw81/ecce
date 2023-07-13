using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;


namespace ecce
{
    public class ChatGpt
    {
        private readonly string _apiKey;
        private readonly string _engine;

        public ChatGpt(string apiKey, string engine)
        {
            _apiKey = apiKey;
            _engine = engine;
           
        }

        public async Task<string> SendChatGPTRequest(string message)
        {
            string apiUrl = $"https://api.openai.com/v1/completions";
           
            using (var httpClient = new HttpClient())
            {


                
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

                var requestBody = new
                {
                    model = "text-davinci-003",
                    prompt = message,
                    max_tokens = 580,
                    temperature = 0.5,
                    top_p = 1,
                    frequency_penalty = 0,
                    presence_penalty = 0
                };
                //Debug.WriteLine(message);

                var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
                var httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
               
                var response = await httpClient.PostAsync(apiUrl, httpContent);
                //Debug.WriteLine(jsonContent.ToString());

                var responseJson = await response.Content.ReadAsStringAsync();
                //Debug.WriteLine(responseJson.ToString());
               
                dynamic responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject(responseJson);
                
                string chatGPTResponse = responseObject.choices[0].text;
                
               
                //Debug.WriteLine(chatGPTResponse);
                


                return chatGPTResponse;
            }
        }
    }
}

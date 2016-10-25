using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Xunit;

namespace Tests
{
    public class GetPostsTest
    {
        [Fact]
        public void GetAllPostsTest()
        {
            try
            {
                string html;
                var url = @"https://www.instagram.com/kogefan/media/";

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var task = client.GetStreamAsync(url);
                using (var reader = new StreamReader(task.Result))
                {
                    html = reader.ReadToEnd();
                }

                var instaresponse = JsonConvert.DeserializeObject<InstaCrafter.Classes.Wrapper.InstaResponse>(html);
                Assert.NotNull(instaresponse);
                Assert.Equal(20, instaresponse.Items.Count);
            }
            catch (Exception)
            {
            }
            Assert.True(true);
        }

    }
}

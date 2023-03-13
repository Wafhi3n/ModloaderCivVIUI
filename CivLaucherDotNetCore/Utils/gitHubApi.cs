using Microsoft.Extensions.DependencyInjection;
using ModloaderClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace ModLoader.Utils
{
    abstract class GitHubApi
    {
        public static async Task<string> getDataFromApi(string call)
        {
            using (var client = new HttpClient())
            {



                IServiceScope services = Services.Service.CreateScope();
                DbSet < Config > config = services.ServiceProvider.GetRequiredService<DbSet<Config>>();
                string apiUrl = config.Find("apiUrl").Value;
                




                client.BaseAddress = new Uri(apiUrl + call);
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
                HttpResponseMessage response = await client.GetAsync(apiUrl + call).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string retour = await response.Content.ReadAsStringAsync();
                    //storeDataJson(call, retour);
                    return retour;
                }
                else
                {
                    return null;
                }
            }

        }
        public async void getLastTagNameReleaseFromRepo()
        {
            try
            {
                string data = await GitHubApi.getDataFromApi("/releases/latest").ConfigureAwait(false);

                if (data == null)
                {
                }
                else
                {
                    JsonApiGitReturnLastRelease returnApi = JsonConvert.DeserializeObject<JsonApiGitReturnLastRelease>(data);
                }
            }
            catch (TaskCanceledException ex)
            {
                throw ex;
            }


        }
    }
}

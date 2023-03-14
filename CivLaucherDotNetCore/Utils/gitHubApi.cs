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
using CivLaucherDotNetCore.Controleur;
using System.IO;
using ModloaderClass.Model;
using ModLoader.Model;
using System.Windows.Markup;

namespace ModLoader.Utils
{
    abstract class GitHubApi
    {
        public static async Task<string> getDataFromApi(string call)
        {
            using (var client = new HttpClient())
            {





                client.BaseAddress = new Uri(call);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
                client.DefaultRequestHeaders.Add("Authorization", "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");

                try
                {


                    HttpResponseMessage response = await client.GetAsync(call);
                        response.EnsureSuccessStatusCode();
                    Console.WriteLine("*****" + call + "*****");


                    Console.WriteLine(response.IsSuccessStatusCode+"TEST");

                    if (response.IsSuccessStatusCode)
                    {

                        string retour = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("response" + response.Content + "response");

                        Console.WriteLine(retour);


                        return retour;

                    }
                    else
                    {
                        Console.WriteLine("echec : " + call);
                        Console.WriteLine(response);
                        
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("echec : " + call);
                }
                return null;
            }

        }

        public static void storeDataJson(string call, string retour)
        {
            string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Modloader", "cache");

            if (!Directory.Exists(dir)) { Directory.CreateDirectory(dir); }
            if (Directory.Exists(dir))
            {
                IServiceScope services = Services.Service.CreateScope();
                DbSet<GitHubCallApiCache> DBapi = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContext>().gitHub;
                GitHubCallApiCache gc = DBapi.ToList().Find(a => a.call == call);
                Console.WriteLine("call:"+call);

                if (gc != null)
                {
                    Console.WriteLine();

                    gc.value = retour;
                    gc.date = DateTime.Now.ToString();
                    DBapi.Update(gc);
                    services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();
                    Console.WriteLine("storeUPdate");

                }
                else
                {
                    gc = new GitHubCallApiCache();
                    gc.call = call;
                    gc.value = retour;
                    gc.date = DateTime.Now.ToString();
                    DBapi.Add(gc);
                    services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();
                    Console.WriteLine("storeADD");
                }

            }

        }







        public static async Task<string> getLastTagNameReleaseFromRepo(ModController m)
        {
            String uriRepo = m.apiUrl();
            string tag = "";
            string data = "";
            IServiceScope services = Services.Service.CreateScope();
            DbSet<GitHubCallApiCache> DBapi = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContext>().gitHub;
            DbSet<Config> DBconfig = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContext>().config;
            String delayApiString = DBconfig.ToList().First(a => a.Key == "delayAPI").Value;
            GitHubCallApiCache gc = DBapi.ToList().Find(a => a.call == m.m.modID + "_releases_latest");
            TimeSpan delayAPI = new TimeSpan(0, Int32.Parse(delayApiString), 0);
            //Console.WriteLine("change :" + (DateTime.Parse(gc.date)));
            //Console.WriteLine(DateTime.Now);


            if (gc == null || (DateTime.Parse(gc.date) + delayAPI) < DateTime.Now)
            {
                //Console.WriteLine("change :" + (DateTime.Parse(gc.date) + delayAPI));

                try
                {
                    data = await GitHubApi.getDataFromApi(uriRepo + "/releases/latest").ConfigureAwait(false);


                }
                catch (TaskCanceledException ex)
                {
                    throw ex;
                }



            }
            else
            {
                //Console.WriteLine("don't change:" + (DateTime.Parse(gc.date) + delayAPI));

                data = gc.value;


            }


            if (data == null)
            {

            }
            else
            {
                JsonApiGitReturnLastRelease returnApi = JsonConvert.DeserializeObject<JsonApiGitReturnLastRelease>(data);

                //Console.WriteLine(returnApi.tag_name);



                storeDataJson(m.m.modID + "_releases_latest", data);
                tag = returnApi.tag_name;
            }





            return tag;
        }
   






public static async Task<string> getTagsFromRepo(ModController m)
        {



            String uriRepo = m.apiUrl();
            string tag = "";
            string data = "";
            IServiceScope services = Services.Service.CreateScope();
            DbSet<GitHubCallApiCache> DBapi = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContext>().gitHub;
            DbSet<Config> DBconfig = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContext>().config;
            String delayApiString = DBconfig.ToList().First(a => a.Key == "delayAPI").Value;
            GitHubCallApiCache gc = DBapi.ToList().Find(a => a.call == m.m.modID + "_tags");
            TimeSpan delayAPI = new TimeSpan(0, Int32.Parse(delayApiString), 0);
            //Console.WriteLine("change :" + (DateTime.Parse(gc.date)));
            //Console.WriteLine(DateTime.Now);


            if (gc == null || (DateTime.Parse(gc.date) + delayAPI) < DateTime.Now)
            {
                //Console.WriteLine("change :" + (DateTime.Parse(gc.date) + delayAPI));

                try
                {


                    data = await GitHubApi.getDataFromApi(uriRepo + "/tags");
                    Console.WriteLine(data);


                }
                catch (TaskCanceledException ex)
                {
                    throw ex;
                }



            }
            else
            {
                Console.WriteLine("pb");

                //Console.WriteLine("don't change:" + (DateTime.Parse(gc.date) + delayAPI));

                data = gc.value;


            }


            if (data == null)
            {
                Console.WriteLine("NULL");

            }
            else
            {
                tags returnApi = JsonConvert.DeserializeObject<tags>(data);

                Console.WriteLine(returnApi.name);



                storeDataJson(m.m.modID + "_tags", data);
                //tag = returnApi.tag_name;
            }





            return tag;
        }

    } 
}
    














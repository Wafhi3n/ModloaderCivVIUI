using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.IO;
using ModloaderClass.Model;
using System.Windows.Markup;
using System.Web;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ModLoader.Controller;
using System.Runtime.CompilerServices;

namespace ModLoader.Utils
{
    public abstract class GitHubApi
    {
        public async static Task<string>  getDataFromApi(string call)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(call);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.UserAgent.TryParseAdd("Request");
                // Console.WriteLine("*****" + call + "*****");
                //client.DefaultRequestHeaders.Add("Authorization", "");
                //client.Timeout =  new TimeSpan(0,0,5);



                HttpResponseMessage response = await client.GetAsync(call);
                
                    ;
                   response.EnsureSuccessStatusCode();

                return response.Content.ReadAsStringAsync().Result;
                    
                
            }

        }

        public static void storeDataJson(string call, string retour)
        {
                IServiceScope services = ServicesModloader.Service.CreateScope();
                DbSet<GitHubCallApiCache> DBapi = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().gitHub;
                GitHubCallApiCache gc = DBapi.ToList().Find(a => a.call == call);
               Console.WriteLine("call:"+call);

                if (gc != null)
                {
                    //Console.WriteLine();
                    gc.call = call;
                    gc.value = retour;
                    gc.date = DateTime.Now.ToString();
                    DBapi.Update(gc);
                    services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();
                services.ServiceProvider.GetService<DBConfigurationContext>().Dispose();

                //Console.WriteLine("storeUPdate");

            }
            else
                {
                    gc = new GitHubCallApiCache();
                    gc.call = call;
                    gc.value = retour;
                    gc.date = DateTime.Now.ToString();
                    DBapi.Add(gc);

                    services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();
                services.ServiceProvider.GetService<DBConfigurationContext>().Dispose();

                //Console.WriteLine("storeADD");
            }

        }





        public async static Task<string> getResultFromCallApiOrDB(string call,string req, ModGitController m) {

            String uriRepo = m.apiUrl();

            string tag = "";
            string data = "";
            IServiceScope services = ServicesModloader.Service.CreateScope();
            DbSet<GitHubCallApiCache> DBapi = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().gitHub;
            DbSet<Config> DBconfig = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().config;
            String delayApiString = DBconfig.ToList().FirstOrDefault(a => a.Key == "delayAPI").Value;
            string store = m.modId + call;
            GitHubCallApiCache gc = DBapi.ToList().FirstOrDefault (a => a.call == store );
            TimeSpan delayAPI = new TimeSpan(0, Int32.Parse(delayApiString), 0);


            if (   ( gc != null) &&  gc.value != null  &&  ((DateTime.Parse(gc.date)) >= (DateTime.Now - delayAPI))    )
            {
                    data = gc.value;
            }
            else
            {
                data = await GitHubApi.getDataFromApi(uriRepo + req);

                Console.WriteLine(data);
                storeDataJson(store, data);

                services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();
                services.ServiceProvider.GetService<DBConfigurationContext>().Dispose();

            }
            return data;
        }

        public static async void getLastTagNameReleaseFromRepo(ModGitController m)
        {



            string call = "_releases_latest";
            string data = await getResultFromCallApiOrDB(call,"/releases/latest", m);


            JsonApiGitReturnLastRelease returnApi = JsonConvert.DeserializeObject<JsonApiGitReturnLastRelease>(data);
            m.UpdatelastTag(returnApi.tag_name);


        }
   






        public static async void GetTagsFromRepo(ModGitController m)
        { 



                 string call = "_tags";
                string data = await getResultFromCallApiOrDB(call, "/tags", m);


                List<Tag> returnApi = JsonConvert.DeserializeObject<List<Tag>>(data);




                
                
                foreach(Tag t in returnApi){
                    m.AddTags(t.name);
                }

                if (m.vue.st != null && m.IsUpdateAviable())
                {
                    m.vue.st.setTextUpdateAviable(m.vue.InfoLabelModCanUpdate());
                }


            }





        }

    } 

    














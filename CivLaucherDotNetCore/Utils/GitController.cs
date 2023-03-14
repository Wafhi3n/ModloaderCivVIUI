using CivLaucherDotNetCore.Controleur;
using CivLaucherDotNetCore.Vue.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModLoader.Model;
using ModloaderClass;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input.Manipulations;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ModLoader.Utils
{
    public abstract class GitController
    {
        public static async void GetModTag(ModController m)
        {

            IServiceScope services = Services.Service.CreateScope();
            DbSet<Mod> DBmod = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContext>().mod;
            Mod m2 = DBmod.ToList().Find(a => a.path == m.path);
            string command = "describe --tag";
            SwapToModDirectory(m);
            string tag = await Task.Run(() => StartGitCommand(command, m));



            m.tag = tag;
            //    m2.vue.tag =
            m2.tag = tag;
            DBmod.Update(m2);
            services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();

   
            m.vue.tagSelect = m.tag;

           

        }
        /*public static async void UpdateCurrentTag(ModController m)
        {

            IServiceScope services = Services.Service.CreateScope();
            DbSet<Mod> DBmod = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContext>().mod;
            Mod m2 = DBmod.ToList().Find(a => a.path == m.path);
            string command = "describe --tag";
            SwapToModDirectory(m);
            string tag = await Task.Run(() => StartGitCommand(command));



            m2.tag = tag;
            DBmod.Update(m2);
            services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();
            //return test;
        }*/

        


        public static void SwapToModDirectory(ModController m) {
            Directory.SetCurrentDirectory(m.path);
        }
        public static string StartGitCommand(string command,ModController mod)
        {
            string test = "";
            try
            {
                //Directory.SetCurrentDirectory(mod.path);
                //Console.WriteLine(command);
                var process = new Process
                {

                    StartInfo =
                    {
                        FileName = "git",
                        Arguments = command,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        WorkingDirectory = mod.path
                        //ErrorDialog = false,
                        //RedirectStandardError = true,
                        //RedirectStandardInput = true,
                        //UseShellExecute = true,
                        //WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
                    }
                };

                process.Start();
                test = process.StandardOutput.ReadToEnd();
                test = test.Replace("\n", ""); //add a line terminating ;
                process.WaitForExit();
                process.Dispose();
                return test;
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("The specified directory does not exist. {0}", e);
                return null;
            }
            
        }
        public static void InstallWithGit(ModController mod)
        {

            if (String.IsNullOrEmpty(mod.path) || !Directory.Exists(mod.path))
            {


                string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games", "Sid Meier's Civilization VI", "Mods");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                    mod.path = dir;

                }
                if (Directory.Exists(dir))
                {

                    //Set the current directory.
                    Directory.SetCurrentDirectory(dir);
                    IServiceScope services = Services.Service.CreateScope();
                    DbSet<Mod> DBmod = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContext>().mod;

                    //Set the current directory.
                    Directory.SetCurrentDirectory(mod.path);
                    Mod m2 = DBmod.ToList().Find(a => a.path == mod.path);




                    string command = "clone " + mod.UriRepo();
                    //Console.WriteLine(command);
                    StartGitCommand(command,mod);

                    //mod.tag = tag;
                    //    m2.vue.tag =
                    //m2.tag = tag;
                    mod.path = Path.Combine(dir, mod.depot);

                    m2.path = mod.path;
                    DBmod.Update(m2);
                    //m.vue.tagSelect = m.tag;
                    services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();


                }

            }
        }
        public static void ResetRepotGit(ModController mod)
        {
            var tcs = new TaskCompletionSource<int>();

            if (!String.IsNullOrEmpty(mod.path) || !Directory.Exists(mod.path))
            {
                try
                {
                    Directory.SetCurrentDirectory(mod.path);

                    StartGitCommand("clean -fxd", mod);
                    StartGitCommand("reset --hard", mod);
                }
                catch (DirectoryNotFoundException e)
                {
                    Console.WriteLine("The specified directory does not exist. {0}", e);
                }
            }
        }

        public static Boolean isInstalled(ModController mod)
        {
            try
            {
                
                if (Directory.Exists(Path.Combine(mod.path, ".git")))
                {
                    if ( mod.tag == null)
                    {

                        GetModTag(mod);
                        //mod.vue.tagSelect = mod.tag;




                    }

                    return true;
               
                }

            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("FALSE");
                return false;
            }
            return false;
        }

        public static void UpdateToTag(ModController m, string tag)
        {
            if (!String.IsNullOrEmpty(m.path) && Directory.Exists(m.path))
            {


                //string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games", "Sid Meier's Civilization VI", "Mods");

                if (Directory.Exists(m.path))
                {

                    IServiceScope services = Services.Service.CreateScope();
                    DbSet<Mod> DBmod = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContext>().mod;

                    //Set the current directory.
                    Directory.SetCurrentDirectory(m.path);
                    Mod m2 = DBmod.ToList().Find(a => a.path == m.path);
                    //IServiceScope services = Services.Service.CreateScope();
                    //DbSet<Config> config = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContext>().config;




                    string command = "-c advice.detachedHead=false checkout " + tag;
                    StartGitCommand(command, m);
                    m.tag = tag;
                    //    m2.vue.tag =
                    m2.tag = tag;
                    DBmod.Update(m2);
                    services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();


                    m.vue.tagSelect = m.tag;


                }

            }
        }


        






















        public static  void fetchAll(ModController m)
        {


           


        }


        public static void GetLastTag(ModController m)
        {

             GitHubApi.getLastTagNameReleaseFromRepo(m);
           

            //return tag;
        }
        public static void updateBranchToTagAsync(ModController m,string tag)
        {

            if (m.isInstalled())
            {
                if (tag != null)
                {
                     UpdateToTag(m, tag);
                    //ommands.Checkout(repository, t.Target.Sha);

                    //m.version = t.FriendlyName;
                }
                else
                {
                    //Tag LastTag = tags.Last();
                    //Commands.Checkout(repository, LastTag.Target.Sha);
                    //m.version = LastTag.FriendlyName;
                     UpdateToTag(m, m.lastag);

                }

            }
            else
            {
                 InstallWithGit(m);
            }
        }
    }


    




        /*public async Task getReleaseTagsFromApi()
{//            LastTag = tags.First();


    if (this.isInstalled())
    {
        try
        {
            string data;
            string storedData = await loadStoredDataJsonAsync("/releases");

            if (storedData != null && storedData != "")
            {
                data = storedData;
            }
            else
            {
                data = await getDataFromApi("/releases").ConfigureAwait(false);

            }

            var returnApis = JsonConvert.DeserializeObject<List<JsonApiGitReturnLastRelease>>(data);
            //Lookup<string, Tag> tg = (Lookup<string, Tag>)repository.Tags.ToLookup(t => t.FriendlyName);
            Lookup<string, JsonApiGitReturnLastRelease> tg2 = (Lookup<string, JsonApiGitReturnLastRelease>)returnApis.ToLookup(t => t.tag_name);
            foreach (IGrouping<string, JsonApiGitReturnLastRelease> tag2 in tg2)
            {
                foreach (IGrouping<string, Tag> tag in tg)
                {
                    if (tag2.Key == tag.Key)
                    {
                        App.Current.Dispatcher.Invoke((Action)delegate //<--- HERE
                        {
                            tags.Add(tag.First());
                        });

                        continue;
                    }
                }
            }
            LastTag = tags.First();

        }
        catch (TaskCanceledException ex)
        {
            throw ex;
        }
    }
}*/
    }


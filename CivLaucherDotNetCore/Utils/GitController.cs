using CivLaucherDotNetCore.Controleur;
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

namespace ModLoader.Utils
{
    public abstract class GitController
    {
        public static string GetModTag(ModController m)
        {

            IServiceScope services = Services.Service.CreateScope();
            DbSet<Mod> DBmod = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContext>().mod;
            Mod m2 = DBmod.ToList().Find(a => a.path == m.m.path);
            string command = "describe --tag";
            SwapToModDirectory(m);
            string s = StartGitCommand(command);
            m2.tag = s;
            DBmod.Update(m2);
            services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();
            return s;
        }

        public static void SwapToModDirectory(ModController m) {
            Directory.SetCurrentDirectory(m.m.path);
        }
        public static string StartGitCommand(string command)
        {
            string test = "";
            try
            {
                Console.WriteLine(command);
                var process = new Process
                {

                    StartInfo =
                    {
                        FileName = "git",
                        Arguments = command,
                        //CreateNoWindow = true,
                        RedirectStandardOutput = true,
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
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("The specified directory does not exist. {0}", e);
            }
            return test;
        }
        public static void InstallWithGit(ModController mod)
        {

            if (String.IsNullOrEmpty(mod.m.path) || !Directory.Exists(mod.m.path))
            {


                string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games", "Sid Meier's Civilization VI", "Mods");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);

                }
                if (Directory.Exists(dir))
                {

                    //Set the current directory.
                    Directory.SetCurrentDirectory(dir);
                    IServiceScope services = Services.Service.CreateScope();
                    //DbSet<Config> config = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContext>().config;



                
                    string command = "clone " + mod.UriRepo();
                    //Console.WriteLine(command);
                    StartGitCommand(command);



                    mod.m.path = Path.Combine(dir,mod.m.depot);
                    services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();


                }

            }
        }
        public static void ResetRepotGit(Mod mod)
        {
            var tcs = new TaskCompletionSource<int>();

            if (!String.IsNullOrEmpty(mod.path) || !Directory.Exists(mod.path))
            {
                try
                {
                    Directory.SetCurrentDirectory(mod.path);

                    StartGitCommand("clean -fxd");
                    StartGitCommand("reset --hard");
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
                
                if (Directory.Exists(Path.Combine(mod.m.path, ".git")))
                {
                    if ( mod.m.tag == null)
                    {

                        GetModTag(mod);

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

        public static void UpdateToTag(Mod mod,string tag)
        {
            if (!String.IsNullOrEmpty(mod.path) && Directory.Exists(mod.path))
            {


                //string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games", "Sid Meier's Civilization VI", "Mods");

                if (Directory.Exists(mod.path))
                {

                    //Set the current directory.
                    Directory.SetCurrentDirectory(mod.path);
                    //IServiceScope services = Services.Service.CreateScope();
                    //DbSet<Config> config = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContext>().config;



                    
                    string command = "-c advice.detachedHead=false checkout " + tag;
                    StartGitCommand(command);

                }

            }
        }

        public static  void fetchAll(ModController m)
        {


           


        }


        public static string GetLastTag(ModController m)
        {

            string tag = GitHubApi.getLastTagNameReleaseFromRepo(m).Result;
           
            m.UpdatelastTag(tag);

            return tag;
        }
    }


    


        /*internal async Task updateBranchToTagAsync(Tag t)
{

    if (isInstalled())
    {
        if (t != null)
        {

            Commands.Checkout(repository, t.Target.Sha);
            //m.version = t.FriendlyName;
        }
        else
        {
            Tag LastTag = tags.Last();
            Commands.Checkout(repository, LastTag.Target.Sha);
            //m.version = LastTag.FriendlyName;
        }

    }
    else
    {
        await cloneMod();
    }
}*/

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


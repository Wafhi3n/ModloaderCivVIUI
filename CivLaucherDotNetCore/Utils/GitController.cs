using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModLoader.Controller;
using ModloaderClass.Model;
using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input.Manipulations;
using System.Xml;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ModLoader.Utils
{
    public abstract class GitController
    {
        public static async void GetModTag(ModGitController m)
        {

            IServiceScope services = ServicesModloader.Service.CreateScope();
            DbSet<ModGit> DBmod = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().mod;
            ModGit m2 = DBmod.ToList().Find(a => a.path == m.path);
            string command = "describe --tag";
            SwapToModDirectory(m);
            string tag = await Task.Run(() => StartGitCommand(command, m.path));



            m.tag = tag;
            //    m2.vue.tag =
            m2.tag = tag;
            DBmod.Update(m2);
            services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();
            services.ServiceProvider.GetService<DBConfigurationContext>().Dispose();

            m.vue.tagSelect = m.tag;



        }
  
        public static void SwapToModDirectory(ModGitController m)
        {
            Directory.SetCurrentDirectory(m.path);

        }
        public static string StartGitCommand(string command, string workingDIR)
        {

            if (Directory.Exists(workingDIR))
            {
                string test = "";

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
                        WorkingDirectory = workingDIR,
                        //ErrorDialog = false,
                        RedirectStandardError = true,
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

            return null;


        }

        public static void checkClonedMod(ModGitController m)
        {
            //Directory.SetCurrentDirectory(m.path);
            if (Directory.Exists(m.path))
            {
                string modIdFile = Directory.GetFiles(m.path, "*.modinfo").FirstOrDefault();

                XmlDocument doc = new XmlDocument();


                doc.Load(modIdFile);
                XmlNode NodeMod = doc.GetElementsByTagName("Mod")[0];
                string attributesModIDValue = NodeMod.Attributes["id"].Value;


                if (m.modId != null && m.modId != attributesModIDValue)
                {
                    // st erreur
                    NodeMod.Attributes["id"].Value = m.modId;

                }
                doc.Save(modIdFile);
            }





        }

        public static void DeleteWithGit(ModGitController mod)
        {
            IServiceScope services = ServicesModloader.Service.CreateScope();
            DbSet<ModGit> DBmod = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().mod;

            ModGit mg = mod.GetModGit();

            if (!String.IsNullOrEmpty(mod.path) && Directory.Exists(mod.path))
            {

                if (Directory.Exists(mod.path))
                {
                    string test = "";
                    try
                    {
                        Directory.SetCurrentDirectory("/");
                        //Console.WriteLine(command);

                        string command = "/K rmdir /s /q " + "\"" + mod.path + "\"";
                        Console.WriteLine(command);
                        var process = new Process
                        {

                            StartInfo =
                            {
                        FileName = "cmd.exe",
                        Arguments = command,
                        //CreateNoWindow = true,
                        //RedirectStandardOutput = true,
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
                        string test2 = process.StandardError.ReadToEnd();



                        process.WaitForExit();
                        process.Dispose();
                        Console.WriteLine(test);
                        Console.WriteLine(test2);



                    }
                    catch (DirectoryNotFoundException e)
                    {
                        Console.WriteLine("The specified directory does not exist. {0}", e);
                    }
                }

                mod.path = null;
                mod.tag = null;
                DBmod.Update(mg);
                services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();
            }

            if (!String.IsNullOrEmpty(mod.path) && !Directory.Exists(mod.path))
            {





                mod.path = null;
                mod.tag = null;
                DBmod.Update(mg);
                services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();

            }

            services.ServiceProvider.GetService<DBConfigurationContext>().Dispose();

        }

        public static void InstallWithGit(ModGitController mod)
        {

            if (String.IsNullOrEmpty(mod.path) || !Directory.Exists(mod.path))
            {


                string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games", "Sid Meier's Civilization VI", "Mods");
                IServiceScope services = ServicesModloader.Service.CreateScope();
                DbSet<ModGit> DBmod = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().mod;

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                    mod.path = dir;
                    DBmod.Update(mod.GetModGit());
                    services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();
                    services.ServiceProvider.GetService<DBConfigurationContext>().Dispose();
                }
                if (Directory.Exists(dir))
                {

                    //Set the current directory.
                    //Directory.SetCurrentDirectory(dir);

                    //Set the current directory.
                    //Directory.SetCurrentDirectory(mod.path);
                    //ModGit m2 = DBmod.ToList().Find(a => a.path == mod.path);




                    string command = "clone " + mod.UriRepo();
                    Console.WriteLine(command);
                    StartGitCommand(command, dir);


                    //checkClonedMod(mod);



                    //mod.tag = tag;
                    //    m2.vue.tag =
                    //m2.tag = tag;
                    mod.path = Path.Combine(dir, mod.depot);

                    //m2.path = mod.path;
                    DBmod.Update(mod.GetModGit());
                    //m.vue.tagSelect = m.tag;
                    services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();
                    services.ServiceProvider.GetService<DBConfigurationContext>().Dispose();

                }

            }
        }
        public static void ResetRepotGit(ModGitController mod)
        {
            var tcs = new TaskCompletionSource<int>();

            if (!String.IsNullOrEmpty(mod.path) || !Directory.Exists(mod.path))
            {
                try
                {
                    Directory.SetCurrentDirectory(mod.path);

                    StartGitCommand("clean -fxd", mod.path);
                    StartGitCommand("reset --hard", mod.path);
                }
                catch (DirectoryNotFoundException e)
                {
                    Console.WriteLine("The specified directory does not exist. {0}", e);
                }
            }
        }

        public static Boolean isInstalled(ModGitController mod)
        {


            return Directory.Exists(Path.Combine(mod.path, ".git"));
                
         
        }

        public static void UpdateToTag(ModGitController m, string tag)
        {
            if (!String.IsNullOrEmpty(m.path) && Directory.Exists(m.path))
            {


                //string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games", "Sid Meier's Civilization VI", "Mods");

                if (Directory.Exists(m.path))
                {

                    IServiceScope services = ServicesModloader.Service.CreateScope();
                    DbSet<ModGit> DBmod = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().mod;

                    //Set the current directory.
                    Directory.SetCurrentDirectory(m.path);
                    ModGit m2 = DBmod.ToList().Find(a => a.path == m.path);
                    //IServiceScope services = Services.Service.CreateScope();
                    //DbSet<Config> config = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContext>().config;


                    ResetRepotGit(m);

                    string command = "-c advice.detachedHead=false checkout " + tag;
                    StartGitCommand(command, m.path);
                    m.tag = tag;
                    //    m2.vue.tag =
                    m2.tag = tag;
                    DBmod.Update(m2);
                    services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();
                    services.ServiceProvider.GetService<DBConfigurationContext>().Dispose();

                    m.vue.tagSelect = m.tag;
                    m.vue.tag = tag;


                }

            }
        }

























        public static void fetchAll(ModGitController m)
        {





        }


        public static void GetLastTag(ModGitController m)
        {

            GitHubApi.getLastTagNameReleaseFromRepo(m);

        }
        public static void updateBranchToTagAsync(ModGitController m, string tag)
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
        public static void InstallGit()
        {
            var process = new Process
            {

                StartInfo =
                    {
                        FileName = "powershell",
                        Arguments = "winget install git.git",
                        CreateNoWindow = true,
                        RedirectStandardOutput = false,
                        RedirectStandardError = false,
                    }
            };

            process.Start();
            process.WaitForExit();
            process.Dispose();


        }


        public static Boolean CheckInstallGit()
        {
            try
            {
                var process = new Process
                {
                    StartInfo =
                    {
                        FileName = "git",
                        CreateNoWindow = true,
                        RedirectStandardOutput = false,
                        RedirectStandardError = false,
                    }
                };
                process.Start();
                process.WaitForExit();
                process.Dispose();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Git non installé");
                //InstallGit();
                return false;
            }
        }
    }



}


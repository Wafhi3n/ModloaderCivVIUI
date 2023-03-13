using ModloaderClass;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModLoader.Utils
{
    abstract class GitController
    {
        public static string GetModTag(Mod m)
        {
            return "MODTAG";
        }

        public static void SwapToModDirectory(Mod m) {
            Directory.SetCurrentDirectory(@"c:\program files\");
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
}

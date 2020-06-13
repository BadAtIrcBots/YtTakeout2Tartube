using System;
using System.Collections.Generic;
using System.IO;
using CommandLine;
using Newtonsoft.Json;

namespace YtTakeout2Tartube
{
    public class Program
    {
        public class Options
        {
            [Option('i', "in", Required = true, HelpText = "YouTube subscriber takeout JSON")]
            public string In { get; set; }
            [Option('o', "out", Required = true, HelpText = "Tartube JSON output filename")]
            public string Out { get; set; }
            [Option("db-start-index", Default = 0, HelpText = "Tartube has DB IDs for each sub, this specifies where our IDs should increment from. From testing this doesn't seem necessary and it seems Tartube resolves potential ID issues itself")]
            public int DbStartIndex { get; set; }
            [Option("strip-colons", Default = false, HelpText = "Colons seem to give the importer issues for some reason, enable this if it's failing to import correctly (if the importer closes without saying how many items imported, it means it failed)")]
            public bool StripColons { get; set; }
        }
        
        static void Main(string[] args)
        {
            string inFile = string.Empty;
            string outFile = string.Empty;
            int dbStartIndex = 0;
            bool stripColons = false;
            Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o =>
            {
                inFile = o.In;
                outFile = o.Out;
                dbStartIndex = o.DbStartIndex;
                stripColons = o.StripColons;
            });

            if (!File.Exists(inFile))
            {
                Console.WriteLine($"'{inFile}' was specified for the 'in' file but it does not exist. Press any key to exit");
                Console.ReadKey();
                Environment.Exit(1);
            }

            if (File.Exists(outFile))
            {
                Console.WriteLine($"'{outFile}' already exists. Press any key if you want to continue and overwrite the file or close the window otherwise.");
                Console.ReadKey();
                File.Delete(outFile);
            }

            List<YtTakeoutModel.Subscription> subscriptions =
                JsonConvert.DeserializeObject<List<YtTakeoutModel.Subscription>>(File.ReadAllText(inFile));
            TartubeModel.DbExport dbExport = new TartubeModel.DbExport
            {
                ScriptName = "tartube",
                ScriptVersion = "2.1.0",
                SaveDate = DateTime.Now.ToString("dd MMM yyyy"),
                // Tartube incorrectly uses hh:mm:ss it seems, don't care about 100% compatibility tbh
                SaveTime = DateTime.Now.ToString("HH:mm:ss"),
                FileType = "db_export",
                DbDict = new Dictionary<string, TartubeModel.DbDictItem>()
            };

            int i = dbStartIndex;
            // For some reason my takeout sometimes had multiple instances of the same channel, this is a kludge to address that
            List<string> processedChannelIds = new List<string>();
            Console.WriteLine($"Found {subscriptions.Count} subscriptions in takeout JSON");
            foreach (var subscription in subscriptions)
            {
                if (processedChannelIds.Contains(subscription.Snippet.ResourceId.ChannelId))
                {
                    Console.WriteLine($"{subscription.Snippet.ResourceId.ChannelId} has already been processed, skipping.");
                    continue;
                }
                string name = subscription.Snippet.Title;

                if (stripColons)
                {
                    name = name.Replace(":", "");
                }
                dbExport.DbDict.Add(i.ToString(), new TartubeModel.DbDictItem
                {
                    Type = "channel",
                    DbId = i,
                    Name = name,
                    Nickname = name,
                    Source = $"https://www.youtube.com/channel/{subscription.Snippet.ResourceId.ChannelId}"
                });
                processedChannelIds.Add(subscription.Snippet.ResourceId.ChannelId);
                Console.WriteLine($"{subscription.Snippet.Title} ({subscription.Snippet.ChannelId}) done");
                i++;
            }
            
            Console.WriteLine("Finished sub list, writing new JSON to file");
            File.WriteAllText(outFile, JsonConvert.SerializeObject(dbExport, Formatting.Indented));
            Console.WriteLine("Finished writing to disk, press any key to exit.");
            Console.ReadKey();
        }
    }
}
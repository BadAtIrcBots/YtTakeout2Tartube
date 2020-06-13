using System.Collections.Generic;
using Newtonsoft.Json;

namespace YtTakeout2Tartube
{
    public class TartubeModel
    {
        public class DbExport
        {
            [JsonProperty("script_name")]
            public string ScriptName { get; set; }
            [JsonProperty("script_version")]
            public string ScriptVersion { get; set; }
            [JsonProperty("save_date")]
            public string SaveDate { get; set; }
            [JsonProperty("save_time")]
            public string SaveTime { get; set; }
            [JsonProperty("file_type")]
            public string FileType { get; set; }
            [JsonProperty("db_dict")]
            public Dictionary<string, DbDictItem> DbDict { get; set; }
        }

        public class DbDictItem
        {
            [JsonProperty("type")]
            public string Type { get; set; }
            [JsonProperty("dbid")]
            public int DbId { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("nickname")]
            public string Nickname { get; set; }
            [JsonProperty("source")]
            public string Source { get; set; }
            [JsonProperty("db_dict")]
            public EmptyDbDict EmptyDbDict { get; set; } = new EmptyDbDict();
        }
        
        public class EmptyDbDict {}
    }
}
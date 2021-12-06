﻿using System.Collections.Generic;
using Newtonsoft.Json;
using Wowthing.Lib.Extensions;

namespace Wowthing.Backend.Models.Data.Journal
{
    public class OutJournalInstance
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<OutJournalEncounter> Encounters { get; set; } = new();
                
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<int, int> BonusIds { get; set; }
        
        public string Slug => Name.Slugify();
    }
}
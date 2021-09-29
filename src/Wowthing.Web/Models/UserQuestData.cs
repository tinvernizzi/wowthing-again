﻿using System;
using System.Collections.Generic;

namespace Wowthing.Web.Models
{
    public class UserQuestData
    {
        public Dictionary<int, UserQuestDataCharacter> Characters { get; set; }
    }

    public class UserQuestDataCharacter
    {
        public DateTime ScannedAt { get; set; }
        public string DailyQuestsPacked { get; set; }
        public string QuestsPacked { get; set; } 
        public string WeeklyQuestsPacked { get; set; }
    }
}
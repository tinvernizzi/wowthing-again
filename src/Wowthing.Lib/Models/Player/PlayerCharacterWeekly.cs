﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Wowthing.Lib.Models
{
    public class PlayerCharacterWeekly
    {
        [Key, ForeignKey("Character")]
        [JsonIgnore]
        public int CharacterId { get; set; }
        [JsonIgnore]
        public PlayerCharacter Character { get; set; }

        public int KeystoneDungeon { get; set; }
        public int KeystoneLevel { get; set; }

        [Column(TypeName = "jsonb")]
        public PlayerCharacterWeeklyVault Vault { get; set; } = new PlayerCharacterWeeklyVault();
    }

    public class PlayerCharacterWeeklyVault
    {
        public DateTime ScannedAt { get; set; }

        public List<List<int>> MythicPlusRuns { get; set; }

        public List<PlayerCharacterWeeklyVaultProgress> MythicPlusProgress { get; set; }
        public List<PlayerCharacterWeeklyVaultProgress> RaidProgress { get; set; }
        public List<PlayerCharacterWeeklyVaultProgress> RankedPvpProgress { get; set; }
    }

    public class PlayerCharacterWeeklyVaultProgress
    {
        public int Level { get; set; }
        public int Progress { get; set; }
        public int Threshold { get; set; }
    }
}
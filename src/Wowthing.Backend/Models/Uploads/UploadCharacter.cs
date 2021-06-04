﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wowthing.Backend.Models.Uploads
{
    public class UploadCharacter
    {
        public bool Resting { get; set; }
        public int FlightSpeed { get; set; }
        public int GroundSpeed { get; set; }
        public int KeystoneInstance { get; set; }
        public int KeystoneLevel { get; set; }
        public int LastSeen { get; set; }
        public int PlayedTotal { get; set; }
        public int RestedXp { get; set; }
        public long Copper { get; set; }

        // currencies
        // lockouts
        public UploadCharacterMythicDungeon[] MythicDungeons { get; set; }
        public Dictionary<string, int> ScanTimes { get; set; }
        public UploadCharacterVault[][] Vault { get; set; }
    }
}
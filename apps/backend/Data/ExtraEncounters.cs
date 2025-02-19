﻿using Wowthing.Backend.Models;

namespace Wowthing.Backend.Data;

public static partial class Hardcoded
{
    public static readonly Dictionary<int, ExtraEncounter[]> ExtraEncounters = new()
    {
        #region Miscellaneous
        // ??? > Darkmaul Citadel
        {
            1,
            new[]
            {
                new ExtraEncounter("Tunk"),
                new ExtraEncounter("Gor'groth"),
            }
        },
        #endregion

        #region Classic
        // Classic > Dire Maul
        {
            230,
            new[]
            {
                new ExtraEncounter("Gordok Tribute")
                {
                    AfterEncounter = 417, // King Gordok
                },
            }
        },
        // Classic > Gnomeregan
        {
            231,
            new[]
            {
                new ExtraEncounter("Endgineer Omegaplugg")
                {
                    AfterEncounter = 422, // Mekgineer Thermaplugg
                },
            }
        },
        // Classic > Stratholme
        {
            236,
            new[]
            {
                new ExtraEncounter("Postmaster Malown")
                {
                    AfterEncounter = 443, // Hearthstinger Forresten
                },
                new ExtraEncounter("Skul")
                {
                    AfterEncounter = 443, // Hearthstinger Forresten
                },
                new ExtraEncounter("Stonespine")
                {
                    AfterEncounter = 450, // The Unforgiven
                },
             }
        },
        #endregion

        #region The Burning Crusade
        // TBC > Old Hillsbrad Foothills
        {
            251,
            new[]
            {
                new ExtraEncounter("Don Carlos"),
            }
        },
        // TBC > Karazhan
        {
            745,
            new[]
            {
                new ExtraEncounter("Nightbane")
                {
                    AfterEncounter = 1561, // Netherspite
                },
            }
        },
        #endregion

        #region Wrath of the Lich King
        // WotLK > The Culling of Stratholme
        {
            279,
            new[]
            {
                new ExtraEncounter("Infinite Corruptor")
                {
                    AfterEncounter = 613, // Chrono-Lord Epoch
                }
            }
        },
        // WotLK > Icecrown Citadel
        {
            758,
            new[]
            {
                new ExtraEncounter("Sanctified T10"),
            }
        },
        #endregion

        #region Mists of Pandaria
        // MoP > Scholomance
        {
            246,
            new[]
            {
                new ExtraEncounter("Doctor Theolen Krastinov")
                {
                    AfterEncounter = 665, // Rattlegore
                }
            }
        },
        #endregion

        #region Legion
        // Legion > Return to Karazhan
        {
            860,
            new[]
            {
                new ExtraEncounter("Nightbane")
                {
                    AfterEncounter = 1836, // The Curator
                },
            }
        },
        // Legion > Seat of the Triumvirate
        {
            945,
            new[]
            {
                new ExtraEncounter("Vixx the Collector")
                {
                    AfterEncounter = 1979, // Zuraal the Ascended
                },
            }
        },
        #endregion Legion
    };
}

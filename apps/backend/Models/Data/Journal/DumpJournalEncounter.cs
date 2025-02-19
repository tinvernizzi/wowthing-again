﻿using CsvHelper.Configuration.Attributes;

// ReSharper disable InconsistentNaming
namespace Wowthing.Backend.Models.Data.Journal;

public class DumpJournalEncounter
{
    public int ID { get; set; }

    public int DifficultyMask { get; set; }
    public int Flags { get; set; }
    public int JournalInstanceID { get; set; }
    public int OrderIndex { get; set; }

    [Name("Name_lang")]
    public string Name { get; set; }
}
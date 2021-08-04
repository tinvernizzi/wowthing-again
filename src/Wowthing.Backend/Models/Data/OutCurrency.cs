﻿namespace Wowthing.Backend.Models.Data
{
    public class OutCurrency
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int MaxPerWeek { get; set; }
        public int MaxTotal { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public OutCurrency(DumpCurrencyTypes currencyType)
        {
            Id = currencyType.ID;
            CategoryId = currencyType.CategoryID;
            MaxPerWeek = currencyType.MaxEarnablePerWeek;
            MaxTotal = currencyType.MaxQty;
            Description = currencyType.Description;
            Name = currencyType.Name;
        }
    }
}
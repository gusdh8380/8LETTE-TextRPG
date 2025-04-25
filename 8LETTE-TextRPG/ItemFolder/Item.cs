using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace _8LETTE_TextRPG.ItemFolder
{
    [JsonConverter(typeof(ItemConverter))]
    public abstract class Item
    {
        public string Id { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        public float Price { get; protected set; }
        public ItemType ItemType { get; protected set; }
        public Dictionary<ItemEffect, float> Effects { get; protected set; }

        public Item()
        {
            Id = string.Empty;
            Name = string.Empty;
            Description = string.Empty;
            Effects = new Dictionary<ItemEffect, float>();
        }

        public abstract string GetEffectName();
    }
}

using System.Text;

namespace _8LETTE_TextRPG.ItemFolder
{
    public abstract class Item
    {
        public string Id { get; private set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        public float Price { get; protected set; }
        public ItemType ItemType { get; protected set; }
        public Dictionary<ItemEffect, float> EffectDict { get; protected set; }

        public Item()
        {
            Id = Guid.NewGuid().ToString();
            Name = string.Empty;
            Description = string.Empty;
            EffectDict = new Dictionary<ItemEffect, float>();
        }

        public abstract string GetEffectName();
    }
}

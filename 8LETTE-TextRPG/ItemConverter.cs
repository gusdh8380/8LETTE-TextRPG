using _8LETTE_TextRPG.ItemFolder;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG
{
    class ItemConverter : JsonConverter<Item>
    {
        public override Item? ReadJson(JsonReader reader, Type objectType, Item? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            JObject jo = JObject.Load(reader);

            string id = jo["Id"]?.Value<string>() ?? throw new NullReferenceException();
            string name = jo["Name"]?.Value<string>() ?? throw new NullReferenceException();
            string desc = jo["Description"]?.Value<string>() ?? throw new NullReferenceException();
            float price = jo["Price"]?.Value<float>() ?? throw new NullReferenceException();
            ItemType itemType = jo["ItemType"]?.ToObject<ItemType>() ?? throw new NullReferenceException();
            Dictionary<ItemEffect, float> effects = jo["Effects"]?.ToObject<Dictionary<ItemEffect, float>>() ?? throw new NullReferenceException();

            Item? item;
            switch (itemType)
            {
                case ItemType.Equipment:
                    EquipmentType equipmentType = jo["EquipmentType"]?.ToObject<EquipmentType>() ?? throw new NullReferenceException();
                    item = new EquipableItem(id, name, desc, price, equipmentType, effects);
                    if (item is EquipableItem equipableItem)
                    {
                        equipableItem.IsEquipped = jo["IsEquipped"]?.Value<bool>() ?? throw new NullReferenceException();
                    }
                    break;
                case ItemType.Usable:
                    UseType useType = jo["UseType"]?.ToObject<UseType>() ?? throw new NullReferenceException();
                    item = new UsableItem(id, name, desc, price, useType, effects);
                    break;
                default:
                    throw new JsonSerializationException($"Unknown ItemType: {itemType}");
            }

            return item;
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, Item? value, JsonSerializer serializer)
        {
            throw new NotImplementedException(); // WriteJson은 호출되면 안됨
        }
    }
}

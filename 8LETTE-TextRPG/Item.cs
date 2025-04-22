using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG
{
    public class Item
    {
        public string Id { get; }
        public string Name { get; }
        public float Attack { get; }
        public float Defense { get; }
        public string Description { get; }
        public float Price { get; }
        public int Type { get; }
        public bool IsEquipped { get; set; }


        public Item(string name, float atk, float def, string desc, float price, int type)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Attack = atk;
            Defense = def;
            Description = desc;
            Price = price;
            Type = type;
        }

        //public string StatString()
        //{
        //    if (Attack > 0) return $"공격력 +{Attack}";
        //    if (Defense > 0) return $"방어력 +{Defense}";
        //    return string.Empty;
        //}
    }
}
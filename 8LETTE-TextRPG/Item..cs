using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG
{
    public class Item
    {
        public string Id {  get; set; }
        public string Name { get; }
        public float Attack { get; }
        public float Defense { get; }
        public string Description { get; }
        public float Price { get; }
        public int Type { get; }
        
        //장착 중인 것을 확인하기 위해 필요할 것 같음
        public bool IsEquipped { get; set; }

        public Item(string name, float atk, float def, string desc, float price, int type)
        {
            Name = name; Attack = atk; Defense = def; Description = desc; Price = price; Type = type;
            Id = Guid.NewGuid().ToString();
        }

        //public string StatString()
        //{
        //    if (Attack > 0) return $"공격력 +{Attack}";
        //    if (Defense > 0) return $"방어력 +{Defense}";
        //    return string.Empty;
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG
{
    public class Item : IEquipable, IUsable
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public float Price { get; private set; }
        public ItemType ItemType { get; private set; }


        // IEquipable 구현
        public float EquipAtkInc { get; set; }
        public float EquipDefInc { get; set; }
        public float EquipHpInc { get; set; }
        public EquipmentType EquipmentType { get; set; }
        public bool IsEquipped { get; set; }
        // IEquipable 구현


        // IUsable 구현
        public float UsedAtkInc { get; set; }
        public float UsedDefInc { get; set; }
        public float UsedHpInc { get; set; }
        // IUsable 구현

        /// <summary>
        /// 장착 가능 아이템
        /// </summary>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        /// <param name="price"></param>
        /// <param name="equipmentType"></param>
        /// <param name="atk"></param>
        /// <param name="def"></param>
        /// <param name="hp"></param>
        public Item(string name, string desc, float price, EquipmentType equipmentType, float atk = 0f, float def = 0f, float hp = 0f)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Description = desc;
            Price = price;
            ItemType = ItemType.Equipment;

            EquipAtkInc = atk;
            EquipDefInc = def;
            EquipHpInc = hp;
            EquipmentType = equipmentType;
        }

        /// <summary>
        /// 소모성 아이템
        /// </summary>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        /// <param name="price"></param>
        /// <param name="atk"></param>
        /// <param name="def"></param>
        /// <param name="hp"></param>
        public Item(string name, string desc, float price, float atk = 0f, float def = 0f, float hp = 0f)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Description = desc;
            Price = price;
            ItemType = ItemType.Usable;

            UsedAtkInc = atk;
            UsedDefInc = def;
            UsedHpInc = hp;
        }

        public string GetEffectName()
        {
            StringBuilder sb = new StringBuilder();
            switch (ItemType)
            {
                case ItemType.Equipment:
                    if (EquipAtkInc != 0f)
                    {
                        sb.Append("공격력 ");
                        sb.Append(EquipAtkInc > 0f ? "+" : "");
                        sb.Append(EquipAtkInc);
                        sb.Append(" ");
                    }

                    if (EquipDefInc != 0f)
                    {
                        sb.Append("방어력 ");
                        sb.Append(EquipDefInc > 0f ? "+" : "");
                        sb.Append(EquipDefInc);
                        sb.Append(" ");
                    }

                    if (EquipHpInc != 0f)
                    {
                        sb.Append("체력 ");
                        sb.Append(EquipHpInc > 0f ? "+" : "");
                        sb.Append(EquipHpInc);
                        sb.Append(" ");
                    }
                    break;
                case ItemType.Usable:
                    if (UsedAtkInc != 0f)
                    {
                        sb.Append("공격력 ");
                        sb.Append(UsedAtkInc > 0f ? "+" : "");
                        sb.Append(UsedAtkInc);
                        sb.Append(" ");
                    }

                    if (UsedDefInc != 0f)
                    {
                        sb.Append("방어력 ");
                        sb.Append(UsedDefInc > 0f ? "+" : "");
                        sb.Append(UsedDefInc);
                        sb.Append(" ");
                    }

                    if (UsedHpInc != 0f)
                    {
                        sb.Append("회복량 ");
                        sb.Append(UsedHpInc > 0f ? "+" : "");
                        sb.Append(UsedHpInc);
                        sb.Append(" ");
                    }
                    break;
            }

            return sb.ToString();
        }

        public void Equip()
        {
            if (!string.IsNullOrEmpty(Player.Instance.EquippedItems[EquipmentType]))
            {
                Unequip();
            }

            IsEquipped = true;
            Player.Instance.EquippedItems[EquipmentType] = Id;

            Player.Instance.BaseAttack += EquipAtkInc;
            Player.Instance.BaseDefense += EquipDefInc;
            Player.Instance.MaxHealth += EquipHpInc;
            Player.Instance.Health += EquipHpInc;
        }

        public void Unequip()
        {
            IsEquipped = false;
            Player.Instance.EquippedItems[EquipmentType] = string.Empty;

            Player.Instance.BaseAttack -= EquipAtkInc;
            Player.Instance.BaseDefense -= EquipDefInc;
            Player.Instance.MaxHealth -= EquipHpInc;
            Player.Instance.Health -= EquipHpInc;
        }

        public void Use()
        {
            Player.Instance.BaseAttack += UsedAtkInc;
            Player.Instance.BaseDefense += UsedDefInc;
            Player.Instance.Health += UsedHpInc;

            Player.Instance.Inventory.RemoveItem(this);
        }

        //public string StatString()
        //{
        //    if (Attack > 0) return $"공격력 +{Attack}";
        //    if (Defense > 0) return $"방어력 +{Defense}";
        //    return string.Empty;
        //}
    }
}
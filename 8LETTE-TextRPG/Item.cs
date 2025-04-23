namespace _8LETTE_TextRPG
{
    public class Item : IEquipable, IUsable
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public float Price { get; private set; }
        public ItemType ItemType { get; private set; }
        public Dictionary<ItemEffect, float> EffectDict { get; set; }

        // IEquipable 구현
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
        /// <param name="effectDict"></param>
        public Item(string name, string desc, float price, EquipmentType equipmentType, Dictionary<ItemEffect, float> effectDict)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Description = desc;
            
            Price = price;
            ItemType = ItemType.Equipment;

            EffectDict = effectDict;
            EquipmentType = equipmentType;
        }

        /// <summary>
        /// 소모성 아이템
        /// </summary>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        /// <param name="price"></param>
        /// <param name="effectDict"></param>
        public Item(string name, string desc, float price, Dictionary<ItemEffect, float> effectDict)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Description = desc;
            Price = price;
            ItemType = ItemType.Usable;

            EffectDict = effectDict;
        }

        public string GetEffectName()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<ItemEffect, float> effectPair in EffectDict)
            {
                if (effectPair.Value != 0f)
                {
                    switch (effectPair.Key)
                    {
                        case ItemEffect.Atk:
                            sb.Append("공격력 ");
                            sb.Append(effectPair.Value > 0f ? "+" : "");
                            sb.Append(effectPair.Value);
                            sb.Append(" ");
                            break;
                        case ItemEffect.Def:
                            sb.Append("방어력 ");
                            sb.Append(effectPair.Value > 0f ? "+" : "");
                            sb.Append(effectPair.Value);
                            sb.Append(" ");
                            break;
                        case ItemEffect.Hp:
                            sb.Append("체력 ");
                            sb.Append(effectPair.Value > 0f ? "+" : "");
                            sb.Append(effectPair.Value);
                            sb.Append(" ");
                            break;
                        case ItemEffect.Critical:
                            sb.Append("치명 ");
                            sb.Append(effectPair.Value > 0f ? "+" : "");
                            sb.Append(effectPair.Value);
                            sb.Append(" ");
                            break;
                        case ItemEffect.Evasion:
                            sb.Append("회피 ");
                            sb.Append(effectPair.Value > 0f ? "+" : "");
                            sb.Append(effectPair.Value);
                            sb.Append(" ");
                            break;
                        default:
                            sb.Append(" 알 수 없음 ");
                            sb.Append(effectPair.Value > 0f ? "+" : "");
                            sb.Append(effectPair.Value);
                            sb.Append(" ");
                            break;
                    }
                }
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

            foreach (KeyValuePair<ItemEffect, float> effectPair in EffectDict)
            {
                if (effectPair.Value != 0f)
                {
                    switch (effectPair.Key)
                    {
                        case ItemEffect.Atk:
                            Player.Instance.BaseAttack += effectPair.Value;
                            break;
                        case ItemEffect.Def:
                            Player.Instance.BaseDefense += effectPair.Value;
                            break;
                        case ItemEffect.Hp:
                            Player.Instance.MaxHealth += effectPair.Value;
                            Player.Instance.Health += effectPair.Value;
                            break;
                        case ItemEffect.Critical:
                            Player.Instance.CriticalChance += effectPair.Value;
                            break;
                        case ItemEffect.Evasion:
                            Player.Instance.EvasionRate += effectPair.Value;
                            break;
                        default:
                            break;
                    }
                }
            }

            QuestManager.Instance?.SendProgress(QuestType.EquipItem, "", 1);
        }

        public void Unequip()
        {
            IsEquipped = false;
            Player.Instance.EquippedItems[EquipmentType] = string.Empty;

            foreach (KeyValuePair<ItemEffect, float> effectPair in EffectDict)
            {
                switch (effectPair.Key)
                {
                    case ItemEffect.Atk:
                        Player.Instance.BaseAttack -= effectPair.Value;
                        break;
                    case ItemEffect.Def:
                        Player.Instance.BaseDefense -= effectPair.Value;
                        break;
                    case ItemEffect.Hp:
                        Player.Instance.MaxHealth -= effectPair.Value;
                        Player.Instance.Health -= effectPair.Value;
                        break;
                    case ItemEffect.Critical:
                        Player.Instance.CriticalChance -= effectPair.Value;
                        break;
                    case ItemEffect.Evasion:
                        Player.Instance.EvasionRate -= effectPair.Value;
                        break;
                    default:
                        break;
                }
            }
        }

        public void Use()
        {
            foreach (KeyValuePair<ItemEffect, float> effectPair in EffectDict)
            {
                switch (effectPair.Key)
                {
                    case ItemEffect.Atk:
                        Player.Instance.BaseAttack += effectPair.Value;
                        break;
                    case ItemEffect.Def:
                        Player.Instance.BaseDefense += effectPair.Value;
                        break;
                    case ItemEffect.Hp:
                        Player.Instance.Health += effectPair.Value;
                        break;
                    case ItemEffect.Critical:
                        Player.Instance.CriticalChance += effectPair.Value;
                        break;
                    case ItemEffect.Evasion:
                        Player.Instance.EvasionRate += effectPair.Value;
                        break;
                    default:
                        break;
                }
            }

            Player.Instance.Inventory.RemoveItem(this);
        }

        /// <summary>
        /// 회복 아이템 (1회성)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        /// <param name="hp"></param>
        /// <param name="price"></param>
        public Item(string name, string desc, float hp, float price)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Description = desc;
            Hp = hp;
            Price = price;
        }

        //public string StatString()
        //{
        //    if (Attack > 0) return $"공격력 +{Attack}";
        //    if (Defense > 0) return $"방어력 +{Defense}";
        //    return string.Empty;
        //}

        public void Equip()
        {
            if (IsEquipped)
            {
                IsEquipped = false;
            }
            else
            {
                // 같은 타입 해제
                foreach (var item in Player.Instance.Inventory.GetAllItems())
                {
                    if (item.Type == item.Type && item.IsEquipped)
                    {
                        item.IsEquipped = false;
                    }
                }
                IsEquipped = true;
            }
        }

        public void Unequip()
        {
            if (IsEquipped)
            {
                IsEquipped = false;
            }
        }

        public void Use()
        {
            Player.Instance.Health += Hp;
            Player.Instance.Inventory.DeleteItem(this);
        }
    }
}

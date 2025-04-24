using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG.ItemFolder
{
    class EquipableItem : Item, IEquipable
    {
        public EquipmentType EquipmentType { get; set; }
        public bool IsEquipped { get; set; }

        public EquipableItem(string name, string desc, float price, EquipmentType equipmentType, Dictionary<ItemEffect, float> effectDict)
        {
            Name = name;
            Description = desc;

            Price = price;
            ItemType = ItemType.Equipment;

            EffectDict = effectDict;
            EquipmentType = equipmentType;
        }

        public override string GetEffectName()
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
                            sb.Append("최대체력 ");
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
            IsEquipped = true;
            Player.Instance.EquippedItems[EquipmentType] = Id;

            foreach (KeyValuePair<ItemEffect, float> effectPair in EffectDict)
            {
                if (effectPair.Value != 0f)
                {
                    switch (effectPair.Key)
                    {
                        case ItemEffect.Atk:
                            if (Player.Instance.BaseAttack + effectPair.Value > 0f)
                            {
                                Player.Instance.BaseAttack += effectPair.Value;
                            }
                            else
                            {
                                Player.Instance.BaseAttack = 0f;
                            }
                            break;
                        case ItemEffect.Def:
                            if (Player.Instance.BaseDefense + effectPair.Value > 0f)
                            {
                                Player.Instance.BaseDefense += effectPair.Value;
                            }
                            else
                            {
                                Player.Instance.BaseDefense = 0f;
                            }
                            break;
                        case ItemEffect.Hp:
                            if (Player.Instance.MaxHealth + effectPair.Value > 0f)
                            {
                                Player.Instance.MaxHealth += effectPair.Value;
                            }
                            else
                            {
                                Player.Instance.MaxHealth = 1f;
                            }
                            break;
                        case ItemEffect.Critical:
                            if (Player.Instance.CriticalChance + effectPair.Value > 0f)
                            {
                                Player.Instance.CriticalChance += effectPair.Value;
                            }
                            else
                            {
                                Player.Instance.CriticalChance = 0f;
                            }
                            break;
                        case ItemEffect.Evasion:
                            if (Player.Instance.EvasionRate + effectPair.Value > 0f)
                            {
                                Player.Instance.EvasionRate += effectPair.Value;
                            }
                            else
                            {
                                Player.Instance.EvasionRate = 0f;
                            }
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
    }
}

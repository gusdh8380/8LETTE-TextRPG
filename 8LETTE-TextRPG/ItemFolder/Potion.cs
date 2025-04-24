using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG.ItemFolder
{
    class Potion : Item, IUsable
    {
        public Potion(string name, string desc, float price, Dictionary<ItemEffect, float> effectDict)
        {
            Name = name;
            Description = desc;

            Price = price;
            ItemType = ItemType.Usable;

            EffectDict = effectDict;
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
    }
}

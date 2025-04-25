using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG.ItemFolder
{
    class UsableItem : Item, IUsable
    {
        public UseType UseType { get; set; }

        public UsableItem(string id, string name, string desc, float price, UseType useType, Dictionary<ItemEffect, float> effects)
        {
            Id = id;
            Name = name;
            Description = desc;

            Price = price;
            ItemType = ItemType.Usable;

            UseType = useType;
            Effects = effects;
        }

        public override string GetEffectName()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<ItemEffect, float> effectPair in Effects)
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
                            sb.Append("HP ");
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
                        case ItemEffect.MP:
                            sb.Append("MP ");
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
            foreach (KeyValuePair<ItemEffect, float> effectPair in Effects)
            {
                switch (effectPair.Key)
                {
                    case ItemEffect.Atk:
                        Player.Instance.Stats.BaseAttack += effectPair.Value;
                        break;
                    case ItemEffect.Def:
                        Player.Instance.Stats.BaseDefense += effectPair.Value;
                        break;
                    case ItemEffect.Hp:
                        Player.Instance.Health += effectPair.Value;
                        break;
                    case ItemEffect.Critical:
                        Player.Instance.Stats.BaseCriticalChance += effectPair.Value;
                        break;
                    case ItemEffect.Evasion:
                        Player.Instance.Stats.BaseEvasionRate += effectPair.Value;
                        break;
                    case ItemEffect.MP:
                        Player.Instance.Mana += effectPair.Value;
                        break;
                    default:
                        break;
                }
            }

            QuestManager.Instance?.SendProgress(QuestType.UseItem, "포션 마시기", 1);
            Player.Instance.Inventory.RemoveItem(this);

            Player.Instance.OnContextChanged();
        }
    }
}

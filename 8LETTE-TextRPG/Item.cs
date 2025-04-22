namespace _8LETTE_TextRPG
{
    public class Item : IEquipable, IUsable
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public float Attack { get; private set; }
        public float Defense { get; private set; }
        public float Hp { get; private set; }
        public float Price { get; private set; }
        public int Type { get; private set; }
        public bool IsEquipped { get; private set; }


        /// <summary>
        /// 장착 가능한 아이템
        /// </summary>
        /// <param name="name"></param>
        /// <param name="atk"></param>
        /// <param name="def"></param>
        /// <param name="desc"></param>
        /// <param name="price"></param>
        /// <param name="type"></param>
        public Item(string name, string desc, float atk, float def, float price, int type)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Description = desc;
            Attack = atk;
            Defense = def;
            Price = price;
            Type = type;
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
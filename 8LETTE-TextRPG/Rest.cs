using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG
{
    class Rest
    {
        public float Price { get; private set; }

        public Rest(float price)
        {
            Price = price;
        }

        public bool TryRest()
        {
            if (Player.Instance.Gold < Price)
            {
                return false;
            }

            Player.Instance.Gold -= Price;
            Player.Instance.IsDead = false;
            Player.Instance.Health = Player.Instance.MaxHealth;
            Player.Instance.Mana = Player.Instance.MaxMana;

            Player.Instance.OnContextChanged();

            return true;
        }
    }
}

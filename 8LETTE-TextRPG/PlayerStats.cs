using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG
{
    [Serializable]
    public class PlayerStats
    {
        public float BaseHealth;
        public float CurHealth;
        public float BaseMP;
        public float CurMP;
        public float BaseAttack;
        public float LevelBonusAtk;
        public float BaseDefense;
        public float LevelBonusDfs;
        public float BaseCriticalChance;
        public float BaseEvasionRate;
    }
}

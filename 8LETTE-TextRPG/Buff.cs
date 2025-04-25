using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG
{
    public enum DurationType { OneTurn, UntilBattleEnd }
    public class Buff
    {
        public string Name { get; }
        public float AttackMultiplier { get; }
        public float DefenseMultiplier { get; }
        public float CriticalMultiplier { get; }

        public float EvasionMultiplier { get; }
        public int TurnsRemaining { get; set; }
        public DurationType Duration { get; set; }

        public Buff(string name, float atkMultiplier, float defMultiplier,float criticalMultiplier, float evasionMultiplier, int turns, DurationType duration)
        {
            Name = name; //버프 이름
            AttackMultiplier = atkMultiplier; //공격력 증가
            DefenseMultiplier = defMultiplier; //방어력 증가
            CriticalMultiplier = criticalMultiplier;
            EvasionMultiplier = evasionMultiplier;  
            TurnsRemaining = turns; // 버프 유지 턴(턴이 있는 버프)
            Duration = duration; // 버프 유지 태그(전투 내내 적용 버프)
        }


        public bool IsExpired => Duration == DurationType.OneTurn && TurnsRemaining <= 0;
    }
}

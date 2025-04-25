using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG
{   

    //직업 추상 클래스
    public abstract class JobBase
    {
        public abstract string Name { get; }

        public abstract float BaseAttack { get; }
        public abstract float BaseDefense { get; }
        public abstract float BaseHealth { get; }

        public abstract int CriticalChance { get; }
        public abstract int EvasionRate { get; }

        public abstract List<Skill> Skills { get; }

        // 전직 단계(미들 -> 시니어 -> 디렉터)
        public abstract int PromotionStage { get; }

        public abstract void IncreaseStats(Player player);
        

        ////직업 클래스 : 임시 작성 
        //protected JobBase(string name, float baseAttack, float baseDefense, float baseHealth, int criticalChance, int evationRate)
        //{
        //    Name = name;
        //    BaseAttack = baseAttack;
        //    BaseDefense = baseDefense;
        //    BaseHealth = baseHealth;
        //    CriticalChance = criticalChance;
        //    EvationRate = evationRate;
        //}

        //public static List<Job> GetJobs()
        //{
        //    return new List<Job>
        //    {
        //        new Job("주니어", 12f,5f,100,15,10),
        //        new Job("버그워리어 ", 12f, 5f, 100f,15,10),
        //        new Job("메모리메이지", 8f, 10f, 120f, 15, 10),
        //        new Job("스레드어쌔신", 10f, 7f, 110f, 15, 10),
        //        new Job("익셉션헌터 ", 15f, 3f, 90f, 15, 10),

        //    };
        //}
    }
}

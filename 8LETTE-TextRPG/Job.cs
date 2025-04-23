using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG
{
    public class Job
    {
        public string Name { get; }

        public float BaseAttack { get; }
        public float BaseDefense { get; }
        public float BaseHealth { get; }

        public int CriticalChance { get; }
        public int EvationRate { get; }

        //직업 클래스 : 임시 작성 
        public Job(string name, float baseAttack, float baseDefense, float baseHealth, int criticalChance, int evationRate)
        {
            Name = name;
            BaseAttack = baseAttack;
            BaseDefense = baseDefense;
            BaseHealth = baseHealth;
            CriticalChance = criticalChance;
            EvationRate = evationRate;
        }

        public static List<Job> GetJobs()
        {
            return new List<Job>
            {
                new Job("빨간 ", 12f, 5f, 100f,15,10),
                new Job("파란 ", 8f, 10f, 120f, 15, 10),
                new Job("초록 ", 10f, 7f, 110f, 15, 10),
                new Job("노란 ", 15f, 3f, 90f, 15, 10),
                new Job("검정 ", 9f, 6f, 130f, 15, 10)
            };
        }
    }
}

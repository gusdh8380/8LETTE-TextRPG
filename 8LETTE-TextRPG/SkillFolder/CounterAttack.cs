using _8LETTE_TextRPG.MonsterFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG.SkillFolder
{
    public class CounterAttack : Skill
    {
        public override string Name => "카운터어택!";
        public override string Description => " 몬스터의 공격을 회피 시, 공격력의 50% 피해를 줍니다 ";

        public override SkillType Type => SkillType.Passive;
        public override SkillEffectType Effect => SkillEffectType.Damage;
        public override float ManaCost => 15f;

        public override bool Execute(Player player, Monster monster)
        {
            float rawDamage = player.Attack * 0.5f * PromotionMultiplier;
            float finalDamage = player.ApplyDefenseReduction(rawDamage, monster.Defense);
            Console.WriteLine($"{player.Name}이(가) '{Name}' 스킬로 {monster.Name}에게 {finalDamage}의 카운터 어택를 입혔습니다!");

            monster.OnDamaged(finalDamage);



            if (monster.IsDead)
            {
                //Console.WriteLine($"\n{monster.Name}을(를) 처치했습니다!");
                player.GainExp(monster.Level);

            }

            return true;
        }
    }
}

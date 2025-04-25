using _8LETTE_TextRPG.MonsterFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG.SkillFolder
{
    public class DebugStrike : Skill
    {
        public override string Name => "디버그스트라이크";
        public override string Description => "몬스터 전체에게 공격력의 30% 피해를 입힙니다.";
        public override SkillType Type => SkillType.Active;
        public override SkillEffectType Effect => SkillEffectType.Damage;

        public override float ManaCost => 15f;

        //몬스터 전체를 공격하는 스킬이기에, 몬스터 파라마터는 무시
        public override bool Execute(Player player, Monster _)
        {
            if (!CanUse(player))
            {
                Console.WriteLine($"MP가 부족하여 '{Name}' 스킬을 사용할 수 없습니다. (필요 MP: {ManaCost})");
                return false;
            }

            player.Mana -= ManaCost;

            Monster[] monsters = MonsterSpawner.Instance.GetAllMonsters();
            float Atk = Player.Instance.Attack;
            float rawDamage = (float)Math.Ceiling(Atk * 0.3f) * PromotionMultiplier;

            foreach (Monster monster in monsters)
            {
                if (monster.IsDead)
                {
                    continue;
                }

                float fianlDamage = Player.Instance.ApplyDefenseReduction(rawDamage, monster.Defense);
                Console.WriteLine($"Lv.{monster.Level} {monster.Name}에게 {fianlDamage}의 데미지를 입혔습니다.");
                monster.OnDamaged(fianlDamage);


                if (monster.IsDead)
                {
                    Player.Instance.GainExp(monster.Level);
                }
            }
            return true;
        }
    }
}

using _8LETTE_TextRPG.MonsterFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG.SkillFolder
{
    public class ShieldStrike : Skill
    {
        public override string Name => "방패치기";
        public override string Description => " 몬스터 전체에게 방어력의 100% 피해를 입힙니다.";

        public override SkillType Type => SkillType.Active;
        public override SkillEffectType Effect => SkillEffectType.Damage;

        public override float ManaCost => 15f;


        //몬스터 전체를 공격하는 스킬이기에, 몬스터 파라마터는 무시
        public override bool Execute(Player player, Monster _)
        {
            if (!CanUse(player))
            {
                Console.WriteLine($"MP가 부족하여 '{Name}' 스킬을 사용할 수 없습니다. (현재 MP: {ManaCost})");
                return false;
            }

            player.Mana -= ManaCost;

            Console.WriteLine($"{player.Name}이(가) '{Name}' 스킬을 사용했습니다!");
            Monster[] monsters = DungeonManager.Instance.GetAllMonsters();

            //데미지 = 플레이어 기본 공격력*2 *{(디렉터 강화 계수) = 기본값 1, 디렉터는 1.5}
            float damage = Player.Instance.Defense * PromotionMultiplier;
            foreach (Monster monster in monsters)
            {
                if (monster.IsDead)
                {
                    continue;
                }

                float fianlDamage = Player.Instance.ApplyDefenseReduction(damage, monster.Defense);
                Console.WriteLine($"Lv.{monster.Level} {monster.Name}에게 {fianlDamage}의 데미지를 입혔습니다.");
                monster.OnDamaged(fianlDamage);

                //if (monster.IsDead)
                //{
                //    Player.Instance.GainExp(monster.Level);
                //}
            }

            return true;
        }
    }
}

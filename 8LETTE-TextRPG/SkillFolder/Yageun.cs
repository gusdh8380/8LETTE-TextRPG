using _8LETTE_TextRPG.MonsterFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG.SkillFolder
{
    public class Yaguen : Skill
    {
        public override string Name => "야근";
        public override string Description => "랜덤 적 하나에게 공격력 2배의 피해를 입힙니다.";

        public override SkillType Type => SkillType.Active;
        public override SkillEffectType Effect => SkillEffectType.Damage;
        public override float ManaCost => base.ManaCost;


        //스킬 실행 로직
        public override bool Execute(Player player, Monster _)
        {
            if (!CanUse(player))
            {
                Console.WriteLine($"MP가 부족하여 '{Name}' 스킬을 사용할 수 없습니다. (필요 MP: {ManaCost})");
                return false;
            }

            player.Mana -= ManaCost;

            Monster[] monsters = DungeonManager.Instance.GetAllMonsters().Where(m => !m.IsDead).ToArray();

            // 랜덤으로 한 마리 선택
            var rand = new Random();
            var chosen = monsters[rand.Next(monsters.Length)];

            //데미지 = 플레이어 기본 공격력*2 *{(디렉터 강화 계수) = 기본값 1, 디렉터는 1.5}
            float damage = Player.Instance.Attack * 2f * PromotionMultiplier;

            // 아래 코드에서 몬스터 방어력에 따른 데미지 계산 로직 추가
            float finalDamege = player.ApplyDefenseReduction(damage, chosen.Defense);

            Console.WriteLine($"{player.Name}이(가) '야근' 스킬을 사용했습니다!");
            Console.WriteLine($"{chosen.Name}에게 {finalDamege}의 피해를 입혔습니다!");

            chosen.OnDamaged(finalDamege);

            if (chosen.IsDead)
            {
                //Console.WriteLine($"\n{chosen.Name}을(를) 처치했습니다!");
                Player.Instance.GainExp(chosen.Level);
            }

            return true;
        }
    }
}

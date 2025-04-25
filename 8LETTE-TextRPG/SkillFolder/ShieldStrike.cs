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
        public override string Description => " 방어력의 100% 피해를 입힙니다.";

        public override SkillType Type => SkillType.Active;
        public override SkillEffectType Effect => SkillEffectType.Damage;

        public override float ManaCost => 15f;


        //스킬 실행 로직
        public override bool Execute(Player player, Monster target)
        {
            if (!CanUse(player))
            {
                Console.WriteLine($"MP가 부족하여 '{Name}' 스킬을 사용할 수 없습니다. (현재 MP: {ManaCost})");
                return false;
            }

            player.Mana -= ManaCost;

            //데미지 = 플레이어 기본 공격력*2 *{(디렉터 강화 계수) = 기본값 1, 디렉터는 1.5}
            float damage = Player.Instance.Defense * PromotionMultiplier;

            // 아래 코드에서 몬스터 방어력에 따른 데미지 계산 로직 추가
            float finalDamege = Player.Instance.ApplyDefenseReduction(damage, target.Defense);
            Console.WriteLine($"{player.Name}이(가) '방패치기' 스킬을 사용했습니다!");
            Console.WriteLine($"{target.Name}에게 {damage}의 피해를 입혔습니다!");

            target.OnDamaged(finalDamege);

            if (target.IsDead)
            {
                // Console.WriteLine($"\n{target.Name}을(를) 처치했습니다!");
                Player.Instance.GainExp(target.Level);
                //만일 몬스터 별로 경험치가 다르게 구현해서
                //속성을 추가해서 파라미터로 받아오게 하면
                //Gain(target.Exp);
            }

            return true;
        }
    }
}

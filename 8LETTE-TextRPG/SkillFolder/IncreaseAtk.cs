using _8LETTE_TextRPG.MonsterFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG.SkillFolder
{
    public class IncreaseAtk : Skill
    {
        public override string Name => "공격력 증가"; //Todo : 스킬명 수정이 필요할 것 같습니다.
        public override string Description => " 공격력이 20% 증가합니다.";

        public override SkillType Type => SkillType.Active;
        public override SkillEffectType Effect => SkillEffectType.Buff;
        public override float ManaCost => base.ManaCost;


        //스킬 실행 로직
        public override bool Execute(Player player, Monster target)
        {
            if (!CanUse(player))
            {
                Console.WriteLine($"MP가 부족하여 '{Name}' 스킬을 사용할 수 없습니다. (필요 MP: {ManaCost})");
                return false;
            }

            player.Mana -= ManaCost;

            Buff buff = new Buff(
                name: "공격력 증가",
                atkMultiplier: 1.2f * PromotionMultiplier,
                defMultiplier: 1,
                criticalMultiplier: 1,
                evasionMultiplier: 1,
                turns: -1,//전투가 끝날 때 까지 유지하기 위해 의미 없는 값 대입
                duration: DurationType.UntilBattleEnd// 전투가 끝날때 까지
            );

            Player.Instance.AddBuff(buff);

            return true;
        }
    }
}

using _8LETTE_TextRPG.MonsterFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG.SkillFolder
{
    public class IncreaseEvasion : Skill
    {
        public override string Name => "회피율 증가";
        public override string Description => " 회피율이 20 증가합니다.";

        public override SkillType Type => SkillType.Active;
        public override SkillEffectType Effect => SkillEffectType.Buff;

        public override float ManaCost => base.ManaCost;

        public override bool Execute(Player player, Monster _)
        {
            if (!CanUse(player))
            {
                Console.WriteLine($"MP가 부족하여 '{Name}' 스킬을 사용할 수 없습니다. (현재 MP: {ManaCost})");
                return false;
            }

            player.Mana -= ManaCost;

            Buff buff = new Buff(
                name: "회피율 증가",
               atkMultiplier: 1,
               defMultiplier: 1f,
               criticalMultiplier: 0f,
               evasionMultiplier: 10f * PromotionMultiplier,
               turns: -1,//전투가 끝날 때 까지 유지하기 위해 의미 없는 값 대입
               duration: DurationType.UntilBattleEnd// 전투가 끝날때 까지
            );

            Player.Instance.AddBuff(buff);

            return true;
        }
    }
}

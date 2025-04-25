using _8LETTE_TextRPG.MonsterFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG.SkillFolder
{
    public class Rampage : Skill
    {
        public override string Name => "난사";
        public override string Description => " 몬스터 절반에게 무작위로 공격합니다(치명타 가능).";

        public override SkillType Type => SkillType.Active;
        public override SkillEffectType Effect => SkillEffectType.Damage;
        public override float ManaCost => 15f;

        public override bool Execute(Player player, Monster monster)
        {
            if (!CanUse(player))
            {
                Console.WriteLine($"MP가 부족하여 '{Name}' 스킬을 사용할 수 없습니다. (현재 MP: {ManaCost})");
                return false;
            }

            player.Mana -= ManaCost;

            //적의 수 가져오기
            List<Monster> m = MonsterSpawner.Instance.GetAllMonsters().Where(m => !m.IsDead).ToList();

            //적의 절반을 무작위로 공격
            int total = m.Count;
            int half = (int)Math.Ceiling(total / 2.0);

            var r = new Random();
            var selected = m.OrderBy(monster => r.Next()).Take(half);

            float baseAtk = player.Attack;
            float rawDamage = (float)Math.Ceiling(baseAtk) * PromotionMultiplier;
            float dmg;
            foreach (Monster mon in selected)
            {
                if (player.TryCritical())
                {
                    dmg = (float)Math.Ceiling(rawDamage * 1.6);
                    dmg = player.ApplyDefenseReduction(rawDamage, mon.Defense);

                    Console.WriteLine($"Lv.{mon.Level} {mon.Name}에게 {dmg}의 피해를 입혔습니다.");
                    mon.OnDamaged(dmg);
                }
                else
                {
                    dmg = player.ApplyDefenseReduction(rawDamage, mon.Defense);

                    Console.WriteLine($"Lv.{mon.Level} {mon.Name}에게 {dmg}의 피해를 입혔습니다.");
                    mon.OnDamaged(dmg);
                }

                if (mon.IsDead)
                {
                    // Console.WriteLine($"\n{mon.Name}을(를) 처치했습니다!");
                    player.GainExp(mon.Level);
                }
            }

            return true;
        }
    }
}

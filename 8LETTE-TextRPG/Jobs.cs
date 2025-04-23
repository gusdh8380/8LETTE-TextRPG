using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG
{
  //직업 : 주니어
    public class Junior : JobBase 
    {
        public override string Name => "주니어";
        public override float BaseAttack => 12f;
        public override float BaseDefense => 5f;
        public override float BaseHealth => 100f;
        public override int CriticalChance => 15;
        public override int EvasionRate => 10;
       
        
        //승급 스테이지 기록
        //1이면 승급?
        public override int PromotionStage => 0;

        public override List<Skill> Skills => new()
        {
            new YaguenSkill()
            
        };

        public override void IncreaseStats(Player player)
        {
            player.BaseAttack += 0.5f;
            player.BaseDefense += 0.5f;
        }
    }

    //직업 : 버그워리어(미들)
    public class BugWarrior_Middle : JobBase
    {
        public override string Name => "버그워리어 (미들)";
        public override float BaseAttack => 15f;
        public override float BaseDefense => 5f;
        public override float BaseHealth => 100f;
        public override int CriticalChance => 15;
        public override int EvasionRate => 10;
        public override int PromotionStage => 1;

        public override List<Skill> Skills => new()
        { 
            new YaguenSkill(),//전 직업 스킬 
            new IncreaseAtk()
        };

        public override void IncreaseStats(Player player)
        {
            player.BaseAttack += 1f;
            player.BaseDefense += 0.5f;
        }
    }

}

using _8LETTE_TextRPG.MonsterFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG.SkillFolder
{
    //스킬 :  추상 클래스 사용 
    //*인터페이스로 구현? -> 일단 추상 클래스로 구현함
    public abstract class Skill
    {
        public abstract string Name { get; }
        public abstract SkillType Type { get; }
        public abstract SkillEffectType Effect { get; }
        public abstract string Description { get; }

        /// <summary>
        /// 스킬 사용 시 소모되는 MP
        /// </summary>
        public virtual float ManaCost => 10f;  // 기본값, 각 스킬별로 오버라이드 가능

        /// <summary>
        /// 디렉터 승진 시 기본 스킬 강화 계수
        /// 1.0 = 기본, 1.8 = 디렉터 
        /// Job.PromotionStage에 따라 JobBase가 설정
        /// </summary>
        public float PromotionMultiplier { get; set; }

        /// <summary>
        /// 현재 MP로 스킬 사용 가능 여부
        /// </summary>
        public virtual bool CanUse(Player player) => player.Mana >= ManaCost;

        public abstract bool Execute(Player player, Monster target);

        public Skill()
        {
            PromotionMultiplier = 1f;
        }
    }
}



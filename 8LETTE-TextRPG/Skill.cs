using _8LETTE_TextRPG.MonsterFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace _8LETTE_TextRPG
{
    //스킬 :  추상 클래스 사용 
    //*인터페이스로 구현? -> 일단 추상 클래스로 구현함
    
    public enum SkillType { Active, Passive }
    public enum EffectType { Damage, Buff}

    public abstract class Skill
    {
        public abstract string Name { get; }
        public abstract SkillType Type { get; }
        public abstract EffectType Effect { get; }
        public abstract string Description { get; }

        /// <summary>
        /// 디렉터 승진 시 기본 스킬 강화 계수
        /// 1.0 = 기본 1.8 = 디렉터 
        /// Job.PromotionStage에 따라 JobBase가 설정
        /// </summary>
        public float PromotionMultiplier { get; set; } = 1f;

        public abstract void Execute(Player player, Monster target);

    }


    //주니어 스킬 [야근]

    public class YaguenSkill : Skill
    {
        private static Random _rand = new Random();

        public override string Name => "야근";
        public override string Description => "랜덤 적 하나에게 공격력 2배의 피해를 입힙니다.";

        public override SkillType Type => SkillType.Active;
        public override EffectType Effect => EffectType.Damage;


        //스킬 실행 로직
        public override void Execute(Player player, Monster target)
        {
            //데미지 = 플레이어 기본 공격력*2 *{(디렉터 강화 계수) = 기본값 1, 디렉터는 1.5}
            float damage = Player.Instance.TotalAttack * 2f * PromotionMultiplier;

            
            // 아래 코드에서 몬스터 방어력에 따른 데미지 계산 로직 추가
            float finalDamege = damage;
            finalDamege = Player.Instance.ApplyDefenseReduction(damage, target.Defense);

            target.OnDamaged(finalDamege);
          
            Console.WriteLine($"{player.Name}이(가) '야근' 스킬을 사용했습니다!");
            Console.WriteLine($"{target.Name}에게 {damage}의 피해를 입혔습니다!");
            if (target.IsDead)
            {
                Console.WriteLine($"\n{target.Name}을(를) 처치했습니다!");
                Player.Instance.GainExp(target.Level);
                //만일 몬스터 별로 경험치가 다르게 구현해서
                //속성을 추가해서 파라미터로 받아오게 하면
                //Gain(target.Exp);
            }

        }
    }

    //버그워리어(미들) 버프스킬 [공격력 증가] 
    public class IncreaseAtk : Skill
    {
       
        public override string Name => "공격력 증가"; //Todo : 스킬명 수정이 필요할 것 같습니다.
        public override string Description => " 공격력이 20% 증가합니다.";

        public override SkillType Type => SkillType.Active;
        public override EffectType Effect => EffectType.Buff;


        //스킬 실행 로직
        public override void Execute(Player player, Monster target)
        {
            var buff = new Buff(
                name: "공격력 증가",
                atkMultiplier: 1.2f*PromotionMultiplier,
                defMultiplier: 1,
                turns: -1,//전투가 끝날 때 까지 유지하기 위해 의미 없는 값 대입
                duration: DurationType.UntilBattleEnd// 전투가 끝날때 까지
                );
            
 

            Player.Instance.AddBuff(buff); 

        }
    }


    public class DebugStrike : Skill
    {
        public override string Name => "디버그스트라이크";
        public override string Description => "몬스터 전체에게 공격력의 30% 피해를 입힙니다.";
        public override SkillType Type => SkillType.Active;
        public override EffectType Effect => EffectType.Damage;

        //몬스터 전체를 공격하는 스킬이기에, 몬스터 파라마터는 무시
        public override void Execute(Player player, Monster _)
        {
            var monsters = MonsterSpawner.Instance.GetAllMonsters();
            float Atk = Player.Instance.TotalAttack;
            float rawDamage = ((float)Math.Ceiling(Atk * 0.3f)) * PromotionMultiplier;

            foreach( var monster in monsters)
            {
                if(monster.IsDead) continue;

                rawDamage = Player.Instance.ApplyDefenseReduction(rawDamage, monster.Defense);  
                monster.OnDamaged(rawDamage);
                Console.WriteLine($"Lv.{monster.Level} {monster.Name}에게 {rawDamage}의 데미지를 입혔습니다.");

                if (monster.IsDead)
                {
                    Player.Instance.GainExp(monster.Level);
                }
            }         
        } 
    }

    public class IncreaseDfs : Skill 
    {
        public override string Name => "방어력 증가";
        public override string Description => "방어력이 20% 증가합니다.";
        public override SkillType Type => SkillType.Active;
        public override EffectType Effect => EffectType.Buff;

        //몬스터 전체를 공격하는 스킬이기에, 몬스터 파라마터는 무시
        public override void Execute(Player player, Monster _)
        {
            var buff = new Buff(
               name: "방어력 증가",
               atkMultiplier: 1,
               defMultiplier: 1.2f* PromotionMultiplier,
               turns: -1,//전투가 끝날 때 까지 유지하기 위해 의미 없는 값 대입
               duration: DurationType.UntilBattleEnd// 전투가 끝날때 까지
               );

            Player.Instance.AddBuff(buff);

        }
    }

    public class ShieldStrike : Skill
    {
        public override string Name => "방패치기"; 
        public override string Description => " 방어력의 100% 피해를 입힙니다.";

        public override SkillType Type => SkillType.Active;
        public override EffectType Effect => EffectType.Damage;


        //스킬 실행 로직
        public override void Execute(Player player, Monster target)
        {
            //데미지 = 플레이어 기본 공격력*2 *{(디렉터 강화 계수) = 기본값 1, 디렉터는 1.5}
            float damage = Player.Instance.TotalDefense * PromotionMultiplier;


            // 아래 코드에서 몬스터 방어력에 따른 데미지 계산 로직 추가
            float finalDamege = Player.Instance.ApplyDefenseReduction(damage, target.Defense);

            target.OnDamaged(finalDamege);

            Console.WriteLine($"{player.Name}이(가) '방패치기' 스킬을 사용했습니다!");
            Console.WriteLine($"{target.Name}에게 {damage}의 피해를 입혔습니다!");
            if (target.IsDead)
            {
                Console.WriteLine($"\n{target.Name}을(를) 처치했습니다!");
                Player.Instance.GainExp(target.Level);
                //만일 몬스터 별로 경험치가 다르게 구현해서
                //속성을 추가해서 파라미터로 받아오게 하면
                //Gain(target.Exp);
            }

        }
    }


}

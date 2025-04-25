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
    public enum EffectType { Damage, Buff }

    public abstract class Skill
    {
        public abstract string Name { get; }
        public abstract SkillType Type { get; }
        public abstract EffectType Effect { get; }
        public abstract string Description { get; }

        /// <summary>
        /// 스킬 사용 시 소모되는 MP
        /// </summary>
        public virtual float ManaCost => 10f;  // 기본값, 각 스킬별로 오버라이드 가능
        /// <summary>
        /// 현재 MP로 스킬 사용 가능 여부
        /// </summary>
        public virtual bool CanUse(Player player) => player.ManaPoint >= ManaCost;

        /// <summary>
        /// 디렉터 승진 시 기본 스킬 강화 계수
        /// 1.0 = 기본 1.8 = 디렉터 
        /// Job.PromotionStage에 따라 JobBase가 설정
        /// </summary>
        public float PromotionMultiplier { get; set; } = 1f;

        public abstract bool Execute(Player player, Monster target);

    }

    //주니어 기본스킬 [야근]
    public class YaguenSkill : Skill
    {
        

        public override string Name => "야근";
        public override string Description => "랜덤 적 하나에게 공격력 2배의 피해를 입힙니다.";

        public override SkillType Type => SkillType.Active;
        public override EffectType Effect => EffectType.Damage;
        public override float ManaCost => base.ManaCost;


        //스킬 실행 로직
        public override bool Execute(Player player, Monster _)
        {
            if (!CanUse(player))
            {
                Console.WriteLine($"MP가 부족하여 '{Name}' 스킬을 사용할 수 없습니다. (필요 MP: {ManaCost})");
                return false;
            }

            player.ManaPoint -= ManaCost;

            var monsters = MonsterSpawner.Instance.GetAllMonsters()
                           .Where(m => !m.IsDead)
                           .ToArray();

            // 랜덤으로 한 마리 선택
            var rand = new Random();
            var chosen = monsters[rand.Next(monsters.Length)];

            //데미지 = 플레이어 기본 공격력*2 *{(디렉터 강화 계수) = 기본값 1, 디렉터는 1.5}
            float damage = Player.Instance.TotalAttack * 2f * PromotionMultiplier;

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

    #region 버그 워리어 스킬
    //버그워리어(미들) 버프스킬 [공격력 증가] 
    public class IncreaseAtk : Skill
    {

        public override string Name => "공격력 증가"; //Todo : 스킬명 수정이 필요할 것 같습니다.
        public override string Description => " 공격력이 20% 증가합니다.";

        public override SkillType Type => SkillType.Active;
        public override EffectType Effect => EffectType.Buff;
        public override float ManaCost => base.ManaCost;


        //스킬 실행 로직
        public override bool Execute(Player player, Monster target)
        {
            if (!CanUse(player))
            {
                Console.WriteLine($"MP가 부족하여 '{Name}' 스킬을 사용할 수 없습니다. (필요 MP: {ManaCost})");
                return false;
            }

            player.ManaPoint -= ManaCost;

            var buff = new Buff(
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

    public class DebugStrike : Skill
    {
        public override string Name => "디버그스트라이크";
        public override string Description => "몬스터 전체에게 공격력의 30% 피해를 입힙니다.";
        public override SkillType Type => SkillType.Active;
        public override EffectType Effect => EffectType.Damage;

        public override float ManaCost => 15f;

        //몬스터 전체를 공격하는 스킬이기에, 몬스터 파라마터는 무시
        public override bool Execute(Player player, Monster _)
        {
            if (!CanUse(player))
            {
                Console.WriteLine($"MP가 부족하여 '{Name}' 스킬을 사용할 수 없습니다. (필요 MP: {ManaCost})");
                return false;
            }

            player.ManaPoint -= ManaCost;

            var monsters = MonsterSpawner.Instance.GetAllMonsters();
            float Atk = Player.Instance.TotalAttack;
            float rawDamage = ((float)Math.Ceiling(Atk * 0.3f)) * PromotionMultiplier;

            foreach (var monster in monsters)
            {
                if (monster.IsDead) continue;

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
    #endregion


    #region 메모리나이트 스킬
    public class IncreaseDfs : Skill
    {
        public override string Name => "방어력 증가";
        public override string Description => "방어력이 20% 증가합니다.";
        public override SkillType Type => SkillType.Active;
        public override EffectType Effect => EffectType.Buff;
        public override float ManaCost => base.ManaCost;

        public override bool Execute(Player player, Monster _)
        {
            if (!CanUse(player))
            {
                Console.WriteLine($"MP가 부족하여 '{Name}' 스킬을 사용할 수 없습니다. (필요 MP: {ManaCost})");
                return false;
            }

            player.ManaPoint -= ManaCost;


            var buff = new Buff(
               name: "방어력 증가",
               atkMultiplier: 1,
               defMultiplier: 1.2f * PromotionMultiplier,
               criticalMultiplier: 1,
               evasionMultiplier: 1,
               turns: -1,//전투가 끝날 때 까지 유지하기 위해 의미 없는 값 대입
               duration: DurationType.UntilBattleEnd// 전투가 끝날때 까지
               );

            Player.Instance.AddBuff(buff);

            return true;
        }
    }

    public class ShieldStrike : Skill
    {
        public override string Name => "방패치기";
        public override string Description => " 방어력의 100% 피해를 입힙니다.";

        public override SkillType Type => SkillType.Active;
        public override EffectType Effect => EffectType.Damage;

        public override float ManaCost => 15f;


        //스킬 실행 로직
        public override bool Execute(Player player, Monster target)
        {

            if (!CanUse(player))
            {
                Console.WriteLine($"MP가 부족하여 '{Name}' 스킬을 사용할 수 없습니다. (현재 MP: {ManaCost})");
                return false;
            }

            player.ManaPoint -= ManaCost;

            //데미지 = 플레이어 기본 공격력*2 *{(디렉터 강화 계수) = 기본값 1, 디렉터는 1.5}
            float damage = Player.Instance.TotalDefense * PromotionMultiplier;


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
    #endregion

    #region 스레드 헌터 스킬 
    public class IncreaseEvasion : Skill
    {
        public override string Name => "회피율 증가";
        public override string Description => " 회피율이 20 증가합니다.";

        public override SkillType Type => SkillType.Active;
        public override EffectType Effect => EffectType.Buff;

        public override float ManaCost => base.ManaCost;

        public override bool Execute(Player player, Monster _)
        {
            if (!CanUse(player))
            {
                Console.WriteLine($"MP가 부족하여 '{Name}' 스킬을 사용할 수 없습니다. (현재 MP: {ManaCost})");
                return false;
            }

            player.ManaPoint -= ManaCost;

            var buff = new Buff(
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

    public class Counterattack : Skill
    {
        public override string Name => "카운터어택!";
        public override string Description => " 몬스터의 공격을 회피 시, 공격력의 50% 피해를 줍니다 ";

        public override SkillType Type => SkillType.Passive;
        public override EffectType Effect => EffectType.Damage;
        public override float ManaCost => 15f;

        public override bool Execute(Player player, Monster monster)
        {
            float rawDamage = player.TotalAttack * 0.5f * PromotionMultiplier;
            float finalDamage = player.ApplyDefenseReduction(rawDamage, monster.Defense);
            Console.WriteLine($"{player.Name}이(가) '{Name}' 스킬로 {monster.Name}에게 {finalDamage}의 카운터 어택를 입혔습니다!");

            monster.OnDamaged(finalDamage);
        


            if (monster.IsDead)
            {
                //Console.WriteLine($"\n{monster.Name}을(를) 처치했습니다!");
                player.GainExp(monster.Level);

            }
            
            return true;
        }
    }
    #endregion

    #region 익셉션 헌터 스킬
    public class IncreaseCritical : Skill
    {
        public override string Name => "치명타 증가";
        public override string Description => " 치명타가 20 증가합니다.";

        public override SkillType Type => SkillType.Active;
        public override EffectType Effect => EffectType.Buff;
        public override float ManaCost => base.ManaCost;

        public override bool Execute(Player player, Monster _)
        {
            if (!CanUse(player))
            {
                Console.WriteLine($"MP가 부족하여 '{Name}' 스킬을 사용할 수 없습니다. (현재 MP: {ManaCost})");
                return false;
            }

            player.ManaPoint -= ManaCost;

            var buff = new Buff(
                name: "치명타 증가",
               atkMultiplier: 1,
               defMultiplier: 1f,
               criticalMultiplier: 10f * PromotionMultiplier,
               evasionMultiplier: 0f ,
               turns: -1,//전투가 끝날 때 까지 유지하기 위해 의미 없는 값 대입
               duration: DurationType.UntilBattleEnd// 전투가 끝날때 까지
               );
            Player.Instance.AddBuff(buff);

            return true;
        }
    }

    //rampage : 난사
    public class Rampage : Skill
    {
        public override string Name => "난사";
        public override string Description => " 몬스터 절반에게 무작위로 공격합니다(치명타 가능).";

        public override SkillType Type => SkillType.Active;
        public override EffectType Effect => EffectType.Damage;
        public override float ManaCost => 15f;

        public override bool Execute(Player player, Monster monster)
        {
            if (!CanUse(player))
            {
                Console.WriteLine($"MP가 부족하여 '{Name}' 스킬을 사용할 수 없습니다. (현재 MP: {ManaCost})");
                return false;
            }

            player.ManaPoint -= ManaCost;

            //적의 수 가져오기
            var m = MonsterSpawner.Instance.GetAllMonsters()
                          .Where(m => !m.IsDead)
                          .ToList();
            //적의 절반을 무작위로 공격
            int total = m.Count;
            int half = (int)Math.Ceiling(total / 2.0);

            var r = new Random();
            var selected = m.OrderBy(monster => r.Next()).Take(half);

            float baseAtk = player.TotalAttack;
            float rawDamage = (float)Math.Ceiling(baseAtk) * PromotionMultiplier;
            float dmg;
            foreach (var mon in selected)
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

        #endregion

    }
}



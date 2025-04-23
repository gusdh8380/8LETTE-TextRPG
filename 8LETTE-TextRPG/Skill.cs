using _8LETTE_TextRPG.MonsterFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG
{
    //스킬 :  추상 클래스 사용
    public enum SkillType { Active, Passive }
    public enum EffectType { Damage, Buff, Debuff, Heal, Utility }

    public abstract class Skill
    {
        public abstract string Name { get; }
        public abstract SkillType Type { get; }
        public abstract EffectType Effect { get; }
        public abstract string Description { get; }

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
            float damage = Player.Instance.BaseAttack * 2f;

            //Todo : 아래 코드에서 몬스터 방어력에 따른 데미지 계산 로직 추가
            float finalDamege = damage;

            target.OnDamaged(damage);
          
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

    //버그워리어(미들) 스킬 [공격력 증가] 
    public class IncreaseAtk : Skill
    {
       
        public override string Name => "공격력 증가"; //스킬 명 수정이 필요할 것 같습니다.
        public override string Description => "공격력이 20% 증가합니다.";

        public override SkillType Type => SkillType.Active;
        public override EffectType Effect => EffectType.Buff;


        //스킬 실행 로직
        public override void Execute(Player player, Monster target)
        {
            float originAtk = Player.Instance.BaseAttack;
            float boostedAtk = originAtk * 1.2f;


            Random r = new Random();
            float varirance = (float)Math.Ceiling(boostedAtk * 0.1f);

            //몬스터에게 피해를 입힐 데미지 계산
            //Todo : 몬스터 방어력에 따른 데미지 감소 로직도 염두
            //현재는 방어력 무시


            float damage = boostedAtk + r.Next(-(int)varirance, (int)varirance);
            damage = Math.Max(1, damage);//최소 데미지 보장

            //크리티컬 계산
            bool isCritical = Player.Instance.TryCritical();
            if (isCritical)
            {
                damage = (float)Math.Ceiling(damage * 1.6);

                //데미지 계산 처리는 몬스터 클래스에서
                target.OnDamaged(damage);

                Console.WriteLine($"{Name}의 공격!");
                Console.WriteLine($"Lv.{target.Level} {target.Name}을 공격.  {damage}의 데미지 - 치명타 공격!!");
            }
            else
            {
                //데미지 계산 처리는 몬스터 클래스에서
                target.OnDamaged(damage);

                Console.WriteLine($"{Name}의 공격!");
                Console.WriteLine($"Lv.{target.Level} {target.Name}에게 {damage}의 데미지를 입혔습니다.");
            }

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

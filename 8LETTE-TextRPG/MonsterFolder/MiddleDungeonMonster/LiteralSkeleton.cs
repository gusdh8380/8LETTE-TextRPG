using _8LETTE_TextRPG.ItemFolder;

namespace _8LETTE_TextRPG.MonsterFolder.MiddleDungeonMonster
{
    public class LiteralSkeleton : Monster
    {
        Random r = new Random();
        public LiteralSkeleton()
        {
            Type = MonsterType.Undead;
            Name = "리터럴 스켈레톤";
            Level = 7;
            MaxHp = 100f;
            Hp = MaxHp;
            Attack = 22f;
            Defense = 11f;
            GoldReward = 350;
        }

        protected override void DefineStates()
        {
            AddState(State.Idle, new StateElem
            {
                Doing = new Action(OnIdle)
            });

            AddState(State.Attack, new StateElem
            {
                Doing = new Action(AttackDoing)
            });

            AddState(State.Dead, new StateElem
            {
                Doing = new Action(OnDeath)
            });
        }

        private void OnIdle()
        {

        }

        private void AttackDoing()
        {
            AttackTo(Player.Instance);
        }

        private void OnDeath()
        {
            Console.WriteLine($"{Name}을(를) 처지했습니다!");
            if (r.Next(1, 101) <= 100)//30% 확률로 아래 아이템을 드랍
            {
                Player.Instance.Inventory.AddItem(new Potion("테스트 물약 f(30)", "사용 시 HP를 30 회복합니다.", 100f, new Dictionary<ItemEffect, float>
                 {
                     { ItemEffect.Hp, 30f }
                 }));

                Console.WriteLine($"{Name}이(가) 테스트 물약을 드랍했습니다.");

            }
        }

        public override void OnDamaged(float dmg)
        {
            base.OnDamaged(dmg);
        }

        public override void AttackTo(Player victim)
        {
            base.AttackTo(victim);
        }
    }
}

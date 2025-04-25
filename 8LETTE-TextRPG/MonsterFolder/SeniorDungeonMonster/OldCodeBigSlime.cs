using _8LETTE_TextRPG.ItemFolder;

namespace _8LETTE_TextRPG.MonsterFolder.SeniorDungeonMonster
{
    class OldCodeBigSlime : Monster
    {
        Random r = new Random();
        public OldCodeBigSlime()
        {
            Type = MonsterType.Slime;
            Name = "올드코드 빅슬라임";
            Level = 11;
            MaxHp = 40f;
            Hp = MaxHp;
            Attack = 30f;
            Defense = 30f;
            GoldReward = 600;
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
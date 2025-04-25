using _8LETTE_TextRPG.ItemFolder;
using _8LETTE_TextRPG.ScreenFolder;

namespace _8LETTE_TextRPG.MonsterFolder.MiddleDungeonMonster
{
    class MemoryMelter : Monster
    {
           Random r = new Random();
        public MemoryMelter()
        {
            Type = MonsterType.Slime;
            Name = "메모리 멜터";
            Level = 8;
            MaxHp = 120f;
            Hp = MaxHp;
            Attack = 24f;
            Defense = 12f;
            GoldReward = 400;
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
            Random r = new Random();
            Console.WriteLine($"{Name}를 처지했습니다!");
            if (r.Next(1, 101) <= 15)
            {
                Player.Instance.Inventory.AddItem(new Potion(15f));

                Console.WriteLine($"{Name}가 체력 포션 (15)을 드랍했습니다.");
                BattleResultScreen.Instance.PrintDropItem += () => Console.WriteLine($"{Name}가 체력 포션 (15)을 드랍했습니다.");
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

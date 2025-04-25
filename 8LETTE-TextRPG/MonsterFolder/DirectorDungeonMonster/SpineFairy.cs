using _8LETTE_TextRPG.ItemFolder;
using _8LETTE_TextRPG.ScreenFolder;

namespace _8LETTE_TextRPG.MonsterFolder.DirectorDungeonMonster
{
    class SpineFairy : Monster
    {
        Random r = new Random();
        public SpineFairy()
        {
            Type = MonsterType.Fairy;
            Name = "척추의 요정";
            Level = 18;
            MaxHp = 320f;
            Hp = MaxHp;
            Attack = 44f;
            Defense = 22f;
            GoldReward = 1150;
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
            Console.WriteLine($"{Name}을 처지했습니다!");
            if (r.Next(1, 101) <= 15)
            {
                Player.Instance.Inventory.AddItem(new UsableItem(25f));

                Console.WriteLine($"{Name}이 체력 포션 (25)을 드랍했습니다.");
                BattleResultScreen.Instance.PrintDropItem += () => Console.WriteLine($"{Name}이 체력 포션 (25)을 드랍했습니다.");
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

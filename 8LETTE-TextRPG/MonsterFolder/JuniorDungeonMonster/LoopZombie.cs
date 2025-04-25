using _8LETTE_TextRPG.ItemFolder;
using _8LETTE_TextRPG.ScreenFolder;

namespace _8LETTE_TextRPG.MonsterFolder.JuniorDungeonMonster
{
    class LoopZombie : Monster
    {

        Random r = new Random();

        public LoopZombie()
        {
            Type = MonsterType.Undead;
            Name = "무한루프 좀비";
            Level = 3;
            MaxHp = 25f;
            Hp = MaxHp;
            Attack = 5f;
            Defense = 5f;
            GoldReward = 150;
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
                Player.Instance.Inventory.AddItem(new UsableItem(10f));

                Console.WriteLine($"{Name}가 체력 포션 (10)을 드랍했습니다.");
                BattleResultScreen.Instance.PrintDropItem += () => Console.WriteLine($"{Name}가 체력 포션 (10)을 드랍했습니다.");
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
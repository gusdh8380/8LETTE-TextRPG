using _8LETTE_TextRPG.ItemFolder;
using _8LETTE_TextRPG.ScreenFolder;

namespace _8LETTE_TextRPG.MonsterFolder.MiddleDungeonMonster
{
    class IllusionPixie : Monster
    {
           Random r = new Random();
        public IllusionPixie()
        {
            Type = MonsterType.Fairy;
            Name = "착각의 픽시";
            Level = 14;
            MaxHp = 45f;
            Hp = MaxHp;
            Attack = 35f;
            Defense = 35f;
            GoldReward = 800;
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
            if (r.Next(1, 101) <= 20)
            {
                Player.Instance.Inventory.AddItem(new UsableItem(20f));

                Console.WriteLine($"{Name}가 체력 포션 (20)을 드랍했습니다.");
                BattleResultScreen.Instance.PrintDropItem += () => Console.WriteLine($"{Name}가 체력 포션 (20)을 드랍했습니다.");
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
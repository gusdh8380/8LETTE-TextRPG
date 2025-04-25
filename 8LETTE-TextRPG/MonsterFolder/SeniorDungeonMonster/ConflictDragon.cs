using _8LETTE_TextRPG.ItemFolder;
using _8LETTE_TextRPG.ScreenFolder;

namespace _8LETTE_TextRPG.MonsterFolder.MiddleDungeonMonster
{
    class ConflictDragon : Monster
    {
        Random r = new Random();    
        public ConflictDragon()
        {
            Type = MonsterType.Dragon;
            Name = "컨플릭트 드래곤";
            Level = 15;
            MaxHp = 50f;
            Hp = MaxHp;
            Attack = 40f;
            Defense = 40f;
            GoldReward = 900;
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
            if (r.Next(1, 101) <= 25)
            {
                Player.Instance.Inventory.AddItem(new UsableItem(20f));

                Console.WriteLine($"{Name}이 체력 포션 (20)을 드랍했습니다.");
                BattleResultScreen.Instance.PrintDropItem += () => Console.WriteLine($"{Name}이 체력 포션 (20)을 드랍했습니다.");
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
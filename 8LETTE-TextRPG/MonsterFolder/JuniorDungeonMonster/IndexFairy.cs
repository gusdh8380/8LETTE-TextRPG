using _8LETTE_TextRPG.ItemFolder;
using _8LETTE_TextRPG.ScreenFolder;

namespace _8LETTE_TextRPG.MonsterFolder.JuniorDungeonMonster
{
    class IndexFairy : Monster
    {

        Random r = new Random();
        public IndexFairy()
        {
            Type = MonsterType.Fairy;
            Name = "인덱스 페어리";
            Level = 4;
            MaxHp = 15f;
            Hp = MaxHp;
            Attack = 13f;
            Defense = 3f;
            GoldReward = 175;
        }
        //

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
                Player.Instance.Inventory.AddItem(new Potion(10f));

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

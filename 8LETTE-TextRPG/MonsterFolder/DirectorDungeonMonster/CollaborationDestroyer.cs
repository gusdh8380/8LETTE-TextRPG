using _8LETTE_TextRPG.ItemFolder;
using _8LETTE_TextRPG.ScreenFolder;

namespace _8LETTE_TextRPG.MonsterFolder.DirectorDungeonMonster
{
    public class CollaborationDestroyer : Monster
    {
        Random r = new Random();
        public CollaborationDestroyer()
        {
            Type = MonsterType.Humanoid;
            Name = "협업 파괴자";
            Level = 20;
            MaxHp = 400f;
            Hp = MaxHp;
            Attack = 50f;
            Defense = 25f;
            GoldReward = 1300;
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
            if (r.Next(1, 101) <= 25)
            {
                Player.Instance.Inventory.AddItem(new Potion(25f));

                Console.WriteLine($"{Name}가 체력 포션 (25)을 드랍했습니다.");
                BattleResultScreen.Instance.PrintDropItem += () => Console.WriteLine($"{Name}가 체력 포션 (25)을 드랍했습니다.");
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

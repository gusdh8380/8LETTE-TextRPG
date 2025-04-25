using _8LETTE_TextRPG.ItemFolder;
using _8LETTE_TextRPG.ScreenFolder;

namespace _8LETTE_TextRPG.MonsterFolder.DirectorDungeonMonster
{
    public class TiredWebSpider : Monster
    {
        Random r = new Random();
        public TiredWebSpider()
        {
            Type = MonsterType.Spider;
            Name = "피곤줄 거미";
            Level = 17;
            MaxHp = 300f;
            Hp = MaxHp;
            Attack = 42f;
            Defense = 21f;
            GoldReward = 1050;
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
            Console.WriteLine($"{Name}을(를) 처지했습니다!");
            if (r.NextSingle() <= 0.1f)
            {
                Item droppedItem = new UsableItem(Guid.NewGuid().ToString(), "체력 포션 (25)", "사용 시 HP를 25 회복합니다.", 85f, UseType.Potion, new Dictionary<ItemEffect, float>
                {
                    { ItemEffect.Hp, 25 }
                });
                Player.Instance.Inventory.AddItem(droppedItem);

                Console.WriteLine($"{Name}(이)가 {droppedItem.Name}을(를) 드랍했습니다.");
                BattleResultScreen.Instance.PrintDropItem += () => Console.WriteLine($"{Name}(이)가 {droppedItem.Name}을(를) 드랍했습니다.");
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

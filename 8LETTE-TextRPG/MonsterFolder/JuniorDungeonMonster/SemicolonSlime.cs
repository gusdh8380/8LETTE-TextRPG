﻿using _8LETTE_TextRPG.ItemFolder;
using _8LETTE_TextRPG.ScreenFolder;

namespace _8LETTE_TextRPG.MonsterFolder.JuniorDungeonMonster
{
    class SemicolonSlime : Monster
    {  
        public SemicolonSlime()
        {
            Type = MonsterType.Slime;
            Name = "세미콜론 슬라임";
            Level = 1;
            MaxHp = 15f;
            Hp = MaxHp;
            Attack = 3f;
            Defense = 1f;
            GoldReward = 100;
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
            if (r.NextSingle() <= 0.05f)
            {
                Item droppedItem = new UsableItem(Guid.NewGuid().ToString(), "체력 포션 (10)", "사용 시 HP를 10 회복합니다.", 40f, UseType.Potion, new Dictionary<ItemEffect, float>
                {
                    { ItemEffect.Hp, 10 }
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

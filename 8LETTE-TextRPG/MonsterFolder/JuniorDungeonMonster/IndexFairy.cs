namespace _8LETTE_TextRPG.MonsterFolder.JuniorDungeonMonster
{
    class IndexFairy : Monster
    {
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

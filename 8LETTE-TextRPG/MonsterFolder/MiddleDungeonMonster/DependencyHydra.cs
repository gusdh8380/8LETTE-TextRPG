namespace _8LETTE_TextRPG.MonsterFolder.MiddleDungeonMonster
{
    class DependencyHydra : Monster
    {
        public DependencyHydra()
        {
            Type = MonsterType.Dragon;
            Name = "디펜던시 히드라";
            Level = 10;
            MaxHp = 160f;
            Hp = MaxHp;
            Attack = 28f;
            Defense = 14f;
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

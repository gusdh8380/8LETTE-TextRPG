namespace _8LETTE_TextRPG.MonsterFolder.MiddleDungeonMonster
{
    class MemoryMelter : Monster
    {
        public MemoryMelter()
        {
            Type = MonsterType.Slime;
            Name = "메모리 멜터";
            Level = 8;
            MaxHp = 120f;
            Hp = MaxHp;
            Attack = 24f;
            Defense = 12f;
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

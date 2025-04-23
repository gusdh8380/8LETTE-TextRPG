namespace _8LETTE_TextRPG.MonsterFolder.DirectorDungeonMonster
{
    class  BurnoutThread : Monster
    {
        public BurnoutThread()
        {
            Name = "번아웃 스레드";
            Level = 20;
            MaxHp = 280f;
            Hp = MaxHp;
            Attack = 40f;
            Defense = 21f;
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

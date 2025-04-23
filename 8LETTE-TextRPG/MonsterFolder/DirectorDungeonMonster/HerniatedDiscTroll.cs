namespace _8LETTE_TextRPG.MonsterFolder.DirectorDungeonMonster
{
    class HerniatedDiscTroll : Monster
    {
        public HerniatedDiscTroll()
        {
            Name = "허리디스크 트롤";
            Level = 22;
            MaxHp = 320f;
            Hp = MaxHp;
            Attack = 44f;
            Defense = 22f;
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

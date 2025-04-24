namespace _8LETTE_TextRPG.MonsterFolder.JuniorDungeonMonster
{
    class TypeMissGoblin : Monster
    {
        public TypeMissGoblin()
        {
            Type = MonsterType.Humanoid;
            Name = "타입미스 고블린";
            Level = 2;
            MaxHp = 20f;
            Hp = MaxHp;
            Attack = 5f;
            Defense = 2f;
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

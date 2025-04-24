namespace _8LETTE_TextRPG.MonsterFolder.DirectorDungeonMonster
{
    class VoidDragon : Monster
    {
        public VoidDragon()
        {
            Type = MonsterType.Dragon;
            Name = "보이드 드래곤";
            Level = 15;
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

namespace _8LETTE_TextRPG.MonsterFolder.JuniorDungeonMonster
{
    class LoopZombie : Monster
    {
        public LoopZombie()
        {
            Type = MonsterType.Undead;
            Name = "무한루프 좀비";
            Level = 3;
            MaxHp = 25f;
            Hp = MaxHp;
            Attack = 5f;
            Defense = 5f;
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
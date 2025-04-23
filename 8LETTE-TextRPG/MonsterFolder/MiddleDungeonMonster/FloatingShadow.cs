namespace _8LETTE_TextRPG.MonsterFolder.MiddleDungeonMonster
{
    public class FloatingShadow : Monster
    {
        public FloatingShadow()
        {
            Name = "플로팅 쉐도우";
            Level = 7;
            MaxHp = 100f;
            Hp = MaxHp;
            Attack = 22f;
            Defense = 11f;
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

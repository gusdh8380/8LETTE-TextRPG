namespace _8LETTE_TextRPG.MonsterFolder.MiddleDungeonMonster
{
    public class InitGhost : Monster
    {
        public InitGhost()
        {
            Type = MonsterType.Ghost;
            Name = "초기화 망령";
            Level = 6;
            MaxHp = 80f;
            Hp = MaxHp;
            Attack = 20f;
            Defense = 10f;
            GoldReward = 300;
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

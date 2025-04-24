namespace _8LETTE_TextRPG.MonsterFolder.DirectorDungeonMonster
{
    class FairyOfVertebrae : Monster
    {
        public FairyOfVertebrae()
        {
            Type = MonsterType.Fairy;
            Name = "척추의 요정";
            Level = 18;
            MaxHp = 320f;
            Hp = MaxHp;
            Attack = 44f;
            Defense = 22f;
            GoldReward = 1150;
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

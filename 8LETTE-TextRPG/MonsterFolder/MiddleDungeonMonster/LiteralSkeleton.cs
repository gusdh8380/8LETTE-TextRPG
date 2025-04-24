namespace _8LETTE_TextRPG.MonsterFolder.MiddleDungeonMonster
{
    public class LiteralSkeleton : Monster
    {
        public LiteralSkeleton()
        {
            Type = MonsterType.Undead;
            Name = "리터럴 스켈레톤";
            Level = 7;
            MaxHp = 100f;
            Hp = MaxHp;
            Attack = 22f;
            Defense = 11f;
            GoldReward = 350;
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

namespace _8LETTE_TextRPG.MonsterFolder.MiddleDungeonMonster
{
    public class LagSpider : Monster
    {
        public LagSpider()
        {
            Type = MonsterType.Spider;
            Name = "렉 스파이더";
            Level = 9;
            MaxHp = 140f;
            Hp = MaxHp;
            Attack = 26f;
            Defense = 13f;
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

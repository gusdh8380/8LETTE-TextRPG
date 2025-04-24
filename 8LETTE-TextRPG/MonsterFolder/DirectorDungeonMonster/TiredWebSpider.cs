namespace _8LETTE_TextRPG.MonsterFolder.DirectorDungeonMonster
{
    public class TiredWebSpider : Monster
    {
        public TiredWebSpider()
        {
            Type = MonsterType.Spider;
            Name = "피곤줄 거미";
            Level = 17;
            MaxHp = 300f;
            Hp = MaxHp;
            Attack = 42f;
            Defense = 21f;
            GoldReward = 1050;
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

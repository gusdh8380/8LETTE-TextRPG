namespace _8LETTE_TextRPG.MonsterFolder.DirectorDungeonMonster
{
    public class EyeblurPhantom : Monster
    {
        public EyeblurPhantom()
        {
            Type = MonsterType.Ghost;
            Name = "아이블러 팬텀";
            Level = 18;
            MaxHp = 340f;
            Hp = MaxHp;
            Attack = 46f;
            Defense = 23f;
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

namespace _8LETTE_TextRPG.MonsterFolder.DirectorDungeonMonster
{
    public class CollaborationDestroyer : Monster
    {
        public CollaborationDestroyer()
        {
            Type = MonsterType.Humanoid;
            Name = "협업 파괴자";
            Level = 20;
            MaxHp = 400f;
            Hp = MaxHp;
            Attack = 50f;
            Defense = 25f;
            GoldReward = 1300;
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

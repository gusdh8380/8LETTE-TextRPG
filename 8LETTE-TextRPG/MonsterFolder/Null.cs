namespace _8LETTE_TextRPG.MonsterFolder
{
        class Null : Monster
        {
            public Null()
            {
                Name = "Null의유령";
                Level = 5;
                MaxHp = 25f;
                Hp = MaxHp;
                Attack = 10f;
                Defense = 7f;
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

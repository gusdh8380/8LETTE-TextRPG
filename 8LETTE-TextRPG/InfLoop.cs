using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG
{
    class InfLoop : Monster
    {
        public InfLoop()
        {
            Name = "무한루프";
            Level = 3;
            MaxHp = 20f;
            Hp = MaxHp;
            Attack = 10f;
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
                Entered = new Action(AttackEntered),
                Doing = new Action(AttackDoing),
                Exited = new Action(AttackExited)
            });

            AddState(State.Dead, new StateElem
            {
                Doing = new Action(OnDeath)
            });
        }

        private void OnIdle()
        {

        }

        private void AttackEntered()
        {

        }

        private void AttackDoing()
        {

        }

        private void AttackExited()
        {

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

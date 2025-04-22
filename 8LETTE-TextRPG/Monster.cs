using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG
{
    public abstract class Monster
    {
        public enum State
        {
            Invalid = -1,
            Idle,
            Attack,
            Dead
        }

        public string? Name { get; protected set; }
        public int Level { get; protected set; }
        public float MaxHp { get; protected set; }
        private float _hp;
        public float Hp
        {
            get
            {
                return _hp;
            }
            set
            {
                if (value > MaxHp)
                {
                    _hp = MaxHp;
                }
                else if (value <= 0)
                {
                    _hp = 0;
                    Death();
                }
                else
                {
                    _hp = value;
                }
            }
        }
        public float Defense { get; protected set; }
        public float Attack { get; protected set; }
        public bool IsDead => CurState == State.Dead;

        protected State CurState
        {
            get
            {
                return _curState;
            }
            set
            {
                TransitionTo(value);
            }
        }

        protected State InvalidState => State.Invalid;

        private State _curState;
        private State _prevState;

        private readonly Dictionary<State, StateElem> _states = [];

        protected class StateElem
        {
            public Action? Entered;
            public Action? Doing;
            public Action? Exited;
        }

        public Monster()
        {
            DefineStates();
        }

        protected abstract void DefineStates();

        protected void TransitionTo(State nextState)
        {
            _prevState = _curState;
            _curState = nextState;
            if (_prevState != InvalidState)
            {
                if (_states.TryGetValue(_prevState, out StateElem? stateElem))
                {
                    if (stateElem != null && stateElem.Exited != null)
                    {
                        stateElem.Exited();
                    }
                }
            }

            if (_curState != InvalidState)
            {
                if (_states.TryGetValue(_curState, out StateElem? stateElem))
                {
                    if (stateElem != null)
                    {
                        stateElem.Entered?.Invoke();
                        stateElem.Doing?.Invoke();
                    }
                }
            }
        }

        protected void AddState(State state, StateElem stateElem)
        {
            _states.Add(state, stateElem);
        }

        public virtual void OnDamaged(float dmg)
        {
            Hp -= dmg;
        }

        public virtual void AttackTo(Player victim)
        {
            Random r = new Random();
            float varirance = (float)Math.Ceiling(Attack * 0.1f);

            //Todo : 방어력에 따른 데미지 감소 로직
            float damage = Attack + r.Next(-(int)varirance, (int)varirance);
            damage = Math.Max(1, damage);

            //몬스터 정보 출력
            Console.WriteLine($"Lv. {Level} {Name} 의 공격!");
            Console.WriteLine($"{victim.Name} 을(를) 맞췄습니다. [데미지 : {damage}]\n");

            //캐릭터 정보 출력
            Console.WriteLine($"Lv.{victim.Level} {victim.Name}");
            Console.WriteLine($"HP {victim.Health} -> {victim.Health - damage}\n");

            victim.OnDamaged(damage);
        }

        protected virtual void Death()
        {
            CurState = State.Dead;
        }
    }
}

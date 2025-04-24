using _8LETTE_TextRPG;
using _8LETTE_TextRPG.MonsterFolder;

class DeadlockKnight : Monster
{
    public DeadlockKnight()
    {
        Name = "데드록 나이트"; //시스템 병목 몬스터
        Level = 15;
        MaxHp = 50f;
        Hp = MaxHp;
        Attack = 40f;
        Defense = 40f;
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
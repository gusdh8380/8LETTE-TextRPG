using _8LETTE_TextRPG;
using _8LETTE_TextRPG.MonsterFolder;

class SemiColonSlime : Monster
{
    public SemiColonSlime()
    {
        Name = "세미콜론 슬라임"; //세미콜론 몬스터
        Level = 1;
        MaxHp = 10f;
        Hp = MaxHp;
        Attack = 7f;
        Defense = 2f;
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
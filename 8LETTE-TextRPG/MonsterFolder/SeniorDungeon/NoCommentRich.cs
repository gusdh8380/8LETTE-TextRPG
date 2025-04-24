using _8LETTE_TextRPG;
using _8LETTE_TextRPG.MonsterFolder;

class NoCommentRich : Monster
{
    public NoCommentRich()
    {
        Type = MonsterType.Undead;
        Name = "노코멘트 리치"; 
        Level = 12;
        MaxHp = 45f;
        Hp = MaxHp;
        Attack = 30f;
        Defense = 30f;
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
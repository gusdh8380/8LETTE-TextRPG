using _8LETTE_TextRPG;
using _8LETTE_TextRPG.MonsterFolder;

class OldCodeBigSlime : Monster
{
    public OldCodeBigSlime()
    {
        Type = MonsterType.Slime;
        Name = "올드코드 빅슬라임";
        Level = 11;
        MaxHp = 40f;
        Hp = MaxHp;
        Attack = 30f;
        Defense = 30f;
        GoldReward = 600;
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
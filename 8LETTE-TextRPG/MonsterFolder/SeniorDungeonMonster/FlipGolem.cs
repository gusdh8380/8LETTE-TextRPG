using _8LETTE_TextRPG;
using _8LETTE_TextRPG.MonsterFolder;

class FlipGolem: Monster
{
    public FlipGolem()
    {
        Type = MonsterType.Humanoid;
        Name = "뒤집기 골렘";
        Level = 13;
        MaxHp = 45f;
        Hp = MaxHp;
        Attack = 35f;
        Defense = 30f;
        GoldReward = 800;
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
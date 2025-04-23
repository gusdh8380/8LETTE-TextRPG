using _8LETTE_TextRPG.MonsterFolder.JuniorDungeon;
using _8LETTE_TextRPG;

class Documentation : Monster
{
    public Documentation()
    {
        Name = "주석의 망령"; //문서화 몬스터
        Level = 13;
        MaxHp = 45f;
        Hp = MaxHp;
        Attack = 35f;
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
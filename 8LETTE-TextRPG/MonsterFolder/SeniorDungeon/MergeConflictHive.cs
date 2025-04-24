using _8LETTE_TextRPG;
using _8LETTE_TextRPG.MonsterFolder;

class MergeConflictHive : Monster
{
    public MergeConflictHive()
    {
        Name = "머지컨플릭트 하이브"; //협업스트레스 몬스터
        Level = 11;
        MaxHp = 40f;
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
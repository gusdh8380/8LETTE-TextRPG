using _8LETTE_TextRPG;
using _8LETTE_TextRPG.MonsterFolder;

class IndexFairy : Monster
{
    public IndexFairy()
    {
        Name = "인덱스 페어리"; //배열과 리스트 몬스터
        Level = 4;
        MaxHp = 15f;
        Hp = MaxHp;
        Attack = 13f;
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
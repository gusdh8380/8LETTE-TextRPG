namespace _8LETTE_TextRPG
{
    internal class MonsterAttackScreen : Screen
    {
        public static readonly MonsterAttackScreen Instance = new MonsterAttackScreen();

        public override void Show()
        {
            Console.Clear();

            PrintTitle("전투!! - 몬스터의 공격");

            MonsterSpawner.Instance.AttackPlayer();

            PrintAnyKeyInstruction();
        }

        public override Screen? Next()
        {
            Console.ReadKey();

            if (Player.Instance.IsDead)
            {
                return BattleResultScreen.Instance;
            }

            return ActionSelectScreen.Instance;
        }
    }
}
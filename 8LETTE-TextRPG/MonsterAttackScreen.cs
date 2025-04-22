namespace _8LETTE_TextRPG
{
	internal class MonsterAttackScreen : Screen
	{
		public static readonly MonsterAttackScreen Instance = new MonsterAttackScreen();
		private MonsterAttackScreen() { }

        public override void Show()
		{
			Console.Clear();

			PrintTitle("전투!!");

			MonsterSpawner.instance.AttackPlayer(player);

            PrintAnyKeyInstruction();
        }

		public override Screen? Next()
		{
            Console.ReadKey();

            if (player.Health <= 0) return BattleResultScreen.instance;
            return ActionSelectScreen.instance;
        }
	}
}
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

			//몬스터 정보
			Console.WriteLine("Lv. 2 미니언 의 공격!");
			Console.WriteLine("Chad 을(를) 맞췄습니다. [데미지 : 6]"); //캐릭터 이름, 몬스터 데미지 정보
            Console.WriteLine();

            //캐릭터 정보
            Console.WriteLine("Lv.1 Chad"); //캐릭터 이름,레벨
			Console.WriteLine("HP 100 -> 94"); // 캐릭터 hp, 받은 데미지 계산
			Console.WriteLine();

			PrintNumAndString(0, "다음");

            PrintUserInstruction();
        }

		public override Screen? Next()
		{
            string input = Console.ReadLine();
            switch (input)
            {
                case "0":
                    //만약 플레이어의 체력이 0이라면
                    //return BattleResultScreen.instance;

                    //아니면, 아래 반환
                    return new ActionSelectScreen();
                default:
                    isRetry = true;
                    return this;
            }
        }
	}
}
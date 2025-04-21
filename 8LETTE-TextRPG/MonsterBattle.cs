using System;

namespace _8LETTE_TextRPG
{
	internal class MonsterBattle : Screen
	{
		public static readonly MonsterBattle Instance = new MonsterBattle();

		public override void Show()
		{
			Console.Clear();

			PrintTitle("Battle!!\n");

			Console.WriteLine("Lv. 2 미니언 의 공격!");
			Console.WriteLine("Chad 을(를) 맞췄습니다. [데미지 : 6]\n");
			Console.WriteLine("Lv.1 Chad");
			Console.WriteLine("HP 100 -> 94");

			Console.WriteLine("\n0.다음");
			PrintUserInstruction();

        }
		public override Screen? Next()
		{
            string input = Console.ReadLine();
            switch (input)
            {
                case "0":
					return null;
                default:
                    isRetry = true;
                    return this;
            }
        }

	}
}
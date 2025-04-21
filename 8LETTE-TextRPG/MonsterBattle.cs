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
			//몬스터 정보
			Console.WriteLine("Lv. 2 미니언 의 공격!");
			Console.WriteLine("Chad 을(를) 맞췄습니다. [데미지 : 6]\n"); //캐릭터 이름, 몬스터 데미지 정보
			Console.WriteLine("Lv.1 Chad"); //캐릭터 이름,레벨
			Console.WriteLine("HP 100 -> 94"); // 캐릭터 hp, 받은 데미지 계산

			Console.WriteLine("\n0.다음");
			PrintUserInstruction();

        }
		public override Screen? Next()
		{
            string input = Console.ReadLine();
            switch (input)
            {
                case "0":
					return null;		//다시 플레이어 페이즈로 넘어가야 함
                default:
                    isRetry = true;
                    return this;
            }
        }

	}
}
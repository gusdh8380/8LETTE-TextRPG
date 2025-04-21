using System;

namespace _8LETTE_TextRPG
{
    internal class BattleResult : Screen
    {
        public static readonly BattleResult Instance = new BattleResult();
        private int hp = 1;

        //결과 텍스트 색깔 값
        public void ResultColor(string result)
        {
            if (hp <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(result);
                Console.ResetColor();
            }
        }

        public override void Show()
        {
            Console.Clear();

            PrintTitle("Battle!! - Result");
            //플레이어 체력 정보
            if (hp <= 0)
            {
                ResultColor("\nYou Lose");
                Console.WriteLine("\nLv.1 Chad");       //플레이어 체력/이름 정보
                Console.WriteLine("HP 100 -> 74\n");      //플레이어 전 체력 -> 현 체력

                PrintUserInstruction();

            }
            else
            {
                ResultColor("\nVictory");
                Console.WriteLine("\n던전에서 몬스터 3마리를 잡았습니다.");  //몬스터 몇 마리 잡았는지 정보
                Console.WriteLine("\nLv.1 Chad");               //플레이어 체력/이름 정보
                Console.WriteLine("HP 100 -> 74\n");            //플레이어 전 체력 -> 현 체력

                PrintUserInstruction();
            }
        }

        public override Screen? Next()
        {
            string input = Console.ReadLine();
            switch (input)
            {
                case "0":
                    return TownScreen.instance;
                default:
                    isRetry = true;
                    return this;
            }
        }


    }
}

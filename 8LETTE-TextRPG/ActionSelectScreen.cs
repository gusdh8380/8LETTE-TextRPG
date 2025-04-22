namespace _8LETTE_TextRPG
{
    internal class ActionSelectScreen : Screen
    {
        private void ShowMonsterInfo()
        {
            //몬스터 객체를 불러와서 정보를 출력
            Console.WriteLine("Lv.2 미니언  HP 15");
            Console.WriteLine("Lv.5 대포미니언 HP 25");
            Console.WriteLine("LV.3 공허충 HP 10");

            Console.WriteLine();
        }

        private void ShowPlayerInfo()
        {
            //플레이어의 필요한 정보(레벨, 이름, 직업, 체력)를 출력
            Console.WriteLine("[내 정보]");
            Console.WriteLine("Lv.1  Chad (전사) ");
            Console.WriteLine("HP 100/100 ");

            Console.WriteLine();
        }

        public override void Show()
        {
            Console.Clear();

            PrintTitle("전투!!");

            ShowMonsterInfo();
            
            ShowPlayerInfo();

            PrintNumAndString(1, "공격");

            PrintUserInstruction();
        }

        public override Screen? Next()
        {
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    return new PlayerAttackScreen();
                case "2":
                    return new MonsterAttackScreen(MainGame.player);
                default:
                    isRetry = true;
                    return this;
            }
        }
    }
}

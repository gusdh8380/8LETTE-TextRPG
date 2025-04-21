namespace _8LETTE_TextRPG
{
    internal class PlayerAttackScreen : Screen
    {
        private bool isAttacked = false;

        private void ShowAttackList()
        {
            //몬스터 객체를 불러와서 입력 번호와 정보를 출력
            Console.WriteLine("1 Lv.2 미니언  HP 15");
            Console.WriteLine("2 Lv.5 대포미니언 HP 25");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("3 LV.3 공허충 HP 10");
            Console.ResetColor();

            Console.WriteLine();

            //플레이어의 필요한 정보(레벨, 이름, 직업, 체력)를 출력
            Console.WriteLine("[내 정보]");
            Console.WriteLine("Lv.1  Chad (전사) ");
            Console.WriteLine("HP 100/100 ");
        }

        private void ShowAttackResult()
        {
            //플레이어가 공격한 몬스터와 데미지를 출력
            Console.WriteLine("Chad 의 공격!");
            Console.WriteLine("Lv.3 공허충 을(를) 맞췄습니다. [데미지 : 10]");

            Console.WriteLine();

            //플레이어가 공격한 몬스터의 체력과 상태를 출력
            Console.WriteLine("Lv.3 공허충");
            Console.WriteLine("HP 10 -> Dead");
        }

        public override void Show()
        {
            Console.Clear();

            PrintTitle("전투!!\n");

            if (!isAttacked)
            {
                ShowAttackList();
            }
            else
            {
                ShowAttackResult();
            }
            Console.WriteLine();

            Console.WriteLine("0. {0}\n", !isAttacked ? "취소" : "다음");

            PrintUserInstruction();
        }

        public override Screen? Next()
        {
            string input = Console.ReadLine();
            if(input == "0")
            {
                if (!isAttacked) return new ActionSelectScreen();
                else 
                {
                    isAttacked = false;
                    return null; //적 공격 페이즈 스크린으로 넘어감
                }
            }
            else if (int.TryParse(input, out int num) && 1 <= num && num <= 3/*&& 몬스터 살아있을 때*/)
            {
                //몬스터에게 플레이어의 공격력에 해당하는 데미지를 가함
                isAttacked = true;
            }
            else
            {
                isAttacked = false;
            }

            return this;
        }
    }
}
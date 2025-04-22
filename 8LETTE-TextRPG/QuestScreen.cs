using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG
{
    internal class QuestScreen : Screen
    {
        public static readonly QuestScreen Instance = new QuestScreen();
        private QuestScreen() { }

        private Screen ShowQuestDetail(string title, string description, string progress, string reward)
        {
            while (true) //퀘스트 수락
            {
                Console.Clear();
                PrintTitle($"Quest!!");
                Console.WriteLine(title);
                Console.WriteLine();
                Console.WriteLine(description);
                Console.WriteLine(progress);
                Console.WriteLine();
                Console.WriteLine(reward);
                Console.WriteLine();
                Console.WriteLine("1. 수락");
                Console.Write("2. 거절");
                PrintUserInstruction();
                

                string input = Console.ReadLine();

                if (input == "1")
                {
                    Console.WriteLine($"\n'{title}' 퀘스트를 수락했습니다! 퀘스트 목록으로 돌아갑니다.");
                    Console.WriteLine("아무 키나 누르면 계속...");
                    Console.ReadKey();
                    return QuestScreen.Instance;
                }
                else if (input == "2")
                {
                    Console.WriteLine("\n퀘스트를 거절했습니다. 퀘스트 목록으로 돌아갑니다.");
                    Console.WriteLine("아무 키나 누르면 계속...");
                    Console.ReadKey();
                    return QuestScreen.Instance;
                }
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다.");
                    Console.WriteLine("아무 키나 누르면 다시 선택 화면으로 돌아갑니다...");
                    Console.ReadKey();
                }
            }
        }

        public override void Show() // 퀘스트 목록
        {
            Console.Clear();
            PrintTitle("Quest!!");

            Console.WriteLine("1. 마을을 위협하는 몬스터 처치");
            Console.WriteLine("2. 장비를 장착해 보자");
            Console.WriteLine("3. 더욱 더 강해지기!");
            Console.WriteLine();
            PrintNumAndString(0, "나가기");
            Console.WriteLine("원하시는 퀘스트를 선택해주세요.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(">> ");
            Console.ResetColor();
        }
        public override Screen? Next()
        {
            string input = Console.ReadLine();

            switch (input) //퀘스트 선택
            {
                case "1":
                    return ShowQuestDetail(
                        "마을을 위협하는 몬스터 처치",
                        "이봐! 마을 근처에 몬스터들이 너무 많아졌다고 생각하지 않나?\n" +
                        "마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n" +
                        "모험가인 자네가 좀 처치해주게!\n",
                        "- 몬스터 5마리 처치(0/5)",
                        "-보상-\n쓸만한방패 x 1 \n5G"
                        );
                case "2":
                    return ShowQuestDetail(
                        "장비를 장착해 보자",
                        "장비를 구매하여 장비를 장착해보자\n",
                        "-아이템 장착하기 (0/1)",
                        "-보상-\n 300G"
                        );
                case "3":
                    return ShowQuestDetail(
                        "더욱 더 강해지기!",
                        "던전을 탐험하여 레벨업을 하자\n",
                        "-레벨업 하기(0/1)",
                        "-보상-\n 500G"
                        );
                case "0":
                    return TownScreen.Instance;
                default:
                    isRetry = true;
                    return this;
            }
        }
    }
}

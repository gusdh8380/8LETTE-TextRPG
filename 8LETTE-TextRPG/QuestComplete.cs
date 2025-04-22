using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG
{
    internal class QuestCompleteScreen //퀘스트 완료된 창 출력
    {
        public static Screen Show(string title, string description, string reward , string progress)
        {
            while (true)
            {
                Console.Clear();
                PrintTitle("Quest!!");

                Console.WriteLine(title);
                Console.WriteLine();
                Console.WriteLine(description);
                Console.WriteLine();
                Console.WriteLine(progress);
                Console.WriteLine();
                Console.WriteLine("- 보상-");
                Console.WriteLine(reward);
                Console.WriteLine();
                Console.WriteLine("1. 보상 받기");
                Console.WriteLine("2. 돌아가기");

                Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine();

                if (input == "1")
                {
                    Console.WriteLine($"\n'{title}' 퀘스트의 보상을 받았습니다!");
                    Console.WriteLine("아무 키나 누르면 마을로 돌아갑니다...");
                    Console.ReadKey();
                    return TownScreen.instance;
                }
                else if (input == "2")
                {
                    Console.WriteLine("\n퀘스트 목록으로 돌아갑니다...");
                    Console.ReadKey();
                    return QuestScreen.instance;
                }
                else 
                {
                    Console.WriteLine("\n잘못된 입력입니다. 다시 입력 해주세요.");
                    Console.WriteLine("아무 키나 누르면 다시 선택화면으로 돌아갑니다...");
                    Console.ReadKey();
                }
            }
        }
        private static void PrintTitle(string title)
        {
            Console.WriteLine($"==== {title} ====\n");
            Console.ResetColor();
        }
    }
}


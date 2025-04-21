using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG
{
    internal class StatusScreen : Screen  //상태보기 화면
    {
        public static readonly StatusScreen instance = new StatusScreen();   // 싱글톤 으로 만듦
        private StatusScreen() { }

    public override void Show()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine ("상태 보기");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            // 플레이어 정보
            Console.WriteLine("Lv. 1");
            Console.WriteLine("직업  : 전사");
            Console.WriteLine("공격력 : 10");
            Console.WriteLine("방어력 : 5");
            Console.WriteLine("체력  : 100");
            Console.WriteLine("골드  : 1500 G");

            Console.WriteLine("\n0. 나가기");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(">> ");
            Console.ResetColor();
        }

        public override Screen? Next()
        {
            string input = Console.ReadLine();
            if (input == "0")
            {
                return TownScreen.instance;
            }
            return this;
        }
    }
}

namespace _8LETTE_TextRPG
{
    internal class QuestScreen : Screen
    {
        public static readonly QuestScreen Instance = new QuestScreen();

        //밑에는 test용
        private string[] questTitle =
        {
            "마을을 위협하는 몬스터 처치",

            "장비를 장착해 보자",

            "더욱 더 강해지기!"
        };
        private string[] questDescription =
        {
            "이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\n" +
            "마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n" +
            "모험가인 자네가 좀 처치해주게!",

            "장비를 구매하여 장착해보자.",

            "던전을 계속 클리어하여, 레벨업을 해보자."
        };
        private string[] questTask =
        {
            "미니언 5마리 처치",

            "아이템 장착",

            "레벨업"
        };
        private string[] questReward =
        {
            "  쓸만한 방패 x 1\n" +
            "  500 G",

            "  300 G",

            "  500 G"
        };
        private bool[] questAccepted = {false, false, false};
        private bool[] questCompleted = { false, true, false };
        //위에는 test용

        //퀘스트 상태(디폴트 : -1 / 퀘스트 수락 : 0 / 퀘스트 중 : 1 / 미완료 : 2 / 완료 확인 : 3 / 보상 수령 : 4)
        private int[] flags = { -1, -1, -1 };

        private bool isSelected = false;
        private int userInput = -1;

        private void ShowQuestDetail()
        {
            Console.WriteLine(questTitle[userInput]);
            Console.WriteLine();

            Console.WriteLine(questDescription[userInput]);
            Console.WriteLine();

            Console.WriteLine("- " + questTask[userInput]);
            Console.WriteLine();

            Console.WriteLine("- 보상");
            Console.WriteLine(questReward[userInput]);
            Console.WriteLine();

            switch (flags[userInput])
            {
                case 0:
                    Console.WriteLine("퀘스트를 수락하였습니다!");

                    PrintAnyKeyInstruction();
                    break;

                case 1:
                    PrintNumAndString(1, "보상 받기");
                    PrintNumAndString(2, "돌아가기");

                    PrintUserInstruction();
                    break;

                case 2:
                    Console.WriteLine("아직 퀘스트를 완료하지 못했습니다...");

                    PrintAnyKeyInstruction();
                    break;

                case 3:
                    Console.WriteLine("축하합니다! 퀘스트를 완료하였습니다!");

                    // 퀘스트 보상 지급 로직
                    // Quest.Reward();

                    PrintAnyKeyInstruction();
                    break;

                case 4:
                    Console.WriteLine("이미 보상이 지급된 퀘스트입니다.");

                    PrintAnyKeyInstruction();
                    break;

                default:
                    PrintNumAndString(1, "수락");
                    PrintNumAndString(2, "거절");

                    PrintUserInstruction();
                    break;
            }
        }

        public override void Show()
        {
            Console.Clear();
            PrintTitle("퀘스트");

            if (!isSelected)
            {
                for (int i = 0; i < questTitle.Length; i++)
                {
                    PrintNumAndString(i + 1, questTitle[i]);
                }
                Console.WriteLine();

                PrintNumAndString(0, "나가기");

                PrintUserInstruction();
            }
            else
            {
                ShowQuestDetail();
            }
        }

        public override Screen? Next()
        {
            string input = Console.ReadLine();
            if (!isSelected)
            {
                if(input == "0") return TownScreen.Instance;
                else if (int.TryParse(input, out int num))
                {
                    if (num < 1 || num > questTitle.Length)
                    {
                        isRetry = true;
                        return this;
                    }
                    userInput = num - 1;
                    isSelected = true;
                }
                else
                {
                    isRetry = true;
                }
            }
            else
            {
                //디폴트 : -1 / 퀘스트 수락 : 0 / 퀘스트 중 : 1 / 미완료 : 2 / 완료 확인 : 3 / 보상 수령 : 4
                switch (flags[userInput])
                {
                    case 0: 
                        flags[userInput] = 1;
                        break;

                    case 2: 
                        flags[userInput] = 1;
                        break;

                    case 3:
                        flags[userInput] = 4;
                        isSelected = false;
                        break;

                    default:
                        if (input == "2" || flags[userInput] == 4) isSelected = false;
                        else if (input == "1")
                        {
                            if (flags[userInput] == -1)
                            {
                                questAccepted[userInput] = true;
                                flags[userInput] = 0;
                            }
                            else
                            {
                                if (questCompleted[userInput]) flags[userInput] = 3;
                                else flags[userInput] = 2;
                            }
                        }
                        else
                        {
                            isRetry = true;
                        }
                        break;
                }
            }

            return this;
        }
    }
}

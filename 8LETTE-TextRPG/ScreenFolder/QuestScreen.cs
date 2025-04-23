namespace _8LETTE_TextRPG.ScreenFolder
{
    internal class QuestScreen : Screen
    {
        public static readonly QuestScreen Instance = new QuestScreen();

        private Quest[] _quests;

        public QuestScreen()
        {
            new QuestManager();

            _quests = QuestManager.Instance.GetAllQuests().ToArray();
        }

        private bool _isSelected = false;
        private int _userInput = -1;

        private void ShowQuestDetail()
        {
            Console.WriteLine(_quests[_userInput].Title);
            Console.WriteLine();

            Console.WriteLine(_quests[_userInput].Description);
            Console.WriteLine();

            foreach (QuestGoal goal in _quests[_userInput].Goals)
            {
                Console.WriteLine($"- {goal.CurrentAmount} / {goal.RequiredAmount}");
                Console.WriteLine();
            }

            Console.WriteLine("- 보상");
            Console.WriteLine(_quests[_userInput].RewardItem?.Name);
            Console.WriteLine(_quests[_userInput].RewardGold + "G");
            Console.WriteLine();

            switch (_quests[_userInput].State)
            {
                case QuestState.BeforeAccept:
                    PrintNumAndString(1, "수락");
                    PrintNumAndString(2, "거절");

                    PrintUserInstruction();
                    break;
                case QuestState.InProgress:
                    PrintNumAndString(0, "돌아가기");

                    PrintUserInstruction();
                    break;
                case QuestState.Completed:

                    PrintNumAndString(1, "보상 받기");
                    PrintNumAndString(2, "돌아가기");

                    PrintUserInstruction();
                    break;
                case QuestState.Rewarded:
                    Console.WriteLine("축하합니다! 퀘스트를 완료하였습니다!");
                    Console.WriteLine();

                    PrintNumAndString(0, "돌아가기");

                    PrintUserInstruction();
                    break;
                default:
                    break;
            }
        }

        public override void Show()
        {
            Console.Clear();
            PrintTitle("퀘스트");

            if (_isSelected)
            {
                ShowQuestDetail();
            }
            else
            {
                for (int i = 0; i < _quests.Length; i++)
                {
                    if (_quests[i].State == QuestState.Rewarded)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }

                    PrintNumAndString(i + 1, _quests[i].Title);
                    Console.ResetColor();

                }
                Console.WriteLine();

                PrintNumAndString(0, "나가기");

                PrintUserInstruction();
            }
        }

        public override Screen? Next()
        {
            string? input = Console.ReadLine();
            if (_isSelected)
            {
                switch (input)
                {
                    case "0":
                        if (_quests[_userInput].State == QuestState.InProgress || _quests[_userInput].State == QuestState.Rewarded)
                        {
                            _isSelected = false;
                        }
                        break;
                    case "1":
                        if (_quests[_userInput].State == QuestState.BeforeAccept)
                        {
                            QuestManager.Instance.Accept(_quests[_userInput]);
                        }
                        else if (_quests[_userInput].State == QuestState.Completed)
                        {
                            QuestManager.Instance.ClaimReward(_quests[_userInput]);
                        }
                        break;
                    case "2":
                        _isSelected = false;
                        break;
                    default:
                        _isRetry = true;
                        break;
                }
            }
            else
            {
                if (input == "0")
                {
                    return TownScreen.Instance;
                }
                else if (int.TryParse(input, out int num))
                {
                    Quest[] activeQuests = QuestManager.Instance.GetAllQuests().FindAll(x => x.State != QuestState.Rewarded).ToArray();
                    if (num < 1 || num > activeQuests.Length)
                    {
                        _isRetry = true;
                        return this;
                    }

                    _userInput = num - 1;
                    _isSelected = true;
                }
                else
                {
                    _isRetry = true;
                }
            }

            return this;
        }
    }
}


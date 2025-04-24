namespace _8LETTE_TextRPG.ScreenFolder
{
    internal class PromotionScreen : Screen
    {
        public static readonly PromotionScreen Instance = new PromotionScreen();

        private int remainClear;
        private JobBase? selectedJob = null;
        private bool hasRequest = false;

        private void ShowPromotionList()
        {
            Console.WriteLine("다음과 같은 직업으로 승진 가능합니다!\n");

            PrintNumAndString(1, "버그 워리어");
            PrintNumAndString(2, "메모리 나이트");
            PrintNumAndString(3, "스레드 어쌔신");
            PrintNumAndString(4, "익셉션 헌터");

            PrintUserInstruction();
        }

        private void PrintCongratulatePromotion()
        {
            Player.Instance.Promote(selectedJob);

            Console.WriteLine("축하합니다~!!");
            Console.WriteLine($"{selectedJob.Name}으로 승진하였습니다!!\n");

            PrintAnyKeyInstruction();
        }

        public override void Show()
        {
            Console.Clear();

            PrintTitle("승진");

            Console.WriteLine("던전의 클리어 수에 따라 승진할 수 있습니다.");
            Console.WriteLine();

            if(Player.Instance.Job.PromotionStage == 3)
            {
                Console.WriteLine("당신은 최고의 개발자가 되었습니다!!");

                PrintAnyKeyInstruction();
                return;
            }

            if (!hasRequest)
            {
                remainClear = 5 * (Player.Instance.Job.PromotionStage + 1) - MonsterSpawner.Instance.ClearCount;
                if (remainClear < 0) remainClear = 0;
                Console.WriteLine($"현재 던전 클리어 수는 {MonsterSpawner.Instance.ClearCount}회 입니다.");
                Console.WriteLine($"다음 승진까지 남은 클리어 횟수는 {remainClear}회 입니다.");
                Console.WriteLine();

                if (remainClear == 0) PrintNumAndString(1, "승진 요청");
                PrintNumAndString(0, "돌아가기");

                PrintUserInstruction();
            }
            else
            {
                if(selectedJob == null)
                {
                    ShowPromotionList();
                }
                else
                {
                    PrintCongratulatePromotion();
                }
            }
        }

        public override Screen? Next()
        {
            string input = Console.ReadLine();

            if(Player.Instance.Job.PromotionStage == 3)
            {
                return TownScreen.Instance;
            }

            if (!hasRequest)
            {
                if (input == "0") return TownScreen.Instance;
                else if (input == "1" && remainClear == 0)
                {
                    if(Player.Instance.Job.PromotionStage != 0)
                    {
                        switch (selectedJob)
                        {
                            case BugWarrior_Middle:
                                selectedJob = new BugWarrior_Senior();
                                break;
                            case MemoryKnight_Middle:
                                selectedJob = new MemoryKnight_Senior();
                                break;
                            case ThreadAssassin_Middle:
                                selectedJob = new ThreadAssassin_Senior();
                                break;
                            case ExceptionHunter_Middle:
                                selectedJob = new ExceptionHunter_Senior();
                                break;
                            case BugWarrior_Senior:
                                selectedJob = new BugWarrior_Director();
                                break;
                            case MemoryKnight_Senior:
                                selectedJob = new MemoryKnight_Director();
                                break;
                            case ThreadAssassin_Senior:
                                selectedJob = new ThreadAssassin_Director();
                                break;
                            case ExceptionHunter_Senior:
                                selectedJob = new ExceptionHunter_Director();
                                break;
                        }
                    }
                    hasRequest = true;
                }
                else _isRetry = true;
            }
            else
            {
                if (Player.Instance.Job.PromotionStage == 0)
                {
                    switch (input)
                    {
                        case "1":
                            selectedJob = new BugWarrior_Middle();
                            break;
                        case "2":
                            selectedJob = new MemoryKnight_Middle();
                            break;
                        case "3":
                            selectedJob = new ThreadAssassin_Middle();
                            break;
                        case "4":
                            selectedJob = new ExceptionHunter_Middle();
                            break;
                        default:
                            _isRetry = true;
                            break;
                    }
                }
                else
                {
                    hasRequest = false;
                }
            }

            return this;
        }
    }
}

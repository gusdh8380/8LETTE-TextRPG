using _8LETTE_TextRPG.JobFolder;

namespace _8LETTE_TextRPG.ScreenFolder
{
    internal class PromotionScreen : Screen
    {
        public static readonly PromotionScreen Instance = new PromotionScreen();

        private int _remainClear;
        private JobBase? _selectedJob = null;
        private bool _hasRequest = false;

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
            Console.WriteLine("축하합니다~!!");
            Console.WriteLine($"{_selectedJob?.Name}으로 승진하였습니다!!\n");

            PrintAnyKeyInstruction();
        }

        public override void Show()
        {
            Console.Clear();

            PrintTitle("승진");

            Console.WriteLine("던전의 클리어 수에 따라 승진할 수 있습니다.");
            Console.WriteLine();

            if (Player.Instance.Job.PromotionType == PromotionType.Director)
            {
                Console.WriteLine("당신은 최고의 개발자가 되었습니다!!");

                PrintAnyKeyInstruction();
                return;
            }

            if (!_hasRequest)
            {
                //_remainClear = 0; //디버깅용
                _remainClear = (int)MathF.Max(5 * ((int)Player.Instance.Job.PromotionType + 1) - MonsterSpawner.Instance.ClearCount, 0);

                Console.WriteLine($"현재 던전 클리어 수는 {MonsterSpawner.Instance.ClearCount}회 입니다.");
                Console.WriteLine($"다음 승진까지 남은 클리어 횟수는 {_remainClear}회 입니다.");
                Console.WriteLine();

                if (_remainClear == 0)
                {
                    PrintNumAndString(1, "승진 요청");
                }

                PrintNumAndString(0, "돌아가기");

                PrintUserInstruction();
            }
            else
            {
                if (_selectedJob == null)
                {
                    ShowPromotionList();
                }
                else
                {
                    Player.Instance.Promote(_selectedJob);
                    PrintCongratulatePromotion();
                }
            }
        }

        public override Screen? Next()
        {
            string? input = Console.ReadLine();

            if (Player.Instance.Job.PromotionType == PromotionType.Director)
            {
                return TownScreen.Instance;
            }

            if (!_hasRequest)
            {
                if (input == "0")
                {
                    return TownScreen.Instance;
                }
                else if (input == "1" && _remainClear == 0)
                {
                    if (Player.Instance.Job.PromotionType != PromotionType.Junior)
                    {
                        switch (Player.Instance.Job)
                        {
                            case BugWarrior:
                                _selectedJob = new BugWarrior(Player.Instance.Job.PromotionType + 1);
                                break;
                            case MemoryKnight:
                                _selectedJob = new MemoryKnight(Player.Instance.Job.PromotionType + 1);
                                break;
                            case ThreadAssassin:
                                _selectedJob = new ThreadAssassin(Player.Instance.Job.PromotionType + 1);
                                break;
                            case ExceptionHunter:
                                _selectedJob = new ExceptionHunter(Player.Instance.Job.PromotionType + 1);
                                break;
                        }
                    }

                    _hasRequest = true;
                }
                else
                {
                    _isRetry = true;
                }
            }
            else
            {
                if (Player.Instance.Job.PromotionType == PromotionType.Junior)
                {
                    switch (input)
                    {
                        case "1":
                            _selectedJob = new BugWarrior(PromotionType.Middle);
                            break;
                        case "2":
                            _selectedJob = new MemoryKnight(PromotionType.Middle);
                            break;
                        case "3":
                            _selectedJob = new ThreadAssassin(PromotionType.Middle);
                            break;
                        case "4":
                            _selectedJob = new ExceptionHunter(PromotionType.Middle);
                            break;
                        default:
                            _isRetry = true;
                            break;
                    }
                }
                else
                {
                    _hasRequest = false;
                }
            }

            return this;
        }
    }
}

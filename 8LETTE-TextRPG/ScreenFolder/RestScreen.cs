namespace _8LETTE_TextRPG.ScreenFolder
{
    internal class RestScreen : Screen
    {
        public static readonly RestScreen Instance = new RestScreen();

        private Rest _rest;
        private bool _isRested = false;

        public RestScreen()
        {
            _rest = new Rest(1000f);
        }

        public override void Show()
        {
            Console.Clear();

            PrintTitle("휴식하기");

            bool isAlreadyMax = Player.Instance.Health == Player.Instance.MaxHealth && Player.Instance.Mana == Player.Instance.MaxMana && !Player.Instance.IsDead;
            bool canRest = Player.Instance.Gold >= _rest.Price;
            if (_isRested && !isAlreadyMax && canRest)
            {
                _rest.StartRest();
            }

            Console.WriteLine($"이곳에서 {_rest.Price}G를 지불하고 휴식할 수 있습니다.");
            Console.WriteLine("휴식 시 HP, MP가 모두 회복되며, 전투불능 상태가 해제됩니다.\n");

            Console.WriteLine("[내 정보]");
            Console.WriteLine($"HP {Player.Instance.Health} / {Player.Instance.MaxHealth}");
            Console.WriteLine($"MP {Player.Instance.Mana} / {Player.Instance.MaxMana}");
            Console.WriteLine($"상태 : {(Player.Instance.IsDead ? "치료 필요..." : "전투 가능!!")}");
            Console.WriteLine($"Gold {Player.Instance.Gold}G");

            Console.WriteLine();

            if (_isRested)
            {
                if (isAlreadyMax)
                {
                    Console.WriteLine("이미 완전히 회복된 상태입니다.\n");
                }
                else if (!canRest)
                {
                    Console.WriteLine("골드가 부족합니다.\n");
                }
                else
                {
                    Console.WriteLine("충분히 휴식했습니다!\n");
                }

                PrintNumAndString(0, "나가기");
            }
            else
            {
                PrintNumAndString(1, "휴식하기");
                PrintNumAndString(0, "나가기");
            }

            PrintUserInstruction();
        }

        public override Screen? Next()
        {
            string? input = Console.ReadLine();
            switch (input)
            {
                case "0":
                    _isRested = false;
                    return TownScreen.Instance;
                case "1":
                    if (_isRested)
                    {
                        _isRested = false;
                        _isRetry = true;
                        return this;
                    }

                    _isRested = true;
                    return this;
                default:
                    _isRested = false;
                    _isRetry = true;
                    return this;
            }
        }
    }
}
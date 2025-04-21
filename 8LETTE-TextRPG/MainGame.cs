namespace _8LETTE_TextRPG
{
    internal class MainGame
    {
        static void Main(string[] args)
        {
            Screen? current = TownScreen.instance; // 시작 화면을 TownScreen으로 설정
            while (current != null)
            {
                current.Show();
                current = current.Next();
            }
        }
    }
}

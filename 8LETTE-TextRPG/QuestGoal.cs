namespace _8LETTE_TextRPG
{
    class QuestGoal
    {
        public QuestType Type { get; set; }
        public string Target { get; set; }
        public int RequiredAmount { get; set; }
        public int CurrentAmount { get; set; }
        public bool IsCompleted => CurrentAmount >= RequiredAmount;

        public QuestGoal(QuestType type, string target, int amount)
        {
            Type = type;
            Target = target;
            RequiredAmount = amount;
        }
    }
}

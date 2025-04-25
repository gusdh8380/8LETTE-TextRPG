namespace _8LETTE_TextRPG
{   
    public abstract class JobBase
    {
        public abstract string Name { get; }

        public abstract float BaseAttack { get; }
        public abstract float BaseDefense { get; }
        public abstract float BaseHealth { get; }
        public abstract float BaseMP { get; }

        public abstract int CriticalChance { get; }
        public abstract int EvasionRate { get; }

        public abstract List<Skill> Skills { get; }

        // 승진 단계(미들 -> 시니어 -> 디렉터)
        public abstract int PromotionStage { get; }

        public virtual void IncreaseStats() 
        {
            QuestManager.Instance.SendProgress(QuestType.IncreaseStat);

            Player.Instance.OnContextChanged();
        }
    }
}

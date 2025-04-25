using _8LETTE_TextRPG.SkillFolder;
using Newtonsoft.Json;

namespace _8LETTE_TextRPG.JobFolder
{
    [JsonConverter(typeof(JobConverter))]
    public abstract class JobBase
    {
        public string Name { get; protected set; }

        public float BaseAttack { get; protected set; }
        public float BaseDefense { get; protected set; }
        public float BaseHealth { get; protected set; }
        public float BaseMP { get; protected set; }

        public float CriticalChance { get; protected set; }
        public float EvasionRate { get; protected set; }

        public List<Skill> Skills { get; protected set; }

        public JobType JobType { get; protected set; }

        public PromotionType PromotionType { get; protected set; }

        public JobBase()
        {
            Name = string.Empty;
            Skills = new List<Skill>();
        }

        public virtual void IncreaseStats()
        {
            QuestManager.Instance.SendProgress(QuestType.IncreaseStat);

            Player.Instance.OnContextChanged();
        }

        public string GetPromotionTypeStr()
        {
            return PromotionType switch
            {
                PromotionType.Junior => "주니어",
                PromotionType.Middle => "미들",
                PromotionType.Senior => "시니어",
                PromotionType.Director => "디렉터",
                _ => "알 수 없음"
            };
        }
    }
}

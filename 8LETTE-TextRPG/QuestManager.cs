using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG
{
    class QuestManager
    {
        private List<Quest> _quests = new List<Quest>();
        private Dictionary<string, bool> _clearDict = new Dictionary<string, bool>(); // 퀘스트 아이디, 클리어 여부

        public QuestManager()
        {
            _quests.Add(new Quest("테스트 퀘스트 1", "무한루프 몬스터 3마리 처치.", 3, new Item("아이템", "테스트 퀘스트 1 클리어 보상", 99f, 999f, 0f, 1), 5000f));
        }
    }
}

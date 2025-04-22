using System;

namespace _8LETTE_TextRPG
{
	//킬카운트,전투 이전/이후 체력 저장
	internal static class BattleData
	{
		public static int KillCountInThisBattle = 0;

		public static float PlayerHpBeforeBattle = 0f;
		public static float PlayerHpAfterBattle = 0f;
	}
}
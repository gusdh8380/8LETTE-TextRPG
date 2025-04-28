# 8LETTE-TextRPG

## 🛡️ 프로젝트 소개
- **8LETTE-TextRPG**는 프로그래머들의 세계를 모티브로 한 텍스트 기반 RPG 게임입니다.
- 플레이어는 "에잇!레인저"의 일원이 되어 몬스터를 무찌르고, 장비를 강화하며 성장해나갑니다.
- [8LETTE 팀 노션 바로가기](https://torpid-stamp-e35.notion.site/8LETTE-1e0662ebba1c80958206d8ec12ee2e40?pvs=4)

![image](https://github.com/user-attachments/assets/5814e742-44dc-4258-8bf3-97e7e0a5da26)

## 🖥️ 설치 및 실행 방법
![image](https://github.com/user-attachments/assets/a5cc4159-a1ba-4bf3-b05f-4a02f08378f7)
1. Releases에서 가장 최근 버전을 들어갑니다.
2. 해당 버전의 .zip 파일을 압축 해제 후 .exe 파일을 플레이하면 됩니다!

## 🎮 조작 방법
- 텍스트 입력을 통해 메뉴 탐색 및 전투, 장비 관리, 아이템 사용

## ✨ 주요 기능
- 전직/승급 시스템 (버그 워리어, 메모리 나이트, 스레드 어쌔신, 익셉션 헌터)
- 장비 착용 시스템
- 다양한 스킬 시스템
- 퀘스트 시스템(퀘스트 완료 -> 보상)
- 몬스터와의 전투 및 성장 시스템(던전)
- JSON 기반 데이터 저장 및 불러오기 예정
- 휴식 시스템(Hp/Mp 회복 시스템)
- 상점 구매/판매 시스템

## 🗂️ 파일 구조
```plaintext
8LETTE-TextRPG/
 ├── ContextFolder/
 │    ├── DungeonContext.cs
 │    ├── Item Converter.cs
 │    └── ...
 ├── ItemFolder/
 │    ├── EquipableItem.cs
 │    └── ...
 ├──  JobFolder/
 │    ├── BugWarrior.cs
 │    ├── JobBase.cx
 │    └── ...
 ├── MonsterFolder/
 │    ├── DirectorDungeonMonster/
 │    ├── JuniorDungeonMonster/
 │    ├── MiddleDungeonMonster/
 │    ├── SeniorDungeonMonster/
 │    ├── Monster.cs
 │    └── MonsterType.cs
 ├── QuestFolder
 │    ├── Quest.cs
 │    └── ...
 ├── ScreenFolder/
 │    ├── ActionSelectScreen.cs
 │    └── ...
 ├── SkillFolder/
 │     ├── CounterAttack.cs
 │     └── ...
 ├── DungeonManager.cs
 ├── DungeonType.cs
 ├── Inventory.cs
 ├── Level.cs
 ├── MainGame.cs
 ├── Player.cs
 ├── PlayerStat.cs
 ├── Rest.cs
 └── Shop.cs
```

## 🧙‍♂️ 개발자 메모
> "코딩하는 당신이 주인공이다! 🖥️⚡"

---


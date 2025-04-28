# 8LETTE-TextRPG

## 🛡️ 프로젝트 소개
- **8LETTE-TextRPG**는 프로그래머들의 세계를 모티브로 한 텍스트 기반 RPG 게임입니다.
- 플레이어는 "에잇!레인저"의 일원이 되어 몬스터를 무찌르고, 장비를 강화하며 성장해나갑니다.

![image](https://github.com/user-attachments/assets/ea231a9c-1afc-49d7-a667-f0905d4d83df)


## 🖥️ 설치 및 실행 방법
1. 이 저장소를 클론하거나 다운로드합니다.
2. Visual Studio 또는 Rider 등의 IDE로 프로젝트를 엽니다.
3. 필요한 경우 NuGet 패키지 관리에서 `Newtonsoft.Json` 패키지를 설치합니다.
4. `8LETTE-TextRPG.csproj`를 빌드한 후 실행합니다.

## 🎮 조작 방법
- 텍스트 입력을 통해 메뉴 탐색 및 전투, 장비 관리,아이템 사용

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
 ├── ItemFolder/
 │    ├── EquipableItem.cs
 │    └── ...
 ├── MonsterFolder/
 │    ├── DirectorDungeonMonster/
 │    ├── JuniorDungeonMonster/
 │    └── ...
 ├── ScreenFolder/
 │    ├── ActionSelectScreen.cs
 │    └── ...
 ├── SkillFolder/
 │     ├── CounterAttack.cs
 │     └── ...
 ├── Buff.cs
 ├── Inventory.cs
 ├── Level.cs
 ├── Player.cs
 ├── QuestManager.cs
 └── MainGame.cs
```

## 🧙‍♂️ 개발자 메모
> "코딩하는 당신이 주인공이다! 🖥️⚡"

---


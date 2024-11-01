# MyFirst3DProject

** 구현해보고싶었던 기능 및 코드를 마음껏 작성해보며 연습을 한 프로젝트입니다.

## 프로젝트 기능

### 1. 기본 이동 및 점프
- 플레이어는 3인칭 시점에서 **기본 이동**(앞, 뒤, 좌, 우)과 **점프**가 가능합니다.

### 2. UI 시스템
- **아이템 정보** 및 **상호작용 알림** 등을 표시하는 UI 시스템이 있습니다.
- **버프**를 실시간으로 표시하는 UI 창을 추가했습니다.

### 3. 동적 환경 조사 (레이캐스트)
- 레이캐스트(Raycast)를 통해 **충돌 및 상호작용**을 감지할 수 있습니다.
- 이를 통해 플레이어가 벽에 비비는 현상을 **일부** 수정했습니다.

### 4. 점프대
- 플레이어가 점프대를 밟으면 **높이 점프**할 수 있도록 구현했습니다.
- 상호작용이 가능한 점프대와 밟으면 점프가 되는 점프대 두가지를 구현했습니다.

### 5. 아이템 데이터 및 사용
- **아이템 데이터 시스템**을 통해 다양한 아이템 속성을 설정할 수 있습니다.
- 플레이어가 획득한 아이템을 **인벤토리에 저장**하고 사용할 수 있습니다.

### 6. 3인칭 시점
- **3인칭 시점 카메라**를 간단하게 구현했습니다.
- 레이캐스트를 활용하여 천장이 있는 경우, 카메라의 높이를 낮추도록 설정했습니다.

### 7. 움직이는 플랫폼
- **움직이는 플랫폼**을 추가하여 플레이어가 이동하는 발판을 경험할 수 있습니다.
- 플랫폼은 지정된 경로를 따라 이동하며, 플레이어가 플랫폼 위에 있을 때 함께 이동합니다.
- 스크립트를 활용성 있게 구현하여, 에디터에서 손쉽게 만들 수 있습니다.

### 8. 벽타기 및 매달리기 (사다리로 구현)
- 사다리 시스템을 통해 플레이어가 **벽에 매달리거나 위아래로 이동**할 수 있도록 구현했습니다.
- 사다리 상호작용 시 자동으로 위치가 고정되고, 위/아래로 움직일 수 있습니다.

### 9. 레이저 트랩
- **플레이어를 감지하고 데미지를 주는 레이저 트랩**을 추가했습니다.
- 레이캐스트와 라인렌더러를 활용하였습니다.

### 10. 상호작용 오브젝트 표시
- 월드 공간에 상호작용 텍스트가 표시되도록 수정했습니다.

### 11. 간단한 AI 네비게이션
- **AI 네비게이션을 적용한 NPC**를 추가하여 특정 경로를 따라 이동하거나 장애물을 회피하는 기능을 구현했습니다.
- 상태패턴을 적용해보고싶어서 뒤늦게 만들었습니다. Ai네비게이션시스템과 디자인패턴의 이해도가 낮아 많이 힘들었고, 구현이 많이 미흡합니다.

### 12. 피격효과
- 피격시 일정시간 무적 및 깜박임으로 피격효과 구현

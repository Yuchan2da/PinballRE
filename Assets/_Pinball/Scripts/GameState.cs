using UnityEngine;

namespace Pinball
{
    public enum GameRunState
    {
        Running, // 게임 진행 중
        Over,    // 게임 종료
    }

    // 게임 상태 관리 및 상태 변경 알림 (게임 로직 없음)
    public class GameState : MonoBehaviour
    {
        public GameRunState runState { get; private set; } // 현재 게임 상태
        public Observable<int> score;                       // 점수 관찰 가능 객체

        private void Awake()
        {
            score = new Observable<int>(); // 점수 초기화
        }

        private void Start()
        {
            EndGame(); // 시작 시 게임 종료 상태로 설정
        }

        public void RestartGame(bool isBotMode)
        {
            runState = GameRunState.Running; // 상태 변경: 진행 중
            EventManager.instance.OnGameStart?.Invoke(isBotMode); // 게임 시작 이벤트 호출
        }

        public void EndGame()
        {
            runState = GameRunState.Over; // 상태 변경: 종료
            EventManager.instance.OnGameOver?.Invoke(); // 게임 종료 이벤트 호출
        }
    }
}

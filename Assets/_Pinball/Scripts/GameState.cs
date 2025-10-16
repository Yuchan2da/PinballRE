using UnityEngine;

namespace Pinball
{
    public enum GameRunState
    {
        Running,
        Over,
    }

    public class GameState : MonoBehaviour
    {
        public GameRunState runState { get; private set; }
        public Observable<int> score;
        public Observable<int> maxScore; // ✅ 최대 점수 필드 추가

        private void Awake()
        {
            score = new Observable<int>();
            maxScore = new Observable<int>(); // ✅ 초기화
        }

        private void Start()
        {
            maxScore.value = PlayerPrefs.GetInt("MaxScore", 0); // ✅ 저장된 최대 점수 불러오기
            EndGame();
        }

        public void RestartGame(bool isBotMode)
        {
            runState = GameRunState.Running;
            EventManager.instance.OnGameStart?.Invoke(isBotMode);
        }

        public void EndGame()
        {
            runState = GameRunState.Over;
            EventManager.instance.OnGameOver?.Invoke();
        }

        // ✅ 최대 점수 갱신 메서드
        public void TryUpdateMaxScore()
        {
            if (score.value > maxScore.value)
            {
                maxScore.value = score.value;
                PlayerPrefs.SetInt("MaxScore", maxScore.value); // 저장
            }
        }
    }
}

using UnityEngine;

namespace Pinball
{
    // 공이 드레인에 빠지면 게임 종료
    [RequireComponent(typeof(GameState))]
    public class GameOverManager : MonoBehaviour
    {
        [Tooltip("공이 이 Y 좌표에 도달하면 게임이 종료됩니다.")]
        public float drainPosition = -20.72f; // 드레인 위치(Y 좌표)

        private Transform _ball;       // 공 Transform
        private GameState _gameState;  // 게임 상태 관리

        private void Awake()
        {
            _ball = GameObject.FindWithTag("Ball").transform; // 공 찾기
            _gameState = GetComponent<GameState>();           // GameState 가져오기
        }

        private void Update()
        {
            _EndGameIfNeeded(); // 게임 종료 조건 체크
        }

        private void _EndGameIfNeeded()
        {
            bool isGameRunning = _gameState.runState == GameRunState.Running; // 게임 진행 중?
            bool isOutOfBorder = _ball.position.y < drainPosition;            // 공이 드레인 위치 이하?
            bool shouldEndGame = isGameRunning && isOutOfBorder;              // 종료 조건
            if (shouldEndGame)
                _gameState.EndGame();                                         // 게임 종료 호출
        }
    }
}

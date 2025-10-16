using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pinball
{
    [RequireComponent(typeof(GameState))]
    // 점수 관리: IScoreAdder에서 발생한 점수를 GameState에 반영
    public class ScoreManager : MonoBehaviour
    {
        private GameState _gameState;

        private void Awake()
        {
            _gameState = GetComponent<GameState>(); // GameState 가져오기

            _SubscribeToScoreAdders(); // 점수 발생 객체 구독
            _ResetScoreOnRestart();   // 게임 시작 시 점수 초기화
        }

        private void _SubscribeToScoreAdders()
        {
            // IScoreAdder를 구현한 모든 MonoBehaviour 찾기 (FindObjectsByType 사용)
            IEnumerable<IScoreAdder> scoreAdders =
                Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<IScoreAdder>();

            foreach (var adder in scoreAdders)
                adder.OnScoreAdded += _AddScore; // 점수 이벤트 구독
        }
        

        private void _AddScore(int score)
    {
    _gameState.score.value += score;
    _gameState.TryUpdateMaxScore(); // ✅ 최대 점수 갱신
    }

        private void _ResetScoreOnRestart()
        {
            EventManager.instance.OnGameStart += _ =>
                _gameState.score.value = 0; // 게임 시작 시 점수 초기화
        }
    }
}

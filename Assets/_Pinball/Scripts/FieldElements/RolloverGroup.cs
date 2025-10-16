using System;
using System.Linq;
using UnityEngine;

namespace Pinball
{
    public class RolloverGroup : MonoBehaviour, IScoreAdder
    {
        public Rollover[] rollovers; // 그룹 내 롤오버들
        public int scoreValue = 20000; // 모두 활성화 시 점수

        public Action<int> OnScoreAdded { get; set; }

        private InputProvider _inputProvider;

        private void Awake()
        {
            // 롤오버 배열이 비어있는지 확인
            if (rollovers == null || rollovers.Length == 0)
            {
                Debug.LogError("RolloverGroup: rollovers 배열이 비어있습니다.");
                return;
            }

            // DontSaveInEditor가 설정되지 않은 InputProvider만 필터링
            _inputProvider = FindObjectsByType<InputProvider>(FindObjectsSortMode.None)
                .FirstOrDefault(p => (p.hideFlags & HideFlags.DontSaveInEditor) == 0);

            if (_inputProvider == null)
            {
                Debug.LogWarning("RolloverGroup: InputProvider를 찾을 수 없습니다.");
            }

            _HandleGroupActivation(); // 롤오버 이벤트 연결
        }

        private void Update()
        {
            if (_inputProvider == null) return;

            if (_inputProvider.IsSideDown(InputSide.Left))
                _ShiftLeft(); // 왼쪽 쉬프트

            if (_inputProvider.IsSideDown(InputSide.Right))
                _ShiftRight(); // 오른쪽 쉬프트
        }

        private void _ShiftLeft()
        {
            bool first = rollovers[0].rolloverEnabled;

            for (int i = 0; i < rollovers.Length - 1; ++i)
                rollovers[i].rolloverEnabled = rollovers[i + 1].rolloverEnabled;

            rollovers[rollovers.Length - 1].rolloverEnabled = first;
        }

        private void _ShiftRight()
        {
            _ShiftLeft();
            _ShiftLeft(); // 두 번 실행해서 오른쪽으로 이동
        }

        private void _HandleGroupActivation()
        {
            foreach (var rollover in rollovers)
                rollover.OnTriggered += _HandleChildTrigger;
        }

        private void _HandleChildTrigger()
        {
            bool wholeGroupActive = rollovers.All(r => r.rolloverEnabled);

            if (wholeGroupActive)
            {
                _ResetGroup();
                _AddScore();
            }
        }

        private void _ResetGroup()
        {
            foreach (var rollover in rollovers)
                rollover.rolloverEnabled = false;
        }

        private void _AddScore()
        {
            OnScoreAdded?.Invoke(scoreValue); // null 체크 포함
        }
    }
}

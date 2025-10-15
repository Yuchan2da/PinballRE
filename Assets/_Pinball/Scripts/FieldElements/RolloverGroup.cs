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
            Debug.Assert(rollovers.Length > 0); // 비어있는지 확인
            _inputProvider = MonoBehaviour.FindObjectOfType<InputProvider>(); // 입력 감지 컴포넌트 찾기
            _HandleGroupActivation(); // 롤오버 이벤트 연결
        }

        private void Update()
        {
            if (_inputProvider.IsSideDown(InputSide.Left))
                _ShiftLeft(); // 왼쪽 쉬프트

            if (_inputProvider.IsSideDown(InputSide.Right))
                _ShiftRight(); // 오른쪽 쉬프트
        }

        private void _ShiftLeft()
        {
            bool first = rollovers[0].rolloverEnabled; // 첫 번째 상태 저장

            for (int i = 0; i < rollovers.Length - 1; ++i)
                rollovers[i].rolloverEnabled = rollovers[i + 1].rolloverEnabled; // 한 칸씩 이동

            rollovers[rollovers.Length - 1].rolloverEnabled = first; // 마지막에 첫 상태 넣기
        }

        private void _ShiftRight()
        {
            _ShiftLeft();
            _ShiftLeft(); // 두 번 실행해서 오른쪽으로 이동
        }

        private void _HandleGroupActivation()
        {
            foreach (var rollover in rollovers)
                rollover.OnTriggered += _HandleChildTrigger; // 롤오버 이벤트 등록
        }

        private void _HandleChildTrigger()
        {
            bool wholeGroupActive = rollovers.All(
                rollover => rollover.rolloverEnabled); // 모두 켜졌는지 확인

            if (wholeGroupActive)
            {
                _ResetGroup(); // 그룹 초기화
                _AddScore(); // 점수 추가
            }
        }

        private void _ResetGroup()
        {
            foreach (var rollover in rollovers)
                rollover.rolloverEnabled = false; // 모두 끄기
        }

        private void _AddScore()
        {
            OnScoreAdded(scoreValue); // 점수 이벤트 호출
        }
    }
}

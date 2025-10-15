using System;
using UnityEngine;
using System.Linq;

namespace Pinball
{
    public class DropTargetGroup : MonoBehaviour, IScoreAdder
    {
        public DropTarget[] targets; // 그룹에 포함된 타깃들
        public int scoreValue = 5000; // 모두 맞췄을 때 점수

        public Action<int> OnScoreAdded { get; set; }

        private void Awake()
        {
            Debug.Assert(targets.Length > 0); // 타깃이 비어있지 않은지 확인

            _ResetWhenAllTargetsHit(); // 타깃 히트 감지 설정
            _ResetOnGameStart(); // 게임 시작 시 리셋 설정
        }

        private void _ResetWhenAllTargetsHit()
        {
            foreach (var target in targets)
                target.OnHit += _ResetTargetsIfNeeded; // 각 타깃에 이벤트 등록
        }

        private void _ResetTargetsIfNeeded()
        {
            bool areTargetsHit = targets.All(target => !target.isTargetActive); // 전부 맞았는지 확인
            if (areTargetsHit)
            {
                _ResetTargets(); // 타깃 리셋
                OnScoreAdded(scoreValue); // 점수 추가
            }
        }

        private void _ResetTargets()
        {
            foreach (var target in targets)
                target.isTargetActive = true; // 타깃 다시 활성화
        }

        private void _ResetOnGameStart()
        {
            EventManager.instance.OnGameStart += _ => _ResetTargets(); // 게임 시작 시 리셋
        }
    }
}

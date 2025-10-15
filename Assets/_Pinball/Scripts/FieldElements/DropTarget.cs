using System;
using UnityEngine;

namespace Pinball
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DropTarget : MonoBehaviour
    {
        public float disabledZPosition = 0.7f; // 비활성화 시 z 위치
        public Action OnHit; // 맞았을 때 실행되는 이벤트

        public bool isTargetActive
        {
            get => _isTargetActive;
            set
            {
                _isTargetActive = value;
                _rigidbody.simulated = value; // 물리 작동 여부 변경
                _UpdateZPosition(); // z 위치 업데이트
            }
        }

        private Rigidbody2D _rigidbody;
        private bool _isTargetActive = true;
        private float _enabledZPosition;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>(); // Rigidbody 가져오기
            _enabledZPosition = transform.position.z; // 기본 z 위치 저장
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            isTargetActive = false; // 비활성화
            OnHit?.Invoke(); // 이벤트 호출
        }

        private void _UpdateZPosition()
        {
            float z = isTargetActive ? _enabledZPosition : disabledZPosition; // 상태에 따라 z 설정
            var pos = transform.position;
            pos.z = z;
            transform.position = pos; // 위치 적용
        }
    }
}

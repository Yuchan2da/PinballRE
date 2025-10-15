using System;
using UnityEngine;

namespace Pinball
{
    public class Rollover : MonoBehaviour
    {
        public Color enabledColor; // 활성화 시 색상
        public Action OnTriggered; // 트리거 시 호출 이벤트

        public bool rolloverEnabled
        {
            get => _rolloverEnabled;
            set
            {
                _rolloverEnabled = value;
                _UpdateState(); // 상태 업데이트
            }
        }

        private Material _material;
        private Color _disabledColor;
        private bool _rolloverEnabled;

        private void Awake()
        {
            _material = GetComponent<MeshRenderer>().material; // 머티리얼 가져오기
            _disabledColor = _material.color; // 기본 색상 저장

            EventManager.instance.OnGameStart += _ =>
                rolloverEnabled = false; // 게임 시작 시 비활성화
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            rolloverEnabled = !rolloverEnabled; // 상태 반전

            if (OnTriggered != null)
                OnTriggered(); // 이벤트 호출
        }

        private void _UpdateState()
        {
            _material.color = rolloverEnabled ? enabledColor : _disabledColor; // 색상 변경
        }
    }
}

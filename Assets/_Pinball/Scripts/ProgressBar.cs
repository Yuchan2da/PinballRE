using UnityEngine;

namespace Pinball
{
    // 발사 충전 게이지 등 진행률 표시용 UI
    public class ProgressBar : MonoBehaviour
    {
        public Transform maskBar; // 진행률 표시 마스크

        private float _defaultOffset; // 초기 위치
        private float _defaultScale;  // 초기 크기

        private float _offset
        {
            get => maskBar.localPosition.y;
            set
            {
                var v = maskBar.localPosition;
                v.y = value;           // y 위치 변경
                maskBar.localPosition = v;
            }
        }

        private float _scale
        {
            get => maskBar.localScale.y;
            set
            {
                var v = maskBar.localScale;
                v.y = value;           // y 스케일 변경
                maskBar.localScale = v;
            }
        }

        private void Awake()
        {
            Debug.Assert(maskBar != null); // 마스크 연결 확인
            _defaultOffset = _offset;       // 초기 위치 저장
            _defaultScale = _scale;         // 초기 크기 저장
        }

        // progress: [0; 1] 범위
        public void SetProgress(float progress)
        {
            _scale = _defaultScale * (1 - progress);           // 스케일 조절
            _offset = _defaultOffset + (_defaultScale / 2 * progress); // 위치 조절
        }
    }
}

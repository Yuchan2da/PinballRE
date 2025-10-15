using UnityEngine;

namespace Pinball
{
    // 플레이어 입력 제공: 에디터에서는 키보드, 모바일에서는 터치 스크린 사용
    [RequireComponent(typeof(InputProvider))]
    public class PlayerInput : MonoBehaviour, IInput
    {
        public StatefulButton leftSide;  // 왼쪽 플리퍼 버튼
        public StatefulButton rightSide; // 오른쪽 플리퍼 버튼

        private const KeyCode _launchKey = KeyCode.Space; // 런처 키

        private void Awake()
        {
            Debug.Assert(leftSide != null);   // 왼쪽 버튼 연결 확인
            Debug.Assert(rightSide != null);  // 오른쪽 버튼 연결 확인

            var inputProvider = GetComponent<InputProvider>();
            EventManager.instance.OnGameStart += (bool isBotMode) =>
            {
                if (!isBotMode)
                    inputProvider.input = this; // 플레이어 모드일 때 입력 연결
            };
        }

        public bool IsSidePressed(InputSide side)
        {
            var guiSide = side == InputSide.Left ? leftSide : rightSide;
            var keyboardSide = side == InputSide.Left ? KeyCode.LeftArrow : KeyCode.RightArrow;

            return Input.GetKey(keyboardSide) || guiSide.isPressed; // 키보드 또는 터치 입력 체크
        }

        public bool IsLaunchStarted()
            => Input.GetKeyDown(_launchKey) || leftSide.isPressStarted || rightSide.isPressStarted; // 런처 시작

        public bool IsLaunchEnded()
            => Input.GetKeyUp(_launchKey) || leftSide.isPressEnded || rightSide.isPressEnded;       // 런처 종료
    }
}

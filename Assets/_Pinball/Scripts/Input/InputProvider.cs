using UnityEngine;

namespace Pinball
{
    // 현재 게임 상태에 따라 입력 제공: 플레이어, 봇, 또는 없음
    [RequireComponent(typeof(PlayerInput))]
    public class InputProvider : MonoBehaviour, IInput
    {
        public IInput input; // 실제 입력 객체

        private NullInput _nullInput; // 게임 오버 시 사용
        private bool[] _isSideDown = new bool[(int)InputSide.Count]; // 이번 프레임 사이드 눌림 여부
        private bool[] _sideWasPressed = new bool[(int)InputSide.Count]; // 이전 프레임 눌림 여부

        private void Awake()
        {
            _nullInput = new NullInput(); // 초기화

            EventManager.instance.OnGameOver += () =>
                input = _nullInput; // 게임 오버 시 입력 무효화
        }

        private void Update()
        {
            _UpdateIsSideDown(InputSide.Left);  // 왼쪽 플리퍼 상태 업데이트
            _UpdateIsSideDown(InputSide.Right); // 오른쪽 플리퍼 상태 업데이트
        }

        // IInput 인터페이스 구현
        public bool IsSidePressed(InputSide side) => input.IsSidePressed(side); // 눌림 확인
        public bool IsLaunchStarted() => input.IsLaunchStarted();                 // 런처 시작 확인
        public bool IsLaunchEnded() => input.IsLaunchEnded();                     // 런처 종료 확인

        public bool IsSideDown(InputSide side) => _isSideDown[(int)side]; // 현재 눌림 상태 반환

        private void _UpdateIsSideDown(InputSide side)
        {
            int s = (int)side;

            if (IsSidePressed(side))
            {
                if (!_isSideDown[s] && !_sideWasPressed[s])
                {
                    _isSideDown[s] = true;   // 눌림 등록
                    _sideWasPressed[s] = true; // 이전 눌림 기록
                }
                else
                    _isSideDown[s] = false;
            }
            else
            {
                _isSideDown[s] = false;       // 눌림 해제
                _sideWasPressed[s] = false;   // 이전 기록 초기화
            }
        }
    }
}

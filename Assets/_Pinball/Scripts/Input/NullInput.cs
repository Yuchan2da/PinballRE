namespace Pinball
{
    // 아무 입력도 없는 상태를 나타내는 IInput 구현
    public class NullInput : IInput
    {
        public bool IsSidePressed(InputSide side) => false; // 사이드 눌림 없음
        public bool IsLaunchStarted() => false;             // 런처 시작 없음
        public bool IsLaunchEnded() => false;               // 런처 종료 없음
    }
}

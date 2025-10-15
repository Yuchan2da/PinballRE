using System;

namespace Pinball
{
    [Serializable]
    public enum InputSide
    {
        Left,   // 왼쪽 플리퍼
        Right,  // 오른쪽 플리퍼

        Count,  // 마지막, 총 개수 확인용
    }

    public interface IInput
    {
        bool IsSidePressed(InputSide side); // 특정 사이드 플리퍼 버튼이 눌렸는지 확인
        bool IsLaunchStarted();             // 런처(공 발사)가 시작되었는지 확인
        bool IsLaunchEnded();               // 런처가 끝났는지 확인
    }
}

using System;

namespace Pinball
{
    // 점수를 추가하는 객체는 이 인터페이스를 구현해야 함
    public interface IScoreAdder
    {
        Action<int> OnScoreAdded { get; set; } // 점수 추가 이벤트
    }
}

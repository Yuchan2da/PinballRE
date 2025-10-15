using System;

namespace Pinball
{
    // 값이 변경될 때 구독자에게 알림을 보내는 제네릭 클래스
    public class Observable<T>
    {
        private T _value;

        public T value
        {
            get { return _value; }
            set
            {
                _value = value;   // 값 저장
                _EmitOnChange();  // 변경 이벤트 호출
            }
        }

        public Action<T> OnChange; // 값 변경 이벤트

        private void _EmitOnChange()
        {
            if (OnChange != null)
                OnChange(value); // 값이 변경되면 구독자에게 알림
        }
    }
}

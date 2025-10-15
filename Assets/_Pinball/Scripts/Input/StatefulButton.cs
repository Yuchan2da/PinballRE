using UnityEngine;
using UnityEngine.EventSystems;

namespace Pinball
{
    // GUI 버튼 입력 상태를 키보드처럼 조회할 수 있도록 제공
    public class StatefulButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool isPressStarted { get; private set; } // 이번 프레임에 눌림 시작
        public bool isPressed { get; private set; }      // 눌린 상태 유지
        public bool isPressEnded { get; private set; }   // 이번 프레임에 눌림 종료

        public void OnPointerDown(PointerEventData pointerEventData)
        {
            isPressed = true;       // 눌림 상태
            isPressStarted = true;  // 눌림 시작
        }

        public void OnPointerUp(PointerEventData pointerEventData)
        {
            isPressed = false;      // 눌림 해제
            isPressEnded = true;    // 눌림 종료
        }

        private void Update()
        {
            if (isPressStarted)
                CoroutineUtil.instance.WaitForEndOfFrame(
                    () => isPressStarted = false); // 프레임 끝에 눌림 시작 초기화

            if (isPressEnded)
                CoroutineUtil.instance.WaitForEndOfFrame(
                    () => isPressEnded = false);   // 프레임 끝에 눌림 종료 초기화
        }
    }
}

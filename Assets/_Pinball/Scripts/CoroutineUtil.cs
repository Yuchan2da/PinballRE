using System;
using System.Collections;
using UnityEngine;

namespace Pinball
{
    // EndOfFrame에서 실행할 액션을 편하게 호출할 수 있는 유틸리티
    public class CoroutineUtil : MonoBehaviour
    {
        public static CoroutineUtil instance; // 싱글톤 인스턴스

        private void Awake()
        {
            instance = this; // 인스턴스 등록
        }

        public void WaitForEndOfFrame(Action action)
        {
            StartCoroutine(_WaitForEndOfFrameCoroutine(action)); // 코루틴 시작
        }

        private IEnumerator _WaitForEndOfFrameCoroutine(Action action)
        {
            yield return new WaitForEndOfFrame(); // 프레임 끝까지 대기
            action(); // 액션 실행
        }
    }
}

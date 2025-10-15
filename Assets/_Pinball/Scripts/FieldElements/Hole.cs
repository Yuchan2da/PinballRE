using System;
using System.Collections;
using UnityEngine;

namespace Pinball
{
    public class Hole : MonoBehaviour, IScoreAdder
    {
        public Rigidbody2D otherHole; // 연결된 다른 홀
        public float delay = 1; // 텔레포트 지연 시간
        public float kickVelocity = 100; // 튕겨내는 속도
        public int scoreValue = 5000; // 점수 값

        public Action<int> OnScoreAdded { get; set; }

        private Rigidbody2D _ball;

        private void Awake()
        {
            Debug.Assert(otherHole != null); // 다른 홀 연결 확인
            _ball = GameObject.FindWithTag("Ball").GetComponent<Rigidbody2D>(); // 공 찾기
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            StartCoroutine(_TeleportBall()); // 공 텔레포트
            OnScoreAdded(scoreValue); // 점수 추가
        }

        private IEnumerator _TeleportBall()
        {
            otherHole.simulated = false; // 무한 루프 방지

            _ball.simulated = false; // 물리 비활성화
            _ball.position = otherHole.position; // 위치 이동
            _ball.transform.position = otherHole.position;

            yield return new WaitForSeconds(delay); // 딜레이

            _ball.simulated = true; // 다시 활성화
            _ball.linearVelocity = otherHole.transform.up * kickVelocity; // 튕겨내기

            yield return new WaitForSeconds(1);
            otherHole.simulated = true; // 다시 트리거 활성화
        }
    }
}

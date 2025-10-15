using System;
using UnityEngine;

namespace Pinball
{
    public class Bumper : MonoBehaviour, IScoreAdder
    {
        public float strength = 40; // 튕겨내는 힘
        public int scoreValue = 100; // 점수 값

        public Action<int> OnScoreAdded { get; set; }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _KickBallBack(collision); // 공 튕기기
            OnScoreAdded(scoreValue); // 점수 추가
        }

        private void _KickBallBack(Collision2D collision)
        {
            Vector2 normal = _GetAverageNormal(collision); // 평균 법선 구하기
            Vector2 impulse = -normal * strength; // 반대 방향으로 힘 적용
            collision.rigidbody.AddForce(impulse, ForceMode2D.Impulse); // 공에 순간적인 힘 가하기
        }

        private static Vector2 _GetAverageNormal(Collision2D collision)
        {
            var normal = new Vector2(); // 법선 누적용 변수

            for (int i = 0; i < collision.contactCount; ++i)
                normal += collision.GetContact(i).normal; // 각 접점의 법선 더하기

            normal /= collision.contactCount; // 평균 계산
            return normal; // 결과 반환
        }
    }
}

using UnityEngine;

namespace Pinball
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Flipper : MonoBehaviour
    {
        public InputProvider input; // 입력 Provider
        public InputSide inputSide; // 좌/우 플리퍼 구분

        [Tooltip("Rotation in activated state")]
        public float activeRotation;   // 활성 상태 회전 각도
        [Tooltip("Rotation speed (degrees per second)")]
        public float rotationSpeed = 30; // 회전 속도(도/초)

        private float _inactiveRotation; // 비활성 상태 각도
        private Rigidbody2D _rigidBody;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>(); // Rigidbody2D 가져오기
            _inactiveRotation = _rigidBody.rotation;   // 초기 회전 저장
        }

        private void FixedUpdate()
        {
            float deltaAngleAbs = rotationSpeed * Time.fixedDeltaTime; // 이번 프레임 최대 회전량

            bool isActive = input.IsSidePressed(inputSide); // 입력 확인
            float targetAngle = isActive ? activeRotation : _inactiveRotation; // 목표 각도
            float currentAngle = _rigidBody.rotation;
            float angleDiff = targetAngle - currentAngle;
            float sign = Mathf.Sign(angleDiff);
            float angleDiffAbs = Mathf.Abs(angleDiff);
            float deltaAngle = sign * Mathf.Min(angleDiffAbs, deltaAngleAbs); // 실제 회전량

            _rigidBody.MoveRotation(currentAngle + deltaAngle); // 회전 적용
        }
    }
}

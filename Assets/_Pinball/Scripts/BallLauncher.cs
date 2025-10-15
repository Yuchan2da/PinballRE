using System.Collections;
using UnityEngine;

namespace Pinball
{
    [RequireComponent(typeof(InputProvider))]
    public class BallLauncher : MonoBehaviour
    {
        public ProgressBar progressBar; // 충전 게이지 표시
        public float maxSpeed = 100;    // 최대 발사 속도
        public float maxChargeTime = 1; // 최대 충전 시간

        public float currentLaunchSpeed { get; private set; } // 현재 발사 속도

        private Rigidbody2D _ball;
        private InputProvider _input;
        private const float _ballRadius = 1; // 공 위치 판단 반경
        private Vector2 _initialPosition;    // 공 초기 위치
        private bool _launchKeyReleased = false; // 발사키 뗐는지 여부

        private void Awake()
        {
            Debug.Assert(progressBar != null); // ProgressBar 연결 확인

            _ball = GameObject.FindWithTag("Ball").GetComponent<Rigidbody2D>(); // 공 Rigidbody
            _input = GetComponent<InputProvider>(); // 입력 Provider

            _initialPosition = _ball.position; // 초기 위치 저장

            EventManager.instance.OnGameStart += _ => _RespawnBall(); // 게임 시작 시 공 초기화
        }

        private void Update()
        {
            if (_input.IsLaunchStarted() && IsBallAtStart())
                _BeginBallLaunch(); // 발사 시작

            // Input.GetKeyUp() 대신, 여기서 발사키 릴리즈 여부 기록
            _launchKeyReleased = _input.IsLaunchEnded();
        }

        public bool IsBallAtStart() =>
            Vector2.Distance(_initialPosition, _ball.position) < _ballRadius; // 공이 출발점에 있는지

        private void _BeginBallLaunch()
        {
            StartCoroutine(_BeginBallLaunchCoroutine()); // 발사 코루틴 시작
        }

        private IEnumerator _BeginBallLaunchCoroutine()
        {
            float startTime = Time.time;
            float progress = 0;

            for (bool shouldStop = false; !shouldStop;)
            {
                float timePassed = Time.time - startTime;
                progress = Mathf.Clamp01(timePassed / maxChargeTime); // 충전 진행률 계산

                shouldStop = progress == 1 || _launchKeyReleased; // 충전 완료 또는 키 릴리즈

                progressBar.SetProgress(progress); // ProgressBar 업데이트
                currentLaunchSpeed = progress * maxSpeed; // 현재 발사 속도 계산

                yield return new WaitForEndOfFrame();
            }

            _LaunchBall(currentLaunchSpeed); // 공 발사
            progressBar.SetProgress(0);      // ProgressBar 초기화
        }

        private void _LaunchBall(float speed)
        {
            _ball.linearVelocity = new Vector2(0, speed); // 위 방향으로 발사
        }

        private void _RespawnBall()
        {
            _ball.linearVelocity = Vector2.zero;  // 속도 초기화
            _ball.position = _initialPosition;    // 위치 초기화
            _ball.transform.position = _initialPosition; // Transform 위치도 갱신
        }
    }
}

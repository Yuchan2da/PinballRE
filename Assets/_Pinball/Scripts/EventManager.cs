using System;

namespace Pinball
{
    // 게임 이벤트를 전역에서 관리하는 싱글톤
    public class EventManager
    {
        public static EventManager instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EventManager(); // 인스턴스 생성
                return _instance;
            }
        }

        private static EventManager _instance;

        public Action<bool> OnGameStart; // 게임 시작 이벤트 (인자: isBotMode)
        public Action OnGameOver;        // 게임 종료 이벤트
    }
}

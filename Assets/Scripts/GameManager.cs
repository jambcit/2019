using UnityEngine;

namespace Home.Core
{
    public static class GameManager
    {
        public static NetworkManager NetworkManager { get; private set; }
        public static PlayerController LocalPlayer { get; set; }
        public static GameMode GameMode = GameMode.Sandbox;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            NetworkManager = new NetworkManager();
        }
    }
}

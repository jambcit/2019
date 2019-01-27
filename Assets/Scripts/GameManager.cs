namespace Home.Core
{
    using UnityEngine;

    public static class GameManager
    {
        public static NetworkManager NetworkManager { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            NetworkManager = new NetworkManager();
        }
    }
}

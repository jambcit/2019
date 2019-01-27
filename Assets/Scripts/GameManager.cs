using Home.Sandbox;
using Home.UI;
using UnityEngine;

namespace Home.Core
{
    public static class GameManager
    {
        public static NetworkManager NetworkManager { get; private set; }
        public static PlayerController LocalPlayer { get; set; }
        public static GameMode GameMode { get; set; } = GameMode.Sandbox;
        public static GameModePopup GameModePopup { get; set; }
        public static HUD Hud { get; set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            NetworkManager = new NetworkManager();
        }

        public static void UpdateGameMode(GameMode gameMode)
        {
            GameMode = gameMode;
            SandboxObjectInteraction[] interactionObjects = Object.FindObjectsOfType<SandboxObjectInteraction>();
            bool isSandbox = GameMode == GameMode.Sandbox;
            foreach (SandboxObjectInteraction interactionObject in interactionObjects)
            {
                interactionObject.gameObject.SetActive(isSandbox);
            }

            if (!isSandbox)
            {
                GameModePopup?.Display(GameMode);
            }
        }
    }
}

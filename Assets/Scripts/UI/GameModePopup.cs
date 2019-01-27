using Home.Core;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Home.UI
{
    public class GameModePopup : MonoBehaviour
    {
        private const int COUNTDOWN_TIMER = 5;

        [SerializeField] private Text gameModeText;
        [SerializeField] private Text countdownText;

        private void Start()
        {
            gameObject.SetActive(false);
            GameManager.GameModePopup = this;
        }

        public void Display(GameMode gameMode)
        {
            countdownText.text = "";
            gameModeText.text = "Changing Game Mode to ";
            switch (gameMode)
            {
                case GameMode.Fps:
                    gameModeText.text += "Dart Wars";
                    break;

                case GameMode.Hns:
                    gameModeText.text += "Hide and Seek";
                    break;
            }

            gameObject.SetActive(true);
            StartCoroutine(CountdownCoroutine());
        }

        private IEnumerator CountdownCoroutine(int countdownTimer = COUNTDOWN_TIMER)
        {
            countdownText.text = countdownTimer + " Seconds";
            while (countdownTimer > 0)
            {
                yield return new WaitForSeconds(1f);
                countdownTimer--;
                countdownText.text = countdownTimer + " Seconds";
            }

            gameObject.SetActive(false);

            GameManager.LocalPlayer.SetGameModePawn(GameManager.GameMode);
        }
    }
}

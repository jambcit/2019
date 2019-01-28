using System;
using UnityEngine;
using UnityEngine.UI;

namespace Home.UI
{
    public class GameTimer : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;
        [SerializeField] private GameObject timer;
        private Text timerText;
        private float seconds;
        private Action callback;
        public void Initialize(int seconds, Action callback = null)
        {
            gameObject.SetActive(true);
            this.callback = callback;
            this.canvas.GetComponent<Canvas>().worldCamera = Camera.main;
            this.canvas.GetComponent<Canvas>().planeDistance = 0.31f;
            this.timerText = this.timer.GetComponent<Text>();
            this.seconds = seconds;
            this.timerText.text = FormatTimeString();
        }

        private void Update()
        {
            MinusSeconds(Time.deltaTime);
        }

        public void MinusSeconds(float seconds)
        {
            this.seconds -= seconds;
            this.seconds = Mathf.Max(this.seconds, 0);
            if (this.seconds == 0 && callback != null)
            {
                callback.Invoke();
                gameObject.SetActive(false);
            }
            this.timerText.text = FormatTimeString();
        }

        private string FormatTimeString()
        {
            int secs = (int)(seconds % 60);
            return ((int)seconds / 60) + ":" + secs;
        }
    }
}

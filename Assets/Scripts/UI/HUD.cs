using Home.Core;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Home.UI
{
    public class HUD : MonoBehaviour
    {
        private const int YOFFSET = -50;

        [SerializeField] private GameObject playerScorePrefab;
        [SerializeField] private Transform scoreHolder;

        private Dictionary<string, GameObject> scores = new Dictionary<string, GameObject>();
        private int currOffset = 0;

        public void Awake()
        {
            GameManager.Hud = this;
        }

        public void Start()
        {
            
            //AddPlayer(PhotonNetwork.LocalPlayer.UserId, PhotonNetwork.LocalPlayer.NickName);
        }

        public void AddPlayer(string playerId, string playerName, int score)
        {
            if (playerId != null)
            {
                GameObject scoreHolder = Instantiate(playerScorePrefab, this.scoreHolder);
                scoreHolder.transform.localPosition = new Vector3(0, currOffset, 0);
                currOffset += YOFFSET;
                Text name = scoreHolder.transform.Find("Name").GetComponent<Text>();
                name.text = playerName;
                Text scoreText = scoreHolder.transform.Find("Score").GetComponent<Text>();
                scoreText.text = score.ToString();
                scores.Add(playerId, scoreHolder); 
            }
        }

        public void UpdateScore(string playerId, int score)
        {
            GameObject scoreHolder = scores[playerId];
            Text scoreText = scoreHolder.transform.Find("Score").GetComponent<Text>();
            scoreText.text = score.ToString();
        }

        public void RemovePlayer(string playerId)
        {
            GameObject score = scores[playerId];
            Destroy(score);
            scores.Remove(playerId);
        }
    } 
}

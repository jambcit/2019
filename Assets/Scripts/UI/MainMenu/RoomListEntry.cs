using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace Home.UI
{
    public class RoomListEntry : MonoBehaviour
    {
        [SerializeField] private Text roomNameText;
        [SerializeField] private Text roomPlayersText;
        [SerializeField] private Button joinRoomButton;

        private void Start()
        {
            joinRoomButton.onClick.AddListener(() =>
            {
                PhotonNetwork.JoinRoom(roomNameText.text);
            });
        }

        public void Initialize(string name, int currentPlayers, int maxPlayers)
        {
            roomNameText.text = name;
            roomPlayersText.text = currentPlayers + " / " + maxPlayers;
        }
    }
}
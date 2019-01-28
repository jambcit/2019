namespace Home.UI
{
    using Home.Core;
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    using Photon.Pun;
    using Photon.Realtime;

    public class MainMenuUI : MonoBehaviour
    {
        public static Action RoomCreated;

        [SerializeField] private InputField charNameInput;

        public void CreateRoomBtnClicked()
        {
            if (ValidPlayerName())
            {
                string roomName = charNameInput.text + "'s Room";
                RoomOptions roomOptions = new RoomOptions { MaxPlayers = 4, PublishUserId = true };
                PhotonNetwork.CreateRoom(roomName, roomOptions);
            }
        }

        public void JoinRoomBtnClicked(GameObject obj)
        {
            if (ValidPlayerName())
            {
                TogglePanel(obj);
            }
        }

        public bool ValidPlayerName()
        {
            if (!string.IsNullOrEmpty(charNameInput.text))
            {
                PhotonNetwork.LocalPlayer.NickName = charNameInput.text;
                return true;
            }
            return false;
        }

        public void TogglePanel(GameObject obj)
        {
            obj.SetActive(!obj.activeSelf);
        }

        public void ActivateElement(GameObject obj)
        {
            obj.SetActive(true);
        }

        public void DisableElement(GameObject obj)
        {
            obj.SetActive(false);
        }

        public void QuitApplication()
        {
            Application.Quit();
        }
    }
}

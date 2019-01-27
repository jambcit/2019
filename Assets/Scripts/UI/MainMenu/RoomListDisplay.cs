namespace Home.UI
{
    using Home.Core;
    using System.Collections.Generic;
    using UnityEngine;
    using Photon.Realtime;

    public class RoomListDisplay : MonoBehaviour
    {
        #region Member Variables
        [SerializeField] private GameObject roomListContent;
        [SerializeField] private GameObject roomListEntryPrefab;

        private Dictionary<string, GameObject> roomListEntries = new Dictionary<string, GameObject>();
        #endregion // Member Variables

        #region MonoBehaviour Callbacks
        private void OnEnable()
        {
            UpdateRoomListPanel();
            NetworkManager.RoomListUpdated += UpdateRoomListPanel;
        }

        private void OnDisable()
        {
            NetworkManager.RoomListUpdated -= UpdateRoomListPanel;
        }
        #endregion // MonoBehvaiour Callbacks

        #region Public Functions
        /// <summary>
        /// Clears room list display
        /// </summary>
        public void ClearRoomListView()
        {
            foreach (GameObject entry in roomListEntries.Values)
            {
                Destroy(entry);
            }

            roomListEntries.Clear();
        }

        /// <summary>
        /// Updates room list display
        /// </summary>
        public void UpdateRoomListView()
        {
            foreach (RoomInfo info in GameManager.NetworkManager.RoomList)
            {
                GameObject entry = Instantiate(roomListEntryPrefab, roomListContent.transform);
                entry.transform.localScale = Vector3.one;
                entry.GetComponent<RoomListEntry>().Initialize(info.Name, info.PlayerCount, info.MaxPlayers);

                roomListEntries.Add(info.Name, entry);
            }
        }
        #endregion // Public Functions

        #region Private Functions
        private void UpdateRoomListPanel()
        {
            ClearRoomListView();
            UpdateRoomListView();
        }
        #endregion // private Functions
    }
}
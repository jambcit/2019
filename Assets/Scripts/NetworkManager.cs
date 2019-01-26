namespace Home.Core
{
    using System;
    using UnityEngine;
    using Photon.Pun;
    using Photon.Realtime;
    using System.Collections.Generic;

    public class NetworkManager : IConnectionCallbacks, IMatchmakingCallbacks, ILobbyCallbacks
    {
        public static Action RoomListUpdated;

        private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();

        public Dictionary<string, RoomInfo>.ValueCollection RoomList { get { return cachedRoomList.Values; } }

        public NetworkManager()
        {
            PhotonNetwork.AddCallbackTarget(this);
            PhotonNetwork.ConnectUsingSettings();
        }

        public void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
        }
            
        public void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel(1);
            // Instatiate all pawns whe joining (drone, nerf gun, kid)
            PhotonNetwork.Instantiate("Cube", Vector3.zero, Quaternion.identity);
        }

        public void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            UpdateCachedRoomList(roomList);
            RoomListUpdated?.Invoke();
        }

        private void UpdateCachedRoomList(List<RoomInfo> roomList)
        {
            // Remove room from cached list if it is closed, invisible or marked as removed
            foreach (RoomInfo info in roomList)
            {
                if (!info.IsOpen || !info.IsVisible || info.RemovedFromList)
                {
                    if (cachedRoomList.ContainsKey(info.Name))
                    {
                        cachedRoomList.Remove(info.Name);
                    }
                    continue;
                }

                // Update cached room info
                if (cachedRoomList.ContainsKey(info.Name))
                {
                    cachedRoomList[info.Name] = info;
                }
                else
                {
                    // Add new room info to cache
                    cachedRoomList.Add(info.Name, info);
                }
            }
        }

        public void OnCreatedRoom() {}
        public void OnCreateRoomFailed(short returnCode, string message) {}
        public void OnFriendListUpdate(List<FriendInfo> friendList) {}
        public void OnJoinRandomFailed(short returnCode, string message) {}
        public void OnJoinRoomFailed(short returnCode, string message) {}
        public void OnLeftRoom() {}
        public void OnConnected() {}
        public void OnDisconnected(DisconnectCause cause) {}
        public void OnRegionListReceived(RegionHandler regionHandler) {}
        public void OnCustomAuthenticationResponse(Dictionary<string, object> data) {}
        public void OnCustomAuthenticationFailed(string debugMessage) {}
        public void OnJoinedLobby() {}
        public void OnLeftLobby() {}
        public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics) {}
    }
}

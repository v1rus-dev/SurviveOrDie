using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Server
{
    public class ConnectToServer : MonoBehaviourPunCallbacks
    {
        private bool isConnected = false;
        void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        private void Update()
        {
            if (isConnected)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    CreateRoom();
                }

                if (Input.GetKeyDown(KeyCode.J))
                {
                    JoinRoom();
                }
            }
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to master");
            PhotonNetwork.JoinLobby();
        }

        private void CreateRoom()
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4;
            PhotonNetwork.CreateRoom("Room", roomOptions);
        }

        private void JoinRoom()
        {
            PhotonNetwork.JoinRoom("Room");
        }

        public override void OnJoinedLobby()
        {
            Debug.Log("Joined Lobby");
            isConnected = true;
        }


        public override void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel("Sandbox");
        }
    }
}

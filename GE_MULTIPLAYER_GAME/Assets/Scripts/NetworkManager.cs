using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
public class NetworkManager : MonoBehaviourPunCallbacks
{

    
    private List<RoomInfo> roomsList; 

    public GUIStyle style;
    private const string roomName = "huone";

    public string playerNameInput;


    
    void Start()
    {
        style.fontSize = 28;
        style.normal.textColor = Color.blue;
        roomsList = new();

        playerNameInput = "enter name here";

        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.IsConnectedAndReady.ToString(), style);
        GUILayout.Label(PhotonNetwork.InLobby.ToString(), style);


        if(PhotonNetwork.CurrentRoom != null)
        {
            GUILayout.Label(PhotonNetwork.CurrentRoom.ToString(), style);
        }

        // when not in room but in lobby allow plr to create room
        // also list all rooms
        if(PhotonNetwork.InRoom == false && PhotonNetwork.InLobby == true)
        {
            
            playerNameInput = GUI.TextField(new Rect(10,70,200,20), playerNameInput, 25);



            // if create room btn is pressed
            if(GUI.Button(new Rect(100,100,250,100), "Create Room"))
            {
                PhotonNetwork.CreateRoom(roomName + System.Guid.NewGuid().ToString("N"));

            }

            // if there are rooms in the list, create buttons for all of them
            if(roomsList.Count > 0)
            {
                int i = 0;
                foreach(RoomInfo room in roomsList)
                {
                    if(GUI.Button(new Rect(100,250 + 100 * i,250,100), "Join " + room.Name))
                    {
                        PhotonNetwork.JoinRoom(room.Name);
                    }
                    i++;
                }
            }
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        roomsList = roomList;
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        // create a player character when room has been joined
        // instantiated via photonnetwork and has to contain photonview and in resources folder             
        string[] plrdata = new string[1];
        plrdata[0] = playerNameInput;
        PhotonNetwork.Instantiate("Player", new(Random.Range(-10,10),2f,0), Quaternion.identity, default, plrdata);
    }
}

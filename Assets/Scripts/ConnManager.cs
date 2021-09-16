using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnManager : MonoBehaviourPunCallbacks
{
    public string RoomName;

    public List<GameObject> players = new List<GameObject>();
    void Start()
    {

    }
    public void OnStart()
    {
        Debug.Log("Start");
        PhotonNetwork.GameVersion = "0.1";

        //게임에서 사용할 사용자의 이름을 랜덤으로
        int num = Random.Range(0, 1000);
        PhotonNetwork.NickName = "Player " + num;

        //게임에 참여하면 마스터 클라이언트가 구성한 씬에 자동으로 동기화하도록 한다.
        PhotonNetwork.AutomaticallySyncScene = true;

        //서버 접속
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnected()
    {
        Debug.Log("OnConnected");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected" + cause);
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
    }
    public void JoinLobby()
    {
        if (PhotonNetwork.IsConnected == false)
        {
            Debug.Log("서버연결 안되어있음 JoinLobby Fail");

            return;
        }

        Debug.Log("JoinLobby12");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("로비 접속 완료!");
        RoomOptions ro = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 8
        };
        PhotonNetwork.JoinOrCreateRoom(this.RoomName, ro, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("룸 입장!");
    }
    public void PlayerEnter()
    {
        OnPlayerEnteredRoom(null);
    }
    public void PlayerExit()
    {
        OnPlayerLeftRoom(null);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom");
        Vector2 originPos = Random.insideUnitCircle * 2.0f;
        GameObject player = PhotonNetwork.Instantiate("Player", new Vector3(originPos.x, 0, originPos.y), Quaternion.identity);
        player.name = "nick";
        players.Add(player);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        // 1.  운동장으로 들어간다
        // 2.  1번째 nick으로 추정되는 player 한테 먼저 가본다
        // 3.  너의 '이름'은 뭐야?? 
        // 4.  대답과 nick을 비교
        // 5.  찾아서 같으면 -> 찾았다 -> 운동장에서 나간다 break
        // 5-1 틀리면
        // 6.  이번엔 2번째 nick으로 추정되는 player 한테 먼저 가본다
        // 7.  ...
        // 8.  마지막 번째 nick으로 추정되는 player 한테 먼저 가본다
        // 9.  너의 이름...
        // 10. 운동장으로 나간다(=틀리면 for문을 종료)
        int index = 0;
        GameObject hand = null;
        for (; index < players.Count; index = index + 1)
        {
            // 2.  1번째 nick으로 추정되는 player 한테 먼저 가본다
            // 3.  너의 '이름'은 뭐야?? 
            // 4.  대답과 nick을 비교
            // 5.  찾아서 같으면 -> 찾았다 -> 운동장에서 나간다 break
            // 5-1 틀리면
            GameObject first = players[index];
            string name = first.name;
            bool result = name == "nick";
            if (result)
            {
                // 5.  찾아서 같으면 -> 찾았다 -> 운동장에서 나간다 break
                hand = first;
                break;
            }
            // 5-1 틀리면
        }
        if (hand == null)
        {
            return;
        }
        Debug.Log("OnPlayerLeftRoom");
        players.Remove(hand);
        PhotonNetwork.Destroy(hand);
    }
}
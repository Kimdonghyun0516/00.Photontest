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
        int num = Random.Range(0,1000);
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
        if(PhotonNetwork.IsConnected == false)
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
        RoomOptions ro = new RoomOptions() {
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
        players.Add(PhotonNetwork.Instantiate("Player", new Vector3(originPos.x, 0, originPos.y), Quaternion.identity));
        
    }
    
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("OnPlayerLeftRoom");
        GameObject last = players[players.Count - 1];
        players.Remove(last);
        PhotonNetwork.Destroy(last);
    }
}

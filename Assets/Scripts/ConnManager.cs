using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnManager : MonoBehaviourPunCallbacks
{
    void Start()
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
        PhotonNetwork.JoinOrCreateRoom("NetTest", ro, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("룸 입장!");

        //반경 2m 이내에 Player프리팹을 생성한다.
        Vector2 originPos = Random.insideUnitCircle * 2.0f;
        PhotonNetwork.Instantiate("Player", new Vector3(originPos.x, 0, originPos.y), Quaternion.identity);
    }

}

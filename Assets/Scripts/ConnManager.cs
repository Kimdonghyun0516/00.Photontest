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

        //���ӿ��� ����� ������� �̸��� ��������
        int num = Random.Range(0,1000);
        PhotonNetwork.NickName = "Player " + num;

        //���ӿ� �����ϸ� ������ Ŭ���̾�Ʈ�� ������ ���� �ڵ����� ����ȭ�ϵ��� �Ѵ�.
        PhotonNetwork.AutomaticallySyncScene = true;

        //���� ����
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
            Debug.Log("�������� �ȵǾ����� JoinLobby Fail");

            return;
        }
        
        Debug.Log("JoinLobby12");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("�κ� ���� �Ϸ�!");
        RoomOptions ro = new RoomOptions() {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 8
        };
        PhotonNetwork.JoinOrCreateRoom(this.RoomName, ro, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("�� ����!");
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

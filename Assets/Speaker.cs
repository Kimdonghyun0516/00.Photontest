using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.Unity;
using Photon.Realtime;

public class Speaker : MonoBehaviour, IConnectionCallbacks, IMatchmakingCallbacks
{
    public bool autoConnect = true;

    [SerializeField]
    private bool publishUserId = false;

    private VoiceConnection voiceConnection;

    private readonly EnterRoomParams enterRoomParams = new EnterRoomParams
    {
        RoomOptions = new RoomOptions()
    };
    public string RoomName;
    void Start()
    {
        Debug.Log("Start");
    }
    private void Awake()
    {
        this.voiceConnection = this.GetComponent<VoiceConnection>();
        Debug.Log("Awake");
    }
    private void OnEnable()
    {
        this.voiceConnection.Client.AddCallbackTarget(this);
        Debug.Log("OnEnable");
        this.voiceConnection.ConnectUsingSettings();
        
    }
    public void OnConnected()
    {

    }
    public void OnConnectedToMaster()
    {
        this.enterRoomParams.RoomOptions.PublishUserId = this.publishUserId;
        this.enterRoomParams.RoomName = this.RoomName;
        this.voiceConnection.Client.OpJoinOrCreateRoom(this.enterRoomParams);
        Debug.Log("Connected and JoinRoom");
    }
    public void OnDisconnected(DisconnectCause cause)
    {

    }
    public void OnRegionListReceived(RegionHandler regionHandler)
    {

    }

    public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {

    }

    public void OnCustomAuthenticationFailed(string debugMessage)
    {

    }


    public void OnCreatedRoom()
    {
        Debug.Log("OnCreateRoom");
    }

    public void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogErrorFormat("OnCreateRoomFailed errorCode={0} errorMessage={1}", returnCode, message);
    }

    public void OnFriendListUpdate(List<FriendInfo> friendList)
    {
        Debug.Log("OnFriendListUpdate");
    }

    public void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        if (this.voiceConnection.PrimaryRecorder == null)
        {
            this.voiceConnection.PrimaryRecorder = this.gameObject.AddComponent<Recorder>();
        }
        this.voiceConnection.PrimaryRecorder.TransmitEnabled = true;
    }

    public void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogErrorFormat("OnJoinRandomFailed errorCode={0} errorMessage={1}", returnCode, message);
    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogErrorFormat("OnJoinRoomFailed roomName={0} errorCode={1} errorMessage={2}", this.RoomName, returnCode, message);
    }

    public void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom");
    }

    void Update()
    {
        
    }


}

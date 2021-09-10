using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.Unity;
using Photon.Realtime;

public class Speaker : MonoBehaviour, IConnectionCallbacks, IMatchmakingCallbacks, IInRoomCallbacks
{
    public bool autoConnect = true;

    [SerializeField]
    private bool publishUserId = false;

    private VoiceConnection voiceConnection;

    public GameObject audioListenerObj;

    public AudioListener listener;

    private readonly EnterRoomParams enterRoomParams = new EnterRoomParams
    {
        RoomOptions = new RoomOptions()
    };
    public string RoomName;
    void Start()
    {
        Debug.Log("Start");
    }
    public void MakeAudioListener()
    {
        audioListenerObj = new GameObject("AudioListenerObj");
        listener = audioListenerObj.AddComponent<AudioListener>();
        AudioListener.volume = 1.0f;
    }
    private void Awake()
    {
        this.voiceConnection = this.GetComponent<VoiceConnection>();
        MakeAudioListener();
        Debug.Log("Awake");
    }
    private void OnEnable()
    {
        this.voiceConnection.Client.AddCallbackTarget(this);
        Debug.Log("OnEnable");
        this.voiceConnection.ConnectUsingSettings();
        
    }
    private void OnDisable()
    {
        this.voiceConnection.Client.RemoveCallbackTarget(this);
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
        int friend= friendList.Count;
        Debug.LogFormat("{0}",friend);
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

    public void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom");
        this.voiceConnection.Client.OpFindFriends(null);
    }
    public void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("OnPlayerLeftRoom");
    }
    public void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {

    }
    public void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {

    }
    public void OnMasterClientSwitched(Player newMasterClient)
    {

    }

    void Update()
    {
        
    }
}

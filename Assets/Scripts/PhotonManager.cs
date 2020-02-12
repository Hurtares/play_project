using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonManager : MonoBehaviourPunCallbacks
{
	List<string> PlayerNames;
	List<RoomInfo> RoomList;
	String PlayerName;
	int PlayerId;

	public PhotonManager Instance;

	void Awake()
	{
		if ( Instance == null )
		{
			Instance = this;
		}
		else
		{
			Destroy( gameObject );
		}
	}

	void Start()
	{
		ConnectToServer( "RandomName" );

		PlayerNames = new List<string>();
		RoomList = new List<RoomInfo>();
	}

	void ConnectToServer( string playerName )
	{
		//primeira vez que se liga ao server
		PhotonNetwork.LocalPlayer.NickName = playerName;
		PhotonNetwork.ConnectUsingSettings();
		PlayerName = playerName;
	}

	public override void OnConnectedToMaster()
	{
		Debug.Log( "OnConnectedToMaster() was called by PUN." );
		Debug.Log( $"there are {PhotonNetwork.CountOfRooms} rooms" );
		PhotonNetwork.JoinLobby();
		Debug.Log( $"PlayerInLobby:{PhotonNetwork.InLobby}" );
		//get all the rooms 
	}

	public override void OnJoinedRoom()
	{
		base.OnJoinedRoom();
		PlayerId = PhotonNetwork.LocalPlayer.ActorNumber;
		Debug.Log( $"Player {PlayerName},{PlayerId} Joined Room : {PhotonNetwork.CurrentRoom.Name}" );
		UpdatePlayerNames();
	}

	public override void OnJoinRoomFailed( short returnCode, string message )
	{
		base.OnJoinRoomFailed( returnCode, message );
		Debug.Log( $"Faild to enter room{message}" );
	}

	public void UpdatePlayerNames()
	{
		Debug.LogError( "Players in room :" );

		foreach ( var player in PhotonNetwork.CurrentRoom.Players )
		{
			//if ( player.Value.NickName != PlayerName )
			if ( player.Value.ActorNumber != PlayerId )
			{
				Debug.LogError( player.Value.NickName );
				PlayerNames.Add( player.Value.NickName );
			}
			else
			{
				Debug.LogError( "You" );
			}
		}
	}

	public override void OnRoomListUpdate( List<RoomInfo> roomList )
	{
		Debug.Log( $"there are {PhotonNetwork.CountOfRooms} rooms" );
		Debug.LogError( $"List of rooms {roomList.Count}:" );

		RoomList = roomList;

		foreach ( var room in roomList )
		{
			Debug.LogError( room.Name );
		}
	}

	public override void OnDisconnected( DisconnectCause cause )
	{
		base.OnDisconnected( cause );
		Debug.LogWarning( $"OnDisconected() was called with reason {cause}" );
	}

	public void JoinRoom( string roomName )
	{
		PhotonNetwork.JoinRoom( roomName );
	}

	public void CreateRoom( string roomName )
	{
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.IsVisible = false;
		roomOptions.MaxPlayers = 4;

		PhotonNetwork.CreateRoom( roomName, roomOptions, TypedLobby.Default );
	}

	public void LeaveRoom()
	{
		Debug.Log( $"PlayerInLobby:{PhotonNetwork.InLobby}" );
		Debug.LogAssertion( "LeaveRoom" );
		PhotonNetwork.LeaveRoom();
		PhotonNetwork.JoinLobby();
		Debug.Log( $"PlayerInLobby:{PhotonNetwork.InLobby}" );
	}
}
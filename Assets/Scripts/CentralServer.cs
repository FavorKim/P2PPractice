using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct RoomInfo
{
    private string ip;
    private int port;
    private bool active;

    public bool Active
    {
        get { return active; }
        set
        {
            active = value;
        }
    }
    public RoomInfo(string ip, int port, bool active)
    {
        this.ip = ip;
        this.port = port;
        this.active = active;
    }
    public string GetIP() { return ip; }
}

public class CentralServer : NetworkBehaviour
{
    public static bool isInitalized = false;
    private static CentralServer instance;
    public static CentralServer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<CentralServer>();
                if (instance == null)
                {
                    var obj = new GameObject("CentralServer");
                    obj.AddComponent<NetworkIdentity>();
                    instance = obj.AddComponent<CentralServer>();
                }
            }
            return instance;
        }
    }
    List<RoomInfo> roomList = new List<RoomInfo>();

    public RoomInfo roomToSend;

    [Command(requiresAuthority = false)]
    public void OnCreateRoom(string ip)
    {
        var newRoom = new RoomInfo(ip, 7777, true);
        roomList.Add(newRoom);
        
    }

    [Command(requiresAuthority = false)]
    public void CmdOnJoinRoom(Client client)
    {
        RoomInfo roomInfo = default;

        foreach(var room in roomList)
        {
            if (room.Active == true)
            {
                roomInfo = room;
                break;
            }
        }
        if(roomInfo.Active == true)
        {
            RpcOnJoinRoom(client);
            //client.OnJoinRoom(roomInfo);
        }
        else
        {
            return;
        }
    }

    [ClientRpc]
    public void RpcOnJoinRoom(Client client)
    {
        client.OnJoinRoom();
    }

    
}

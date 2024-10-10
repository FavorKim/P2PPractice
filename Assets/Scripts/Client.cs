using kcp2k;
using Mirror;
using System.Net.Sockets;
using System.Net;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Client : NetworkBehaviour
{
    [SerializeField] Text txt;
    [SerializeField] Button createBtn;
    [SerializeField] Button joinBtn;
   
    public override void OnStartServer()
    {
        base.OnStartServer();
    }



    private void OnEnable()
    {
        txt = GameObject.Find("DebugText").GetComponent<Text>();
        //txt.text = "클라";
        createBtn = GameObject.Find("Button_Host").GetComponent<Button>();
        createBtn.onClick.AddListener(OnCreateRoom);
        joinBtn = GameObject.Find("Button_Client").GetComponent<Button>();
        joinBtn.onClick.AddListener(OnClick_JoinRoom);
    }

    private void OnDisable()
    {
        createBtn.onClick.RemoveListener(OnClick_JoinRoom);
        joinBtn.onClick.RemoveListener(OnCreateRoom);
    }

    public void OnCreateRoom()
    {
        string ip = GetLocalIPAddress();
        Transport port = Transport.active;
        CentralServer.Instance.OnCreateRoom(ip, this);
        NetworkManager.singleton.StopClient();
        NetworkManager.singleton.networkAddress = ip;
        NetworkManager.singleton.transport = port;
        NetworkManager.singleton.StartHost();
        txt.text = $"아이피 주소 : {ip}";
    }

    public void OnClick_JoinRoom()
    {
        CentralServer.Instance.CmdOnJoinRoom(this);
    }

    public void OnJoinRoom(RoomInfo info)
    {
        
        string ip;
        if(info.Active == true)
        {
            ip = info.GetIP();
            //NetworkManager.singleton.StopClient();
            NetworkManager.singleton.networkAddress = ip;
            NetworkManager.singleton.transport = Transport.active;
            NetworkManager.singleton.StartClient();
        }
        else
        {
            txt.text = "방 없음";
        }
    }

    private string GetLocalIPAddress()
    {
        string localIP = "Not available"; // 기본값
        try
        {
            // 호스트 이름을 가져오고, IP 주소 목록을 가져옵니다.
            string hostName = Dns.GetHostName();
            IPHostEntry hostEntry = Dns.GetHostEntry(hostName);

            foreach (IPAddress ip in hostEntry.AddressList)
            {
                // IPv4 주소만 가져오기 (IPv6은 제외)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            txt.text = "로컬 ip주소를 얻어오는 것에 실패함";

            Debug.LogError($"Error fetching local IP address: {ex.Message}");
        }

        return localIP;
    }

    public override void OnStopServer()
    {
        NetworkServer.UnSpawn(gameObject);
        base.OnStopServer();
    }

}

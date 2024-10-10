using Mirror;
using UnityEngine;
using UnityEngine.UI;
public class MyNetworkManager : NetworkManager
{
    [SerializeField] private GameObject client;
    [SerializeField] private GameObject centralServer;

    public override void Start()
    {
        base.Start();
        StartClient();
    }

    public override void OnServerReady(NetworkConnectionToClient conn)
    {
        base.OnServerReady(conn);
        if (!CentralServer.isInitalized)
        {
            var obj = Instantiate(centralServer);
            NetworkServer.Spawn(obj);
            CentralServer.isInitalized = true;
        }
        //var player = Instantiate(playerPrefab);
        //NetworkServer.Spawn(player);
    }


    public void OnClick_StartClient()
    {

    }

    public void OnClick_StartHost()
    {

    }
}

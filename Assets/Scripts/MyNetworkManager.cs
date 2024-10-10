using Mirror;
using UnityEngine;
public class MyNetworkManager : NetworkManager
{
    public override void Start()
    {
        base.Start();
        StartClient();
    }

}

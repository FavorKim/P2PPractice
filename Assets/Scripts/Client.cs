using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class Client : NetworkBehaviour
{
    [SerializeField] Text txt;
    public override void OnStartClient()
    {
        base.OnStartClient();
        txt.text = "클라";
    }
}

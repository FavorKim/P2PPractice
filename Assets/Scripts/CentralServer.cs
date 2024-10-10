using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class CentralServer : NetworkBehaviour
{
    [SerializeField] Text txt;
    void Start()
    {
        if (isServerOnly)
            txt.text = "서버";
    }

}

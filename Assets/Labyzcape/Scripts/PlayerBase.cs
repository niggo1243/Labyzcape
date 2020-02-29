using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Labyzcape
{
    public class PlayerBase : NetworkBehaviour
    {
        [SyncVar]
        public string playerName;

        public static event Action<PlayerBase, string> OnMessage;

        [Command]
        public void CmdSend(string message)
        {
            if (message.Trim() != "")
                RpcReceive(message.Trim());
        }

        [ClientRpc]
        public void RpcReceive(string message)
        {
            OnMessage?.Invoke(this, message);
        }
    }
}

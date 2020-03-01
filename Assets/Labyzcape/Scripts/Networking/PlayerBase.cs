using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Labyzcape.Networking
{
    public class PlayerBase : NetworkBehaviour
    {
        [SyncVar]
        public string playerName;

        public static event Action<PlayerBase, string> OnMessage;
        public static event Action OnStartLocalPlayerEvent;

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();

            Debug.Log("Starting local player");

            NetworkServer.Listen(GameConfig.MAX_CONNECTIONS_TO_LISTEN);

            OnStartLocalPlayerEvent?.Invoke();
            SceneNetworkManipulator.Instance.InitManager();
        }


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

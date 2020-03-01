using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

namespace Labyzcape
{
    public class LabyzcapeNetworkManager : NetworkManager
    {
        public string playerName { get; set; }

        public void SetHostname(string hostname)
        {
            this.networkAddress = hostname;
        }

        public class CreatePlayerMessage : MessageBase
        {
            public string name;
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
        }

        public override void OnClientConnect(NetworkConnection conn)
        {
            base.OnClientConnect(conn);
        }
    }
}

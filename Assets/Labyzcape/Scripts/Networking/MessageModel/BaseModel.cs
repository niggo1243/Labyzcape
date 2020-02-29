using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace Labyzcape.Networking.MessageModel
{
    [Serializable]
    public class BaseModel
    {
        public int messageType = -1;

        /// <summary>
        /// corridor:
        /// full cross, corner, straight, half cross
        /// traps:
        /// spikes, ...
        /// </summary>
        public int prefabType = 0;
    }
}

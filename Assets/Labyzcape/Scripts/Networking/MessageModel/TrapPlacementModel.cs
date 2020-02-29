using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Labyzcape.Networking.MessageModel
{
    [Serializable]
    public class TrapPlacementModel : BaseModel
    {
        public bool isTrapActive;
    }
}

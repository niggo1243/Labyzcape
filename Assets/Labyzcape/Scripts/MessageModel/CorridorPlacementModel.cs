using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace Labyzcape.MessageModel
{
    [Serializable]
    public class CorridorPlacementModel : BaseModel
    {
        /// <summary>
        /// north, east, south, west
        /// </summary>
        public int corridorDirection = 0;

        public Vector3 corridorPosition;
    }
}

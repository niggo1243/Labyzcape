using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using Labyzcape.Helpers;
using Labyzcape.Networking;

namespace Labyzcape.Corridor
{
    public class CorridorManager : MonoBehaviour
    {
        public List<CorridorBehaviour> staticCorridors = new List<CorridorBehaviour>();

        public List<CorridorSliderContainer> corridorSliderContainers = new List<CorridorSliderContainer>();

        /// <summary>
        /// disable all sliding cors
        /// </summary>
        private void OnDisable()
        {
            foreach(CorridorSliderContainer corridorSliderContainer in this.corridorSliderContainers)
            {
                HandleCoroutines.StopCor(SceneNetworkManipulator.Instance, corridorSliderContainer.slideCoroutine);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Labyzcape.Helpers;
using Labyzcape.Networking;

namespace Labyzcape.Corridor
{
    public class CorridorSliderContainer : MonoBehaviour
    {
        public List<CorridorBehaviour> dynamicCorridors = new List<CorridorBehaviour>();
        public GameConfig.CorridorSliderTypes corridorSliderType = GameConfig.CorridorSliderTypes.VerticalCorridorSlider;

        public Vector3 moveAxis = Vector3.right;

        [HideInInspector]
        public Coroutine slideCoroutine;

        [SerializeField]
        private float maxRaycastDistance = 30;

        private int corridorToKillIndex;

        public void SlideCorridors(CorridorBehaviour newCorridor, bool inverse)
        {
            this.corridorToKillIndex = inverse ? 0 : this.dynamicCorridors.Count - 1;

            this.dynamicCorridors.Add(newCorridor);

            //TODO start coroutine to move the corridors to the desired dest and destroy the corridor to kill
            HandleCoroutines.StartOneCoroutine(SceneNetworkManipulator.Instance, this.slideCoroutine, out this.slideCoroutine, this.SlideCorridorsMovement());
        }

        public void RefreshCorridorsList()
        {
            RaycastHit[] raycastHits = Physics.RaycastAll(this.transform.position, this.transform.forward, 
                this.maxRaycastDistance, LayerMask.NameToLayer(GameConfig.LAYER_MASK_CORRIDOR_STRING));

            this.dynamicCorridors.Clear();

            foreach(RaycastHit raycastHit in raycastHits)
            {
                CorridorBehaviour corridorBehaviour = raycastHit.collider.GetComponent<CorridorBehaviour>();

                if (corridorBehaviour != null)
                {
                    this.dynamicCorridors.Add(corridorBehaviour);
                }
            }
        }

        private IEnumerator SlideCorridorsMovement()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
}

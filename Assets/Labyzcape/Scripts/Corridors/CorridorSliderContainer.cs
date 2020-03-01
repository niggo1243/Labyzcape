using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Labyzcape.Helpers;
using Labyzcape.Networking;

using System;
using Mirror;

namespace Labyzcape.Corridor
{
    public class CorridorSliderContainer : MonoBehaviour
    {
        public List<CorridorBehaviour> dynamicCorridors = new List<CorridorBehaviour>();
        public GameConfig.CorridorSliderTypes corridorSliderType = GameConfig.CorridorSliderTypes.VerticalCorridorSlider;

        public Vector3 moveAxis = Vector3.right;

        public static bool isMoving = false;

        [SerializeField]
        private Transform helperInstance;

        [HideInInspector]
        public Coroutine slideCoroutine;

        [SerializeField]
        private float maxRaycastDistance = 30, unlimitedPower = 10, stepDistance = 1.5f;

        private void Start()
        {
            PlayerBase.OnStartLocalPlayerEvent += this.OnStartLocalPlayer;
        }

        private void OnStartLocalPlayer()
        {
            this.RefreshCorridorsList();
        }

        public void SlideCorridors(CorridorBehaviour newCorridor, bool inverse)
        {
            if (isMoving) return;

            this.RefreshCorridorsList();

            this.dynamicCorridors.Add(newCorridor);

            HandleCoroutines.StartOneCoroutine(SceneNetworkManipulator.Instance, this.slideCoroutine, out this.slideCoroutine, this.SlideCorridorsMovement(newCorridor, inverse));
        }

        public void RefreshCorridorsList()
        {
            RaycastHit[] raycastHits = Physics.RaycastAll(this.transform.position, this.transform.forward, 
                this.maxRaycastDistance);

            this.dynamicCorridors.Clear();

            foreach(RaycastHit raycastHit in raycastHits)
            {
                CorridorBehaviour corridorBehaviour = raycastHit.collider.GetComponent<CorridorBehaviour>();

                if (corridorBehaviour == null && raycastHit.collider.transform.parent != null)
                    corridorBehaviour = raycastHit.collider.transform.parent.GetComponent<CorridorBehaviour>();

                if (corridorBehaviour != null && !corridorBehaviour.IsStatic)
                {
                    this.dynamicCorridors.Add(corridorBehaviour);
                }
            }
        }


        private IEnumerator SlideCorridorsMovement(CorridorBehaviour newCorridor, bool inverted)
        {
            isMoving = true;
            int invert = inverted ? -1 : 1;

            this.helperInstance.rotation = Quaternion.identity;
            this.helperInstance.position = Vector3.zero;

            foreach (CorridorBehaviour corridorBehaviour in this.dynamicCorridors)
            {
                corridorBehaviour.transform.parent = this.helperInstance;
            }

            Vector3 nextTargetPos = this.helperInstance.position + (this.moveAxis * this.stepDistance * invert);

            while (Vector3.Distance(this.helperInstance.transform.position, nextTargetPos) > GameConfig.MIN_DISTANCE_CHECK_LERP)
            {
                this.helperInstance.transform.position = Vector3.Lerp(this.helperInstance.transform.position, nextTargetPos, this.unlimitedPower * Time.fixedDeltaTime);

                yield return new WaitForFixedUpdate();
            }

            this.helperInstance.transform.position = nextTargetPos;

            foreach (CorridorBehaviour corridorBehaviour in this.dynamicCorridors)
            {
                if (corridorBehaviour == null) continue;

                corridorBehaviour.transform.parent = null;
            }

            this.dynamicCorridors.TrimExcess();

            newCorridor.spawnProtection = false;
            isMoving = false;
        }
    }
}

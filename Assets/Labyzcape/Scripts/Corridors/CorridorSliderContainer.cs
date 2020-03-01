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

        [HideInInspector]
        public Coroutine slideCoroutine;

        [SerializeField]
        private float maxRaycastDistance = 30, unlimitedPower = 10, stepDistance = 1.5f;

        private int corridorToKillIndex;

        private void Start()
        {
            PlayerBase.OnStartLocalPlayerEvent += OnStartLocalPlayer;
        }

        private void OnStartLocalPlayer()
        {
            this.RefreshCorridorsList();
        }

        public void SlideCorridors(CorridorBehaviour newCorridor, bool inverse)
        {
            this.corridorToKillIndex = inverse ? 0 : this.dynamicCorridors.Count - 1;

            this.dynamicCorridors.Add(newCorridor);

            //TODO start coroutine to move the corridors to the desired dest and destroy the corridor to kill
            HandleCoroutines.StartOneCoroutine(SceneNetworkManipulator.Instance, this.slideCoroutine, out this.slideCoroutine, this.SlideCorridorsMovement(inverse));
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


        private IEnumerator SlideCorridorsMovement(bool inverted)
        {
            int invert = inverted ? -1 : 1;
            int counter = 0;

            while (counter ++ < 12000)
            {
                foreach(CorridorBehaviour corridorBehaviour in this.dynamicCorridors)
                {
                    corridorBehaviour.corRigidBody.AddForce(this.moveAxis * this.unlimitedPower * invert);
                }

                yield return new WaitForFixedUpdate();
            }

            try
            {
                CorridorBehaviour corridorBehaviourToDestroy = this.dynamicCorridors[this.corridorToKillIndex];
                this.dynamicCorridors.RemoveAt(this.corridorToKillIndex);

                NetworkServer.Destroy(corridorBehaviourToDestroy.gameObject);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }

            this.RefreshCorridorsList();
        }
    }
}

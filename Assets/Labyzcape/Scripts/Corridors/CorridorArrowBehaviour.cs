using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Labyzcape.Networking;

namespace Labyzcape.Corridor
{
    public class CorridorArrowBehaviour : MonoBehaviour, IMouseClickable
    {
        [SerializeField]
        private CorridorSliderContainer targetCorridorSlider;

        [SerializeField]
        private bool inverseSlide;

        [SerializeField]
        private Transform corridorStartingPositionTransform;

        private void Start()
        {
            if (this.corridorStartingPositionTransform == null)
            {
                this.corridorStartingPositionTransform = this.transform;
            }
        }

        public void GotClicked()
        {
            if (CorridorSliderContainer.isMoving) return;

            GameObject corridor = SceneNetworkManipulator.Instance.PlaceCorridorForAll(corridorStartingPositionTransform.position);

            this.targetCorridorSlider.SlideCorridors(corridor.GetComponent<CorridorBehaviour>(), this.inverseSlide);
        }

    }
}

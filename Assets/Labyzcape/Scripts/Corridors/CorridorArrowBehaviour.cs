using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Labyzcape.Corridor
{
    public class CorridorArrowBehaviour : MonoBehaviour
    {
        [SerializeField]
        private CorridorSliderContainer targetCorridorSlider;

        [SerializeField]
        private bool inverseSlide;

        public void InitSlide()
        {
            //this.corridorSlider.SlideCorridors();
        }
    }
}

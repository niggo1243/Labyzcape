using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Labyzcape.Corridor
{
    public class CorridorArrowBehaviour : MonoBehaviour, IMouseClickable
    {
        [SerializeField]
        private CorridorSliderContainer targetCorridorSlider;

        [SerializeField]
        private bool inverseSlide;

        public void GotClicked()
        {
            //this.corridorSlider.SlideCorridors(, this.inverseSlide);
        }

    }
}

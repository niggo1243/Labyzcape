using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Labyzcape
{
    public static class GameConfig
    {
        public const string LAYER_MASK_CORRIDOR_STRING = "CorridorLayer";

        public enum CorridorTypes
        {
            FullCross = 0,
            HalfCross = 1,
            Straight = 2,
            Corner = 3
        }

        public enum TrapTypes
        {
            Spikes = 0
        }

        public enum MessageTypes
        {
            CorridorPlacement = 0,
            TrapPlacement = 1
        }

        public enum CorridorSliderTypes
        {
            VerticalCorridorSlider = 0,
            HorizontalCorridorSlider = 1
        }
    }
}

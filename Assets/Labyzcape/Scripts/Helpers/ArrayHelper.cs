using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Labyzcape.Helpers
{
    public static class ArrayHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="increment"></param>
        /// <param name="pointer"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static int PointerHandler(bool increment, int pointer, int maxLength)
        {
            if (increment && ++pointer >= maxLength)
            {
                pointer = 0;
            }
            else if (!increment && --pointer < 0)
            {
                if (maxLength - 1 < 0)
                {
                    pointer = 0;
                }
                else
                {
                    pointer = maxLength - 1;
                }
            }

            return pointer;
        }

        public static void LogList<T>(List<T> l)
        {
            foreach (T t in l)
            {
                Debug.Log(t);
            }
        }

    }

}

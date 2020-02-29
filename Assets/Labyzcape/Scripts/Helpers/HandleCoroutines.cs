using System.Collections;
using UnityEngine;

namespace Labyzcape.Helpers
{

    public static class HandleCoroutines
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="cor"></param>
        public static void StopCor(MonoBehaviour sender, Coroutine cor)
        {
            if (cor != null && sender != null)
            {
                sender.StopCoroutine(cor);
            }
        }

        /// <summary>
        /// Starts a Coroutine (only one instance)
        /// </summary>
        /// <param name="sender">the original sender</param>
        /// <param name="cor">the coroutine to stop</param>
        /// <param name="outCor">the coroutine to set</param>
        /// <param name="enumeratorToStart">the method to start</param>
        public static Coroutine StartOneCoroutine(MonoBehaviour sender, Coroutine cor, out Coroutine outCor, IEnumerator enumeratorToStart)
        {
            //stop the cor
            StopCor(sender, cor);

            try
            {
                return outCor = sender.StartCoroutine(enumeratorToStart);
            }
            catch (System.NullReferenceException ne)
            {
                Debug.LogWarning(ne.Message);
            }

            return outCor = null;
        }
    }

}
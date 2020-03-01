using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;

namespace Labyzcape.Corridor
{
    public class CorridorKillzone : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            //Debug.Log(other.tag);

            if (other.tag.Equals(GameConfig.TAG_CORRIDOR_STRING))
            {
                CorridorBehaviour root = other.gameObject.GetComponent<CorridorBehaviour>() == null ? other.transform.GetComponentInParent<CorridorBehaviour>()
                    : other.gameObject.GetComponent<CorridorBehaviour>();

                if (!root.spawnProtection)
                    NetworkServer.Destroy(root.gameObject);
            }
        }
    }
}

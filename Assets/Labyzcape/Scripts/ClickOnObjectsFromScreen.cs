using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Labyzcape
{
    
    public class ClickOnObjectsFromScreen : MonoBehaviour
    {
        [SerializeField]
        private Camera cameraScreen;

        [SerializeField]
        private float rayCastDistance = 100;

        private RaycastHit raycastHit;

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Physics.Raycast(this.cameraScreen.ScreenPointToRay(Input.mousePosition), out this.raycastHit, this.rayCastDistance);

                if (this.raycastHit.collider != null)
                {
                    this.raycastHit.collider.SendMessage(GameConfig.GOT_CLICKED_INTERFACE_METHOD_NAME);

                    if (this.raycastHit.collider.transform.parent != null)
                        this.raycastHit.collider.transform.parent.SendMessage(GameConfig.GOT_CLICKED_INTERFACE_METHOD_NAME);
                }
            }
        }
    }
}

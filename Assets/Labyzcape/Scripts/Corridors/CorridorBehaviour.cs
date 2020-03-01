using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;

namespace Labyzcape.Corridor
{
    public class CorridorBehaviour : NetworkBehaviour
    {
        public Rigidbody corRigidBody;

        [SerializeField]
        private bool isStatic = false;
        public bool IsStatic
        {
            get
            {
                return this.isStatic;
            }
        }


        private void Start()
        {
            if (this.corRigidBody == null)
                this.corRigidBody = this.GetComponent<Rigidbody>();
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
        }
    }
}

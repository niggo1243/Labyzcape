using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

using Mirror;
using System;

using Labyzcape.Networking.MessageModel;
using Labyzcape.Helpers;

namespace Labyzcape.Networking
{
    [Serializable]
    public class CorridorContainer
    {
        public GameConfig.CorridorTypes corridorType;

        public GameObject corridorPrefab;
    }

    public class SceneNetworkManipulator : Singleton<SceneNetworkManipulator>
    {
        public List<CorridorContainer> corridorContainers = new List<CorridorContainer>();

        private int currentSelectedCorridorIndexToSpawn = 0;

        public void InitManager()
        {
            foreach (CorridorContainer c in this.corridorContainers)
                ClientScene.RegisterPrefab(c.corridorPrefab);
        }

        public GameObject PlaceCorridorForAll(Vector3 startingPosition)
        {
            GameObject instance = MonoBehaviour.Instantiate(this.corridorContainers[this.currentSelectedCorridorIndexToSpawn].corridorPrefab);
            instance.name = this.corridorContainers[this.currentSelectedCorridorIndexToSpawn].corridorType.ToString();
            instance.transform.position = startingPosition;

            if (NetworkServer.active)
            {
                NetworkServer.Spawn(instance);
            }
            else
                Debug.LogWarning("NetworkServer not active");

            return instance;
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                this.currentSelectedCorridorIndexToSpawn = ArrayHelper.PointerHandler(true, this.currentSelectedCorridorIndexToSpawn, 
                    this.corridorContainers.Count);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                this.currentSelectedCorridorIndexToSpawn = ArrayHelper.PointerHandler(false, this.currentSelectedCorridorIndexToSpawn, 
                    this.corridorContainers.Count);
            }
        }
    }
}

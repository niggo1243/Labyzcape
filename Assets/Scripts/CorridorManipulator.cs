using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

using Mirror;
using System;
using Labyzcape.MessageModel;

namespace Labyzcape
{
    [Serializable]
    public class CorridorContainer
    {
        public GameConfig.CorridorTypes corridorType;

        public GameObject corridorPrefab;
    }

    public class CorridorManipulator : MonoBehaviour
    {
        [SerializeField]
        List<CorridorContainer> corridorContainers = new List<CorridorContainer>();

        private PlayerBase playerBaseLocal;

        public void InitManager()
        {
            PlayerBase.OnMessage += OnMessageReceivedFromPlayer;

            this.playerBaseLocal = NetworkClient.connection.identity.GetComponent<PlayerBase>();
        }

        private void OnMessageReceivedFromPlayer(PlayerBase playerBase, string message)
        {
            BaseModel model = null;
            try
            {
                model = JsonUtility.FromJson<BaseModel>(message);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return;
            }

            if (model == null)
            {
                Debug.LogError("model is empty");
                return;
            }

            //TODO check if player is the manipulator

            //TODO check messagetype in switch statement

            switch (model.messageType)
            {
                case (int)GameConfig.MessageTypes.CorridorPlacement:

                    CorridorPlacementModel corridorPlacementModel = JsonUtility.FromJson<CorridorPlacementModel>(message);

                    CorridorContainer targetContainerToPlace = this.corridorContainers.Find((container) =>
                    {
                        return model.prefabType == (int)container.corridorType;
                    });

                    GameObject instance = MonoBehaviour.Instantiate(targetContainerToPlace.corridorPrefab);
                    instance.name = targetContainerToPlace.corridorType.ToString();

                    instance.transform.position = corridorPlacementModel.corridorPosition;
                    //TODO add rotation

                    break;
                case (int)GameConfig.MessageTypes.TrapPlacement:
                    break;
            }

        }

        public void PlaceCorridorForAll()
        {
            CorridorPlacementModel corridorPlacementModel = new CorridorPlacementModel
            {
                messageType = (int)GameConfig.MessageTypes.CorridorPlacement,
                prefabType = 0,

                corridorPosition = new Vector3(UnityEngine.Random.Range(0, 20), 0, UnityEngine.Random.Range(0, 20)),
                corridorDirection = 0
            };

            string jsonString = JsonUtility.ToJson(corridorPlacementModel);

            this.playerBaseLocal.CmdSend(jsonString);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                this.PlaceCorridorForAll();
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Mirror;

namespace Labyzcape
{
    public class ChatWindowExample : MonoBehaviour
    {
        public InputField chatMessage;
        public Text chatHistory;
        public Scrollbar scrollbar;

        public void Awake()
        {

            PlayerBase.OnMessage += OnPlayerMessage;

        }

        private void OnPlayerMessage(PlayerBase player, string message)
        {
            string prettyMessage = player.isLocalPlayer ?
                $"<color=red>{player.playerName}: </color> {message}" :
                $"<color=blue>{player.playerName}: </color> {message}";
            AppendMessage(prettyMessage);

            Debug.Log(message);
        }

        public void OnSend()
        {
            if (chatMessage.text.Trim() == "") return;

            // get our player
            PlayerBase player = NetworkClient.connection.identity.GetComponent<PlayerBase>();

            // send a message
            player.CmdSend(chatMessage.text.Trim());

            chatMessage.text = "";
        }

        internal void AppendMessage(string message)
        {
            StartCoroutine(AppendAndScroll(message));
        }

        IEnumerator AppendAndScroll(string message)
        {
            chatHistory.text += message + "\n";

            // it takes 2 frames for the UI to update ?!?!
            yield return null;
            yield return null;

            // slam the scrollbar down
            scrollbar.value = 0;
        }
    }
}

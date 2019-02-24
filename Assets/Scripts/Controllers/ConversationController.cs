using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    class ConversationController
    {
        private TextAsset textAsset;
        private string[][] conversation;
        private int currentMessage = -1;
        private Text textBox;
        private Image image;
        private Sprite[] othersSprite;
        private Sprite playerSprite;
        private GameObject messageBox;
        private Action<string> action;

        public ConversationController(TextAsset textAsset, Text text, Image image, Sprite[] others, GameObject messageBox)
        {
            this.action = null;
            this.textAsset = textAsset;
            this.textBox = text;
            this.image = image;
            this.othersSprite = others;
            this.messageBox = messageBox;
            this.playerSprite = this.image.sprite;
            string[] lines = textAsset.text.Split('\n');
            conversation = new string[lines.Length][];
            for(int i = 0; i<lines.Length; i++)
            {
                conversation[i] = lines[i].Split('-');
            }
        }

        public ConversationController(TextAsset textAsset, Text text, Image image, Sprite[] others, GameObject messageBox, Action<string> action):this(textAsset, text, image, others, messageBox)
        {
            this.action = action;

        }
        private void startConversation()
        {
            messageBox.SetActive(true);
            currentMessage++;
        }

        public void nextMessage()
        {
            if (currentMessage == -1)
            {
                startConversation();
                nextMessage();
            }
            else if (currentMessage >= conversation.Length)
            {
                closeConversation();
            }
            else
            {
                textBox.text = conversation[currentMessage][1];
                changeSprite(int.Parse(conversation[currentMessage][0]));
                if (conversation[currentMessage].Length == 3)
                {
                    action(conversation[currentMessage][2]);
                }
                currentMessage++;
            }
        }

        public void closeConversation()
        {
            messageBox.SetActive(false);
            currentMessage = -1;
            changeSprite(0);
        }

        public void changeSprite(int sprite)
        {
            if (sprite == 0)
                image.sprite = playerSprite;
            else
            {
                image.sprite = othersSprite[sprite-1];
            }
        }
    }
}

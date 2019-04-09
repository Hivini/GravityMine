using Assets.Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    public GameObject message;
    public TextAsset textAsset;
    public Sprite[] sprites;
    public Text textBox;
    public Image image;

    public Action onActionFound;

    
    private Sprite playerSprite;

    private float responseTime;
    private ConversationController conversation;

    bool lastPressed;
    bool openPauseMenu;

    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        conversation = new ConversationController(textAsset, textBox, image, sprites, message, onActionFound);
        
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if (!PlayerController.openPauseMenu)
        {
            float interact = Input.GetAxisRaw("Fire1");
            if (message.activeSelf && interact == 1 && Time.time > responseTime)
            {
                conversation.nextMessage();
                responseTime = Time.time + .4f;
            }
            else if (interact == 1 && !message.activeSelf && Time.time > responseTime)
            {
                conversation.nextMessage();
                responseTime = Time.time + .4f;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        conversation.closeConversation();
    }
}

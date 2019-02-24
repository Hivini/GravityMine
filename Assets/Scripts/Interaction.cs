using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public GameObject message;
    public TextAsset textAsset;
    public Sprite sprite;
    public Text textBox;
    public Image image;

    private IActionOnInteraction action;

    private Sprite playerSprite;

    private float responseTime;
    private ConversationController conversation;

    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        action = GetComponent<ActionForPC>();
        conversation = new ConversationController(textAsset, textBox, image, new Sprite[] {sprite}, message, action.action);
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay(Collider other)
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

    private void OnTriggerExit(Collider other)
    {
        conversation.closeConversation();
    }
}

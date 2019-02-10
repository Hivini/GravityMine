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
    private Sprite playerSprite;
    private int cont;
    private string[] conversation;

    private float responseTime;
    private bool currentSprite; // if false current sprite is the object

    // Start is called before the first frame update
    void Start()
    {
        conversation = textAsset.text.Split('\n');
        cont = 0;
        playerSprite = image.sprite;
        image.sprite = sprite;
        responseTime = 0;
        currentSprite = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        
        float interact = Input.GetAxisRaw("Fire1");
        if (message.activeSelf && interact == 1 && Time.time > responseTime)
        {
            nextMessage();
            responseTime = Time.time + .4f;
        }
        else if (interact == 1 && !message.activeSelf && Time.time > responseTime)
        {
            message.SetActive(true);
            nextMessage();
            responseTime = Time.time + .4f;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        closeConversation();
    }

    private void nextMessage()
    {
        if (cont >= conversation.Length)
        {
            closeConversation();
        }
        else
        {
            textBox.text = conversation[cont];
            changeSprite();
            cont++;
        }
    }

    private void closeConversation()
    {
        message.SetActive(false);
        currentSprite = true;
        cont = 0;
    }

    private void changeSprite()
    {
        if (!currentSprite)
        {
            image.sprite = playerSprite;
        }
        else
        {
            image.sprite = sprite;
        }
        currentSprite = !currentSprite;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{

    public GameObject startInstructions, endInstructions, bestScore;

    private Text startInstructionsTextComponent, endInstructionsTextComponent, bestScoreTextComponent;
    
    void Start()
    {
        startDisplay();
        startInstructionsTextComponent =startInstructions.GetComponent<Text>(); 
        endInstructionsTextComponent = endInstructions.GetComponent<Text>();
        bestScoreTextComponent = bestScore.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startDisplay()
    {
        this.gameObject.SetActive(true);
        startInstructions.SetActive(true);
        endInstructions.SetActive(false);
        bestScore.SetActive(true);
    }

    public void endDisplay()
    {
        this.gameObject.SetActive(true);
        startInstructions.SetActive(false);
        endInstructions.SetActive(true);
        bestScore.SetActive(true);
    }

    public void hideAll()
    {
        this.gameObject.SetActive(false);
    }

    public void setBestScoreText(string bestScore)
    {
        bestScoreTextComponent.text = bestScore;
    }

    public void setStartInstructions(string start)
    {
        startInstructionsTextComponent.text = start;
    }

    public void setEndInstructions(string end)
    {
        endInstructionsTextComponent.text = end;
    }

}

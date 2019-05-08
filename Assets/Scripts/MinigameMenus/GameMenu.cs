using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{

    public GameObject startInstructions, endInstructions, bestScore;

    private Text startInstructionsTextComponent, endInstructionsTextComponent, bestScoreTextComponent;
    private string stringStartInstructions, stringEndInstructions, stringBestScore;
    
    void Start()
    {
        startDisplay();
        startInstructionsTextComponent = startInstructions.GetComponent<Text>(); 
        endInstructionsTextComponent = endInstructions.GetComponent<Text>();
        bestScoreTextComponent = bestScore.GetComponent<Text>();
        if (stringStartInstructions != null)
        {
            startInstructionsTextComponent.text = stringStartInstructions;
        }
        if (stringEndInstructions != null)
        {
            endInstructionsTextComponent.text = stringEndInstructions;
        }
        if (stringBestScore != null)
        {
            bestScoreTextComponent.text = stringBestScore;
        }
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
        if(bestScoreTextComponent!=null)
            bestScoreTextComponent.text = bestScore;
        else
            stringBestScore = bestScore;
    }

    public void setStartInstructions(string start)
    {
        if (startInstructionsTextComponent != null)
            startInstructionsTextComponent.text = start;
        else
            stringStartInstructions = start;
    }

    public void setEndInstructions(string end)
    {
        if (endInstructionsTextComponent != null)
            endInstructionsTextComponent.text = end;
        else
            stringEndInstructions = end;
    }

}

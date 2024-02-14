using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questsystem : MonoBehaviour
{

    public string[] currentQuest;
    public GameObject[] currentUI;

    public string[] quest1;
    public string[] quest2;
    public string[] quest3;
    public GameObject[] questUI1;
    public GameObject[] questUI2;
    public GameObject[] questUI3;

    public string currentText;
    int questLength;
    public int textNumber;
    public int uiNumber;

    public StringDelay questText;
    // Start is called before the first frame update
    void Start()
    {
        QuestStart(1);
    }

    public void QuestStart(int questnumber)
    {

        textNumber = 0;
        switch(questnumber)
        {
            case 1:
                questLength = quest1.Length;
                break;
            case 2:
                questLength = quest2.Length;
                break;
            case 3:
                questLength = quest3.Length;
                break;
            default:
                questLength = 0;
                break;
        }

        currentQuest = new string[questLength];

        for (int i = 0; i < questLength; i++)
        {
            switch(questnumber)
            {
                case 1:
                    currentQuest[i] = quest1[i];
                    break;
                case 2:
                    currentQuest[i] = quest2[i];
                    break;
                case 3:
                    currentQuest[i] = quest3[i];
                    break;
            }
            
        }

        currentText = currentQuest[0];
        Invoke("StringPlay", 0.15f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextText()
    {
        if (textNumber < currentQuest.Length - 1)
        {
            textNumber++;
            currentText = currentQuest[textNumber];
            questText.StringPlay();
        }
        else if(uiNumber < currentUI.Length - 1)
        {
            Debug.Log("Äù½ºÆ® ³¡³²");
        }
    }

    public void StringPlay()
    {
        questText.StringPlay();
    }
}

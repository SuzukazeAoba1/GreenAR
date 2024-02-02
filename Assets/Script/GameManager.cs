using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        rador,ar,combat,result,dogam
    }

    static GameManager instance;

    private RectTransform currentUI;
    public GameState gameState = GameState.rador;

    public RectTransform radorUI;
    public RectTransform arUI;
    public RectTransform combatUI;
    public RectTransform resultUI;
    public RectTransform dogamUI;

    
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        currentUI = radorUI;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UILoad(int goui)
    {
        currentUI.localScale = new Vector3(0, 0, 0);
        switch(goui)
        {

            case 1:
                gameState = GameState.rador;
                currentUI = radorUI;
                break;
            case 2:
                gameState = GameState.ar;
                currentUI = arUI;
                break;
            case 3:
                gameState = GameState.combat;
                currentUI = combatUI;
                break;
            case 4:
                gameState = GameState.result;
                currentUI = resultUI;
                break;
            case 5:
                gameState = GameState.dogam;
                currentUI = dogamUI;
                break;
            default:
                gameState = GameState.rador;
                currentUI = radorUI;
                break;
        }

        currentUI.localScale = new Vector3(1, 1, 1);
    }
}

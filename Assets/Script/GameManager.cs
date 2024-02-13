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

    public GameObject Origin;
    public GameObject Session;
    public GameObject Camera;
    public GameObject Monster;

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
        currentUI.gameObject.SetActive(false);
        switch(goui)
        {

            case 1:
                gameState = GameState.rador;
                currentUI = radorUI;
                ARCamChange(false);
                break;
            case 2:
                gameState = GameState.ar;
                currentUI = arUI;
                ARCamChange(true);
                Monster.SetActive(true);
                break;
            case 3:
                gameState = GameState.combat;
                currentUI = combatUI;
                ARCamChange(true);
                break;
            case 4:
                gameState = GameState.result;
                currentUI = resultUI;
                ARCamChange(false);
                break;
            case 5:
                gameState = GameState.dogam;
                currentUI = dogamUI;
                ARCamChange(false);
                break;
            default:
                gameState = GameState.rador;
                currentUI = radorUI;
                ARCamChange(false);
                break;
        }
        currentUI.gameObject.SetActive(true);
    }

    void ARCamChange(bool ARCam)
    {
        if(ARCam)
        {
            Origin.SetActive(true);
            Session.SetActive(true);
            Camera.SetActive(false);
        }
        else
        {
            Origin.SetActive(false);
            Session.SetActive(false);
            Camera.SetActive(true);
            Monster.SetActive(false);
        }

    }

}

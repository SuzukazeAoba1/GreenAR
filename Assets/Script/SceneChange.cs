using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void IntroScene()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void MainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void BattleScene()
    {
        SceneManager.LoadScene("BattleScene");
    }
}

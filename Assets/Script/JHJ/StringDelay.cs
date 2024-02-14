using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;

public class StringDelay : MonoBehaviour
{
    public bool startTyping=false;
    public TMP_Text tmp;

    
    public string str;

    private float stringDelay = 0.1f;
    public Questsystem questsystem;

  

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        startTyping = false;
        //gameManager = GameManager.instance;
        //str = tmp.text;
        tmp.text = "";
        //StartCoroutine(DelayText());
    }

    // Update is called once per frame
    void Update()
    {
        if (startTyping)
        {
            startTyping=!startTyping;
            tmp.text = "";
            StartCoroutine(DelayText());
        }
    }
    IEnumerator DelayText()
    { 
        string[] arrStr = str.Split("\\n");
        for(int j = 0; j < arrStr.Length; j++)
        {
            for (int i = 0; i < arrStr[j].Length; i++)
            {
                Debug.Log(i);
                tmp.text += arrStr[j][i];
                if (!char.IsWhiteSpace(arrStr[j][i]))
                {
                    //gameManager.audioManager.CreateSFXAudioSource(gameManager.playerVR.gameObject, gameManager.audioManager.FindSFXAudioClipByString("SansSpeak"));
                }
                yield return new WaitForSeconds(stringDelay);
            }
            tmp.text += "\\n";
            tmp.text = tmp.text.Replace("\\n", "\n");
        }
        startTyping =false;
    }
   
    /*public IEnumerator DataPlay()
    {
        tmp.text = "";
        //AudioManager audio = GameManager.instance.audioManager;
        yield return new WaitForSeconds(GetComponent<AudioSource>().FindSFXAudioClipByString("�ؼ�").length+0.1f);
        //audio.CreateSFXAudioSource(GameManager.instance.playerVR, audio.FindSFXAudioClipByString("���� �����"));
        yield return new WaitForSeconds(GetComponent<AudioSource>().FindSFXAudioClipByString("���� �����").length+0.1f);
        if (answerType == 1)
        {
            //str = (string)GameManager.instance.webTest.getData(0, "trashCanText1", QuizManager.Instance.question);
        }
        else if(answerType == 2)
        {
            str = (string)GameManager.instance.webTest.getData(0, "trashCanText2", QuizManager.Instance.question);
        }
        else
        {
            Debug.LogError("asnwertype�� �߸� �����Ͽ����ϴ�");
        }
        startTyping = !startTyping;
    }*/

    public void StringPlay()
    {
        StopAllCoroutines();
        tmp.text = "";
        Debug.Log(questsystem.currentText);
        str = questsystem.currentText;
        
        

        startTyping =true;
    }
}

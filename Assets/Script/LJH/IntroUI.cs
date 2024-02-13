using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroUI : MonoBehaviour
{
    public enum State
    {
        fadein, fadeout
    }
    public Image myImage;
    public float alphaSpeed = 0.5f;
    public Sprite[] stories;
    public float audiodeacreaseTime;

    public State state = State.fadein;
    private AudioSource myaudio;
    public AudioSource booksound;
    public bool sounddecrease = false;
    // Start is called before the first frame update
    void Start()
    {
        myaudio = GetComponent<AudioSource>();
        StartCoroutine(StartStory());
    }

    // Update is called once per frame
    void Update()
    {
        float currentAlpha;
        switch (state)
        {
            case State.fadeout:
                currentAlpha = myImage.color.a;

                // Time.deltaTime�� ����Ͽ� ���� �� ����
                currentAlpha -= alphaSpeed * Time.deltaTime;

                // ���� ���� 0���� 1 ���̷� ����
                currentAlpha = Mathf.Clamp01(currentAlpha);

                // ����� ���� ���� �̹����� ����
                myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, currentAlpha);
                break;
            case State.fadein:
                currentAlpha = myImage.color.a;

                // Time.deltaTime�� ����Ͽ� ���� �� ����
                currentAlpha += alphaSpeed * Time.deltaTime;

                // ���� ���� 0���� 1 ���̷� ����
                currentAlpha = Mathf.Clamp01(currentAlpha);

                // ����� ���� ���� �̹����� ����
                myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, currentAlpha);
                break;
        }

        if(sounddecrease)
        {
            DecreaseVolume();
        }

        
        
        
    }

    public void skip()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public IEnumerator StartStory()
    {
        //yield return new WaitForSeconds(3.5f);
        //state = State.fadeout;
        yield return new WaitForSeconds(0.2f);
        int a = stories.Length;
        //myaudio.Play();
        for (int i = 0; i < a; i++)
        {
            myImage.sprite = stories[i];
            //booksound.Play();
            state = State.fadein;

            yield return new WaitForSeconds(6f);

            if (i == a - 2)
            {
                //sounddecrease = true;
            }
            state = State.fadeout;

            yield return new WaitForSeconds(4f);

            myImage.sprite = stories[i];

            if (i == a - 1)
            {
                SceneManager.LoadScene("IntroScene");
            }
        }
    }

    void DecreaseVolume()
    {
        float currentVolume = myaudio.volume;
        float decreaseAmount = currentVolume / audiodeacreaseTime;
        myaudio.volume -= decreaseAmount * Time.deltaTime;
    }
}

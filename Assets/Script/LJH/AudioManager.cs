using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyAudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip myclip;
    public AudioClip myclip2;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        // AudioSource�� ������ �� ����� �ݹ� �Լ� ����
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.clip = myclip;
        audioSource.Play();
        StartCoroutine(WaitForAudioToEnd());
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "BattleScene" && audioSource.isPlaying)
        {
           MusicPause();
        }
        else if(SceneManager.GetActiveScene().name != "BattleScene" && !audioSource.isPlaying)
        {
           MusicPlay();
        }
    }

    public void MusicPause()
    {
        audioSource.Pause();
        audioSource.clip = myclip2;
        audioSource.loop = true;
    }

    public void MusicPlay()
    {
        audioSource.UnPause();
    }

    IEnumerator WaitForAudioToEnd()
    {
        // ������� ��� ���� ���� ���
        yield return new WaitWhile(() => audioSource.isPlaying);

        // ������� ������ �� ������ �ڵ�
        //Debug.Log("Audio has finished playing");
        audioSource.clip = myclip2;
        audioSource.loop = true;
        audioSource.Play();
        // ���⿡ ������ �Լ��� ������ �˴ϴ�.
    }
}
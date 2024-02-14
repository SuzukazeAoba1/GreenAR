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
        // AudioSource가 끝났을 때 실행될 콜백 함수 지정
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
        // 오디오가 재생 중인 동안 대기
        yield return new WaitWhile(() => audioSource.isPlaying);

        // 오디오가 끝났을 때 실행할 코드
        //Debug.Log("Audio has finished playing");
        audioSource.clip = myclip2;
        audioSource.loop = true;
        audioSource.Play();
        // 여기에 실행할 함수를 넣으면 됩니다.
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
//using Input = ARFoundationRemote.Input;

public class GPS_Manager : MonoBehaviour
{
    public event EventHandler OnGPSEvent;
    public float latitude = 0;
    public float longitude = 0;
    public float magneticHeading = 0;
    public float trueHeading = 0;

    public Text latitude_text;
    public Text longitude_text;
    public Text magnetic_text;
    public Text true_text;

    public float maxWaitTime = 10.0f;
    float waitTime = 0;

    public float resendTime = 1.0f;
    public bool receiveGPS = false;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GPS_On());
    }

    public IEnumerator GPS_On()
    {
        
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            while(!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                yield return null;
            }
        } //1. �㰡

        yield return new WaitForSeconds(3); //editor wait

        if (!Input.location.isEnabledByUser)
        {
            latitude_text.text = "GPS Off";
            longitude_text.text = "GPS Off";
            yield break;
        } //2. GPS ��ġ

        Input.location.Start(); // 3. ��û
        Input.compass.enabled = true; //��ħ�� Ȱ��ȭ

        while (Input.location.status == LocationServiceStatus.Initializing && waitTime < maxWaitTime)
        {
            yield return new WaitForSeconds(1.0f);
            waitTime++;
        } // 4. ����ϴµ���

        if(Input.location.status == LocationServiceStatus.Failed)
        {
            latitude_text.text = "��ġ ���� ���� ����";
            longitude_text.text = "��ġ ���� ���� ����";
        } // 5. ���� ����

        if (waitTime >= maxWaitTime)
        {
            latitude_text.text = "���� ��� �ð� �ʰ�";
            longitude_text.text = "���� ��� �ð� �ʰ�";
        } // 6. Ÿ�� �ƿ�

        LocationInfo li = Input.location.lastData;
        latitude = li.latitude;
        longitude = li.longitude;
        latitude_text.text = "���� : " + latitude.ToString("F5");
        longitude_text.text = "�浵 : " + longitude.ToString("F5");

        if (Input.compass.headingAccuracy == 0 || Input.compass.headingAccuracy > 0)
        {
            magneticHeading = Input.compass.magneticHeading;
            trueHeading = Input.compass.trueHeading;
            magnetic_text.text = magneticHeading.ToString("F5");
            true_text.text = trueHeading.ToString("F5");
        }

        // 7. GPS ���� ���
        receiveGPS = true;
        OnGPSEvent?.Invoke(this, EventArgs.Empty);
        while (receiveGPS)
        {
            yield return new WaitForSeconds(resendTime);
            li = Input.location.lastData;
            latitude = li.latitude;
            longitude = li.longitude; 
            latitude_text.text = "���� : " + latitude.ToString("F5");
            longitude_text.text = "�浵 : " + longitude.ToString("F5");

            if (Input.compass.headingAccuracy == 0 || Input.compass.headingAccuracy > 0)
            {
                magneticHeading = Input.compass.magneticHeading;
                trueHeading = Input.compass.trueHeading;
                magnetic_text.text = magneticHeading.ToString("F5");
                true_text.text = trueHeading.ToString("F5");
            }
        } // 8. GPS ���� ���
    }
}

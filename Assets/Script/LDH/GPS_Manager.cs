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
        } //1. 허가

        yield return new WaitForSeconds(3); //editor wait

        if (!Input.location.isEnabledByUser)
        {
            latitude_text.text = "GPS Off";
            longitude_text.text = "GPS Off";
            yield break;
        } //2. GPS 장치

        Input.location.Start(); // 3. 요청
        Input.compass.enabled = true; //나침반 활성화

        while (Input.location.status == LocationServiceStatus.Initializing && waitTime < maxWaitTime)
        {
            yield return new WaitForSeconds(1.0f);
            waitTime++;
        } // 4. 대기하는동안

        if(Input.location.status == LocationServiceStatus.Failed)
        {
            latitude_text.text = "위치 정보 수신 실패";
            longitude_text.text = "위치 정보 수신 실패";
        } // 5. 수신 실패

        if (waitTime >= maxWaitTime)
        {
            latitude_text.text = "응답 대기 시간 초과";
            longitude_text.text = "응답 대기 시간 초과";
        } // 6. 타임 아웃

        LocationInfo li = Input.location.lastData;
        latitude = li.latitude;
        longitude = li.longitude;
        latitude_text.text = "위도 : " + latitude.ToString("F5");
        longitude_text.text = "경도 : " + longitude.ToString("F5");

        if (Input.compass.headingAccuracy == 0 || Input.compass.headingAccuracy > 0)
        {
            magneticHeading = Input.compass.magneticHeading;
            trueHeading = Input.compass.trueHeading;
            magnetic_text.text = magneticHeading.ToString("F5");
            true_text.text = trueHeading.ToString("F5");
        }

        // 7. GPS 정보 출력
        receiveGPS = true;
        OnGPSEvent?.Invoke(this, EventArgs.Empty);
        while (receiveGPS)
        {
            yield return new WaitForSeconds(resendTime);
            li = Input.location.lastData;
            latitude = li.latitude;
            longitude = li.longitude; 
            latitude_text.text = "위도 : " + latitude.ToString("F5");
            longitude_text.text = "경도 : " + longitude.ToString("F5");

            if (Input.compass.headingAccuracy == 0 || Input.compass.headingAccuracy > 0)
            {
                magneticHeading = Input.compass.magneticHeading;
                trueHeading = Input.compass.trueHeading;
                magnetic_text.text = magneticHeading.ToString("F5");
                true_text.text = trueHeading.ToString("F5");
            }
        } // 8. GPS 정보 출력
    }
}

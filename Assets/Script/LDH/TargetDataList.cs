using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct TargetData        // 타겟 하나의 위경도 포함 모든 정보를 가지고 있는 객체
{
    public int id;
    public string name;
    public float lat;
    public float lon;

    public TargetData(int id, string name, float lat, float lon)
    {
        this.id = id;
        this.name = name;
        this.lat = lat;
        this.lon = lon;
    }

};

public class TargetDataList : MonoBehaviour
{
    public GPS_Manager gpsManager;
    public bool debugMode;
    public List<TargetData> targetsData;   //모든 타겟 데이터를 실제로 보관하는 장소

    private void Awake()
    {
        gpsManager.OnGPSEvent += TargetGPSData;
        gpsManager.debugMode = debugMode;
    }
    // Start is called before the first frame update
    void Start()
    {
        targetsData = new List<TargetData>();

        if (debugMode)
        {
            TargetTestData();
        }
    }
    void TargetTestData()       // GPS가 꺼졌을 경우에 사용되는 임시 데이터
    {
        targetsData.Add(new TargetData(1, "test1", 50f, 10f));
        targetsData.Add(new TargetData(2, "test2", 20f, 20f));
        targetsData.Add(new TargetData(3, "test3", 30f, 30f));
        targetsData.Add(new TargetData(4, "test4", 40f, 40f));
        targetsData.Add(new TargetData(5, "test5", 50f, 50f));
        targetsData.Add(new TargetData(6, "test6", 60f, 60f));
    }

    void TargetGPSData(object sender, EventArgs eventArgs)     //GPS가 켜져 있을 경우에 사용되는 실제 데이터 예제 (1당 1m)
    {
        targetsData.Clear();
        targetsData.Add(new TargetData(0, "test0", 3515955f, 12906030f));
        targetsData.Add(new TargetData(1, "test1", 3515905f, 12905995f));
        targetsData.Add(new TargetData(2, "test2", 3515998f, 12905955f));
        targetsData.Add(new TargetData(3, "test3", 3515938f, 12905794f));
        targetsData.Add(new TargetData(4, "test4", 3516042f, 12905828f));
        targetsData.Add(new TargetData(5, "test5", 3516060f, 12905928f));
        targetsData.Add(new TargetData(6, "test6", 3516058f, 12906114f));
    }
}

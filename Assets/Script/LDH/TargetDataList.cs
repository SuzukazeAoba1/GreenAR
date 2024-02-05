using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct TargetData        // Ÿ�� �ϳ��� ���浵 ���� ��� ������ ������ �ִ� ��ü
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
    public List<TargetData> targetsData;   //��� Ÿ�� �����͸� ������ �����ϴ� ���

    //bool test;
    //float testcount;

    // Start is called before the first frame update
    void Start()
    {
        targetsData = new List<TargetData>();

        //testcount = 0;

        //TargetGPSData();
        //TargetTestData();
        //InvokeRepeating("TargetMoveTest", 0, 1f);
    }

    void TargetGPSData()     //GPS�� ���� ���� ��쿡 ���Ǵ� ���� ������ ���� (1�� 1m)
    {
        targetsData.Add(new TargetData(0, "test0", 3515955f, 12906030f));
        targetsData.Add(new TargetData(1, "test1", 3515905f, 12905995f));
        targetsData.Add(new TargetData(2, "test2", 3515998f, 12905955f));
        targetsData.Add(new TargetData(3, "test3", 3515938f, 12905794f));
        targetsData.Add(new TargetData(4, "test4", 3516042f, 12905828f));
        targetsData.Add(new TargetData(5, "test5", 3516060f, 12905928f));
        targetsData.Add(new TargetData(6, "test6", 3516058f, 12906114f));
    }
    void TargetTestData()       // GPS�� ������ ��쿡 ���Ǵ� �ӽ� ������
    {
        targetsData.Add(new TargetData(1, "test1", 50f, 10f));
        targetsData.Add(new TargetData(2, "test2", 20f, 20f));
        targetsData.Add(new TargetData(3, "test3", 30f, 30f));
        targetsData.Add(new TargetData(4, "test4", 40f, 40f));
        targetsData.Add(new TargetData(5, "test5", 50f, 50f));
        targetsData.Add(new TargetData(6, "test6", 60f, 60f));
    }

    //public void TargetMoveTest()    //��� Ÿ���� ���� ���� �ʴ� 1�� ��, �Ʒ��� �ٲٴ� �׽�Ʈ �ڵ�
    //{
    //    addcooltime = true;

    //    for (int i = 0; i < targetsData.Count; i++)
    //    {
    //        TargetData buf = targetsData[i];

    //        if (!test) buf.lat += 0.1f;
    //        else buf.lat -= 0.1f;

    //        targetsData[i] = buf;
    //    }

    //    testcount += 0.1f;

    //    if (testcount > 50f)
    //    {
    //        test = !test;
    //        testcount = 0f;
    //    }

    //    addcooltime = false;
    //}
}

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

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

public class RaderView : MonoBehaviour
{
    List<TargetData> targetsData;   //모든 타겟 데이터를 실제로 보관하는 장소
    List<GameObject> targetsUI;     //레이더 안의 원과 삼각형 UI 개체 

    public GPS_Manager gpsManager;
    public float raderRange; //레이더 탐색 범위
    public float raderLat; //정수 데이터로 변환
    public float raderLon; //정수 데이터로 변환

    public RectTransform OutRangeUI; //레이더 외곽의 UI (N 표기)
    public RectTransform ContactRangeUI; //초록 레이더 그림

    public GameObject targetUI; //타겟 개체
    public Sprite targetCir;    //레이더 범위 안일 경우 대체되는 스프라이트
    public Sprite targetTri;    //레이더 범위 밖일 경우 대체되는 스프라이트
    public Text monster_text;   //몬스터 등장 시 변경되는 텍스트

    public float OutRangeSize;      //외곽 레이더 사이즈
    public float ContactRangeSize;  //초록 레이더 사이즈
    public float targetSize;        //타겟 사이즈
    public float arrowPadding;      //외곽 레이더와 Tri 타겟 간의 여백

    public bool test;
    public float testcount;

    // Start is called before the first frame update
    void Start()
    {
        OutRangeUI.sizeDelta = new Vector2(OutRangeSize, OutRangeSize);
        ContactRangeUI.sizeDelta = new Vector2(ContactRangeSize, ContactRangeSize);

        targetUI.GetComponent<RectTransform>().sizeDelta = new Vector2(targetSize, targetSize);

        targetsData = new List<TargetData>();
        targetsUI = new List<GameObject>();

        raderLat = 0;
        raderLon = 0;

        testcount = 0;

        //TargetDataUpdate();
        TargetTestData();
    }

    void TargetDataUpdate()     //GPS가 켜져 있을 경우에 사용되는 실제 데이터 예제 (1당 1m)
    {
        targetsData.Add(new TargetData(1, "test1", 3515905f, 12905995f));
        targetsData.Add(new TargetData(2, "test2", 3515998f, 12905955f));
        targetsData.Add(new TargetData(3, "test3", 3515938f, 12905794f));
        targetsData.Add(new TargetData(4, "test4", 3516042f, 12905828f));
        targetsData.Add(new TargetData(5, "test5", 3516060f, 12905928f));
        targetsData.Add(new TargetData(6, "test6", 3516058f, 12906114f));
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

    private void Update()
    {
        StartCoroutine(UIUpdateTick());
    }

    IEnumerator UIUpdateTick()
    {
        yield return new WaitForSeconds(1.0f);

        //if (gpsManager.latitude != 0 && gpsManager.longitude != 0)
        //{
        //    raderLat = gpsManager.latitude * 100000f;
        //    raderLon = gpsManager.longitude * 100000f;
        //}
        
        //GPS 연결 되어 있을 경우 위 코드 주석 해제 필수

        TargetUIUpdate();
        TargetUIPositionUpdate();
        TargetMoveTest();

    }

    void TargetUIUpdate() //targetsData와 UI의 갯수를 동기화하는 함수
    {
        if (targetsData.Count > targetsUI.Count) //데이터보다 UI가 적으면
        {
            for (int i = targetsUI.Count; i < targetsData.Count; i++)  //필요한 UI 추가
            {
                GameObject buf = Instantiate(targetUI);
                buf.transform.SetParent(OutRangeUI.transform, true);
                targetsUI.Add(buf);
            }
        }
        else if (targetsData.Count < targetsUI.Count)
        {
            for (int i = targetsData.Count; i < targetsUI.Count; i++)  //필요 없는 UI 삭제
            {
                Destroy(targetsUI[targetsUI.Count - 1]);
                targetsUI.RemoveAt(targetsUI.Count - 1);
            }
        }

    }

    void TargetUIPositionUpdate()   //타겟 UI의 위치를 GPS 회전에 맞춰 매 프레임 위치를 이동시키는 함수
    {
        int count = 0;
        int rangeInMonsterCount = 0;

        OutRangeUI.localRotation = Quaternion.Euler(0, 0, gpsManager.magneticHeading);      //레이더 바깥 부분 회전 (GPS와 동기화)
        ContactRangeUI.localRotation = Quaternion.Euler(0, 0, -gpsManager.magneticHeading); //레이더 안쪽 부분 역회전 (회전 상쇄)

        foreach (TargetData data in targetsData)    // 모든 타겟에 대하여 작업
        {
            float deltaLat = (data.lat - raderLat);
            float deltaLon = (data.lon - raderLon);
            float distance = Vector2.Distance(new Vector2(deltaLon, deltaLat), new Vector2(0, 0));

            if (distance <= raderRange) // 레이더 범위 안의 타겟은 원으로 변경하는 코드
            {
                targetsUI[count].GetComponent<Image>().sprite = targetCir;
                targetsUI[count].transform.GetChild(0).GetComponent<Text>().text = distance.ToString("N0") +"m";
                targetsUI[count].transform.GetChild(0).localPosition = Vector3.zero;
                targetsUI[count].transform.GetChild(0).localRotation = Quaternion.Euler(0, 0, -gpsManager.magneticHeading);
                targetsUI[count].transform.localPosition = new Vector3(deltaLon, deltaLat, 0) * ((ContactRangeSize - 50) / 2 / raderRange);
                targetsUI[count].transform.localRotation = Quaternion.Euler(0, 0, 0);
                rangeInMonsterCount++;
            }
            else // 레이더 범위 밖의 타겟은 화살표로 변경하는 코드
            {
                float rot = GetAngle(new Vector2(0, 0), new Vector2(deltaLon, deltaLat));
                targetsUI[count].GetComponent<Image>().sprite = targetTri;
                targetsUI[count].transform.GetChild(0).GetComponent<Text>().text = distance.ToString("N0") + "m";
                targetsUI[count].transform.GetChild(0).localPosition = Vector3.up * 50;
                targetsUI[count].transform.localPosition = (new Vector3(deltaLon, deltaLat, 0).normalized) * (((OutRangeSize + targetSize) / 2) + arrowPadding);
                targetsUI[count].transform.localRotation = Quaternion.Euler(0, 0, -rot);
                //모든 화살표는 중심에서 정해진 거리에 위치 (단위 벡터 * 거리)
                //화살표 방향을 벡터 방향으로 회전
            }
            count = count + 1;
        }

        if (rangeInMonsterCount > 0)    //레이더 범위 안에 몬스터가 존재하는 경우 작동하는 코드
        {
            monster_text.text = "Monster!!!!";
        }
        else
        {
            monster_text.text = "";
        }
    }

    float GetAngle(Vector2 start, Vector2 end)      //수학 회전 각도 수치를 유니티의 회전 각도로 변형하는 코드
    {
        Vector2 v2 = end - start;
        return (450.0f - Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg) % 360.0f;
    }

    public void TargetMoveTest()    //모든 타겟의 위도 값을 초당 1씩 위, 아래로 바꾸는 테스트 코드
    {
        for(int i = 0; i < targetsData.Count; i++)
        {
            TargetData buf = targetsData[i];
            
            if(!test) buf.lat += 0.1f;
            else     buf.lat -= 0.1f;

            targetsData[i] = buf;
        }

        testcount += 0.1f;

        if (testcount > 100f)
        {
            test = !test;
            testcount = 0f;
        }
    }
}

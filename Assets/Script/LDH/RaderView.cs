using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class RaderView : MonoBehaviour
{
    public TargetDataList dataList;
    private List<GameObject> targetsUI;     //레이더 안의 원과 삼각형 UI 개체 

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
    public int rangeInMonsterCount;


    // Start is called before the first frame update
    void Start()
    {
        OutRangeUI.sizeDelta = new Vector2(OutRangeSize, OutRangeSize);
        ContactRangeUI.sizeDelta = new Vector2(ContactRangeSize, ContactRangeSize);

        targetUI.GetComponent<RectTransform>().sizeDelta = new Vector2(targetSize, targetSize);
        targetsUI = new List<GameObject>();

        raderLat = 0;
        raderLon = 0;
    }

    private void Update()
    {
        StartCoroutine(UIUpdateTick());
    }

    IEnumerator UIUpdateTick()
    {
        yield return new WaitForSeconds(1.0f);

        if (gpsManager.receiveGPS)
        {
            raderLat = gpsManager.latitude * 100000f;
            raderLon = gpsManager.longitude * 100000f;
        }


        TargetUIUpdate();
        TargetUIPositionUpdate();
    }

    void TargetUIUpdate() //targetsData와 UI의 갯수를 동기화하는 함수
    {
        if (dataList.targetsData.Count > targetsUI.Count) //데이터보다 UI가 적으면
        {
            for (int i = targetsUI.Count; i < dataList.targetsData.Count; i++)  //필요한 UI 추가
            {
                GameObject buf = Instantiate(targetUI);
                buf.transform.SetParent(OutRangeUI.transform, true);
                targetsUI.Add(buf);
            }
        }
        else if (dataList.targetsData.Count < targetsUI.Count)
        {
            for (int i = dataList.targetsData.Count; i < targetsUI.Count; i++)  //필요 없는 UI 삭제
            {
                Destroy(targetsUI[targetsUI.Count - 1]);
                targetsUI.RemoveAt(targetsUI.Count - 1);
            }
        }
    }

    void TargetUIPositionUpdate()   //타겟 UI의 위치를 GPS 회전에 맞춰 매 프레임 위치를 이동시키는 함수
    { 
        rangeInMonsterCount = 0;

        OutRangeUI.localRotation = Quaternion.Euler(0, 0, gpsManager.magneticHeading);      //레이더 바깥 부분 회전 (GPS와 동기화)
        ContactRangeUI.localRotation = Quaternion.Euler(0, 0, -gpsManager.magneticHeading); //레이더 안쪽 부분 역회전 (회전 상쇄)

        int count = 0;

        foreach (TargetData data in dataList.targetsData)    // 모든 타겟에 대하여 작업
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
                targetsUI[count].transform.GetChild(0).localRotation = Quaternion.Euler(0, 0, 0);
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

}

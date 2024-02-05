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
    private List<GameObject> targetsUI;     //���̴� ���� ���� �ﰢ�� UI ��ü 

    public GPS_Manager gpsManager;
    public float raderRange; //���̴� Ž�� ����
    public float raderLat; //���� �����ͷ� ��ȯ
    public float raderLon; //���� �����ͷ� ��ȯ

    public RectTransform OutRangeUI; //���̴� �ܰ��� UI (N ǥ��)
    public RectTransform ContactRangeUI; //�ʷ� ���̴� �׸�

    public GameObject targetUI; //Ÿ�� ��ü
    public Sprite targetCir;    //���̴� ���� ���� ��� ��ü�Ǵ� ��������Ʈ
    public Sprite targetTri;    //���̴� ���� ���� ��� ��ü�Ǵ� ��������Ʈ
    public Text monster_text;   //���� ���� �� ����Ǵ� �ؽ�Ʈ

    public float OutRangeSize;      //�ܰ� ���̴� ������
    public float ContactRangeSize;  //�ʷ� ���̴� ������
    public float targetSize;        //Ÿ�� ������
    public float arrowPadding;      //�ܰ� ���̴��� Tri Ÿ�� ���� ����
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

    void TargetUIUpdate() //targetsData�� UI�� ������ ����ȭ�ϴ� �Լ�
    {
        if (dataList.targetsData.Count > targetsUI.Count) //�����ͺ��� UI�� ������
        {
            for (int i = targetsUI.Count; i < dataList.targetsData.Count; i++)  //�ʿ��� UI �߰�
            {
                GameObject buf = Instantiate(targetUI);
                buf.transform.SetParent(OutRangeUI.transform, true);
                targetsUI.Add(buf);
            }
        }
        else if (dataList.targetsData.Count < targetsUI.Count)
        {
            for (int i = dataList.targetsData.Count; i < targetsUI.Count; i++)  //�ʿ� ���� UI ����
            {
                Destroy(targetsUI[targetsUI.Count - 1]);
                targetsUI.RemoveAt(targetsUI.Count - 1);
            }
        }
    }

    void TargetUIPositionUpdate()   //Ÿ�� UI�� ��ġ�� GPS ȸ���� ���� �� ������ ��ġ�� �̵���Ű�� �Լ�
    { 
        rangeInMonsterCount = 0;

        OutRangeUI.localRotation = Quaternion.Euler(0, 0, gpsManager.magneticHeading);      //���̴� �ٱ� �κ� ȸ�� (GPS�� ����ȭ)
        ContactRangeUI.localRotation = Quaternion.Euler(0, 0, -gpsManager.magneticHeading); //���̴� ���� �κ� ��ȸ�� (ȸ�� ���)

        int count = 0;

        foreach (TargetData data in dataList.targetsData)    // ��� Ÿ�ٿ� ���Ͽ� �۾�
        {
            float deltaLat = (data.lat - raderLat);
            float deltaLon = (data.lon - raderLon);
            float distance = Vector2.Distance(new Vector2(deltaLon, deltaLat), new Vector2(0, 0));

            if (distance <= raderRange) // ���̴� ���� ���� Ÿ���� ������ �����ϴ� �ڵ�
            {
                targetsUI[count].GetComponent<Image>().sprite = targetCir;
                targetsUI[count].transform.GetChild(0).GetComponent<Text>().text = distance.ToString("N0") +"m";
                targetsUI[count].transform.GetChild(0).localPosition = Vector3.zero;
                targetsUI[count].transform.GetChild(0).localRotation = Quaternion.Euler(0, 0, -gpsManager.magneticHeading);
                targetsUI[count].transform.localPosition = new Vector3(deltaLon, deltaLat, 0) * ((ContactRangeSize - 50) / 2 / raderRange);
                targetsUI[count].transform.localRotation = Quaternion.Euler(0, 0, 0);
                rangeInMonsterCount++;
            }
            else // ���̴� ���� ���� Ÿ���� ȭ��ǥ�� �����ϴ� �ڵ�
            {
                float rot = GetAngle(new Vector2(0, 0), new Vector2(deltaLon, deltaLat));
                targetsUI[count].GetComponent<Image>().sprite = targetTri;
                targetsUI[count].transform.GetChild(0).GetComponent<Text>().text = distance.ToString("N0") + "m";
                targetsUI[count].transform.GetChild(0).localPosition = Vector3.up * 50;
                targetsUI[count].transform.GetChild(0).localRotation = Quaternion.Euler(0, 0, 0);
                targetsUI[count].transform.localPosition = (new Vector3(deltaLon, deltaLat, 0).normalized) * (((OutRangeSize + targetSize) / 2) + arrowPadding);
                targetsUI[count].transform.localRotation = Quaternion.Euler(0, 0, -rot);
                //��� ȭ��ǥ�� �߽ɿ��� ������ �Ÿ��� ��ġ (���� ���� * �Ÿ�)
                //ȭ��ǥ ������ ���� �������� ȸ��
            }
            count = count + 1;
        }

        if (rangeInMonsterCount > 0)    //���̴� ���� �ȿ� ���Ͱ� �����ϴ� ��� �۵��ϴ� �ڵ�
        {
            monster_text.text = "Monster!!!!";
        }
        else
        {
            monster_text.text = "";
        }
    }

    float GetAngle(Vector2 start, Vector2 end)      //���� ȸ�� ���� ��ġ�� ����Ƽ�� ȸ�� ������ �����ϴ� �ڵ�
    {
        Vector2 v2 = end - start;
        return (450.0f - Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg) % 360.0f;
    }

}

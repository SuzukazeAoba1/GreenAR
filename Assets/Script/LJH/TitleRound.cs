using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleRound : MonoBehaviour
{
    public RectTransform myTR;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        myTR = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        RoundTitle();
    }

    public void RoundTitle()
    {
        float currentRotation = transform.eulerAngles.z;

        // �ݽð� �������� ȸ�� �ӵ��� �����Ͽ� ���ο� ������ ����մϴ�.
        float newRotation = currentRotation + speed * Time.deltaTime;

        myTR.rotation = Quaternion.Euler(new Vector3(0, 0, newRotation));
    }
}

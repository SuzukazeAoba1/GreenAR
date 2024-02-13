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

        // 반시계 방향으로 회전 속도를 적용하여 새로운 각도를 계산합니다.
        float newRotation = currentRotation + speed * Time.deltaTime;

        myTR.rotation = Quaternion.Euler(new Vector3(0, 0, newRotation));
    }
}

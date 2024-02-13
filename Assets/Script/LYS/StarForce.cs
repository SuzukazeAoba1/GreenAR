using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class StarForce : MonoBehaviour
{
    public Image starForceBG;
    public Image starForceBar;
    public float barMoveSpeed = 300f;
    private float direction = 1f;

    void Start()
    {

    }

    void Update()
    {
        MoveStarForceBar();
  }

    void MoveStarForceBar() // ��Ÿ���� ������
    {
        Vector3 currentPosition = starForceBar.rectTransform.position;

        // �¿�� �̵�
        currentPosition.x += barMoveSpeed * direction * Time.deltaTime;
        currentPosition.y = starForceBG.rectTransform.position.y;

        // ���� ������ ���ٸ� ������ ���������� ��ȯ
        if (currentPosition.x < starForceBG.rectTransform.position.x - starForceBG.rectTransform.rect.width * 0.5f)
        {
            currentPosition.x = starForceBG.rectTransform.position.x - starForceBG.rectTransform.rect.width * 0.5f;
            direction = 1f;
        }
        // ������ ������ ���ٸ� ������ �������� ��ȯ
        else if (currentPosition.x > starForceBG.rectTransform.position.x + starForceBG.rectTransform.rect.width * 0.5f)
        {
            currentPosition.x = starForceBG.rectTransform.position.x + starForceBG.rectTransform.rect.width * 0.5f;
            direction = -1f;
        }

        starForceBar.rectTransform.position = currentPosition;


    }
}

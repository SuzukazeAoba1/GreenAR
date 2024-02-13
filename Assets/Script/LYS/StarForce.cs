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

    void MoveStarForceBar() // 스타포스 움직임
    {
        Vector3 currentPosition = starForceBar.rectTransform.position;

        // 좌우로 이동
        currentPosition.x += barMoveSpeed * direction * Time.deltaTime;
        currentPosition.y = starForceBG.rectTransform.position.y;

        // 왼쪽 끝으로 갔다면 방향을 오른쪽으로 전환
        if (currentPosition.x < starForceBG.rectTransform.position.x - starForceBG.rectTransform.rect.width * 0.5f)
        {
            currentPosition.x = starForceBG.rectTransform.position.x - starForceBG.rectTransform.rect.width * 0.5f;
            direction = 1f;
        }
        // 오른쪽 끝으로 갔다면 방향을 왼쪽으로 전환
        else if (currentPosition.x > starForceBG.rectTransform.position.x + starForceBG.rectTransform.rect.width * 0.5f)
        {
            currentPosition.x = starForceBG.rectTransform.position.x + starForceBG.rectTransform.rect.width * 0.5f;
            direction = -1f;
        }

        starForceBar.rectTransform.position = currentPosition;


    }
}

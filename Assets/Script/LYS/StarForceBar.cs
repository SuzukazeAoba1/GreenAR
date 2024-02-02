
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StarForceBar : MonoBehaviour, IPointerClickHandler {
    public Image starForceBar;
    public Image starForceHighlight;

    Enemy enemy;

    void Start() {

    }

    void Update() {

    }

    public void OnPointerClick(PointerEventData eventData) { // Ŭ�� �Լ�
        Enemy enemy = FindObjectOfType<Enemy>();

        if(enemy != null) {
            if(BarInHighlight()) {
                enemy.TakeDamage(15);
            } else {
                enemy.TakeDamage(3);
            }
            enemy.EnemyName.text = enemy.currentEnemyName.ToString() + "\n���� ü��: " + enemy.currentHealth;
            enemy.StartCoroutine("ShowAttackText");
        }
    }

    bool BarInHighlight() // Bar�� Highlight ���� ���� ������ true
    {
        RectTransform barRect = starForceBar.rectTransform;
        RectTransform highlightRect = starForceHighlight.rectTransform;

        float barLeftX = barRect.position.x - barRect.rect.width * 0.5f;
        float barRightX = barRect.position.x + barRect.rect.width * 0.5f;

        float highlightLeftX = highlightRect.position.x - highlightRect.rect.width * 0.5f;
        float highlightRightX = highlightRect.position.x + highlightRect.rect.width * 0.5f;

        return (barRightX >= highlightLeftX && barLeftX <= highlightRightX);
    }
}

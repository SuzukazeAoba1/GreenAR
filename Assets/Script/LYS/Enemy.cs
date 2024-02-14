using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum EnemyName {
    Dragon, 슬라임캣, EnemyC
}

public class Enemy : MonoBehaviour
{
    public int maxHealth = 200;
    public int currentHealth;
    public bool canTakeDamage = true; // 광클 방지용

    public GameObject hitEffect;

    public Text AttackMessage;
    public Text EnemyName;
    public RectTransform victoryUI;

    public EnemyName currentEnemyType;

    Animator anim;


    private void Start() {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    private void Update() {
        EnemyName.text = currentEnemyType.ToString() + "\n현재 체력: " + currentHealth;

        if(Input.GetMouseButtonDown(0) || Input.touchCount > 0) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject && canTakeDamage) {
                TakeDamage(10);
                StartCoroutine(ShowAttackText());
                StartCoroutine(TakeDamageTime(1f));
            }
        }
        Die();
    }

    public IEnumerator ShowAttackText() {
        AttackMessage.gameObject.SetActive(true);
        anim.SetBool("isDamage", true);
        yield return new WaitForSeconds(1f);
        AttackMessage.gameObject.SetActive(false);
        anim.SetBool("isDamage", false);
    }

    public void TakeDamage(int damageValue) {
        currentHealth -= damageValue;
        Debug.Log("현재 체력: " + currentHealth);

        Vector3 effectPosition = new Vector3(gameObject.transform.position.x, 1.5f, gameObject.transform.position.z);
        Instantiate(hitEffect, effectPosition, Camera.main.transform.rotation);
    }

    public IEnumerator TakeDamageTime(float seconds) {
        canTakeDamage = false;
        yield return new WaitForSeconds(seconds);
        canTakeDamage = true;
    }

    public void Die() {
        if(currentHealth <= 0) {
            Destroy(gameObject);
            EnemyName.text = "";
            AttackMessage.gameObject.SetActive(false);

            // 처치하면 승리UI
            // victoryUI.gameObject.SetActive(true);
            //Time.timeScale = 0f;
            SceneManager.LoadScene("MainScene");
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum EnemyName {
    EnemyA, EnemyB, EnemyC
}

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public GameObject hitEffect;

    public Text AttackMessage;
    public Text EnemyName;

    private void Start() {
        currentHealth = maxHealth;
    }

    private void Update() {
        EnemyName.text = currentEnemyName.ToString() + "\n현재 체력: " + currentHealth;

        if(Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject) {
                TakeDamage(10);
                StartCoroutine(ShowAttackText());
            }
        }
        Die();
    }

    public IEnumerator ShowAttackText() {
        AttackMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        AttackMessage.gameObject.SetActive(false);
    }

    public void TakeDamage(int damageValue) {
        currentHealth -= damageValue;
        Debug.Log("현재 체력: " + currentHealth);

        //Vector3 effectPosition = new Vector3(gameObject.transform.position.x, 1.5f, gameObject.transform.position.z);
        Instantiate(hitEffect, gameObject.transform.position, Camera.main.transform.rotation);
    }

    public void Die() {
        if(currentHealth <= 0) {
            Destroy(gameObject);
            EnemyName.text = "";
            AttackMessage.gameObject.SetActive(false);
            Debug.Log("사망");
            SceneManager.LoadScene("ResultScene");
        }
    }

    public EnemyName currentEnemyName;
}

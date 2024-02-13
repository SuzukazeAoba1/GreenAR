using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Missile : MonoBehaviour {
    public float missileHP = 20;
    public float currentHP;
    public GameObject missilePrefab;
    public float missileSpeed = 5f;
    public Image DragZone;
    private GameObject missile;

    void Start() {
        currentHP = missileHP;
        InvokeRepeating("ShootMissile", 0f, 8f);
    }

    void Update() {
        if(missile == null){
            DragZone.gameObject.SetActive(false);
        }
    }

    void ShootMissile() {
        missile = Instantiate(missilePrefab, transform.position, transform.rotation);
        currentHP = 50;
        Rigidbody missileRb = missile.GetComponent<Rigidbody>();
        missileRb.velocity = transform.forward * missileSpeed;

        DragZone.gameObject.SetActive(true);
        Destroy(missile, 5f);
    }

    public void TakeDamage(float damageValue) {
        currentHP -= damageValue;

        if(currentHP <= 0) {
            Destroy(missile);
        }
    }
}

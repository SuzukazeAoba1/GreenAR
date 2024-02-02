using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAttack : MonoBehaviour, IDragHandler
{
    private Missile missile;

    void Start() {
    }

    void Update() {
        
    }

    public void OnDrag(PointerEventData eventData) {
        missile = FindObjectOfType<Missile>();
        missile.TakeDamage(1);
    }
}

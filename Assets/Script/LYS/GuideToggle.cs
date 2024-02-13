using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GuideToggle : MonoBehaviour, IPointerClickHandler {
    public Text guideText; 
    private bool guide = true;

    public void OnPointerClick(PointerEventData eventData) {
        guide = !guide;
        guideText.gameObject.SetActive(guide);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

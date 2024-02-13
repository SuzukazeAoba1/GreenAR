using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class FrameColor : MonoBehaviour {
    [SerializeField]TextMeshProUGUI text;
    [SerializeField]Image neon;
    [Range(0, 255)]
    public int R,G,B;
    public bool sameColor = true;
    public bool rainbow = false;
    [Range(0.001f, 0.01f)]
    public float time;

    bool flag = false;

	private void Update() {

        if (!sameColor)
            text.color = Color.white;
        else
            text.color = new Color(R / 255f, G / 255f, B / 255f);

        if (rainbow)
            StartCoroutine(DoRainbow());
        else
            flag = false;
        neon.color = new Color(R / 255f, G / 255f, B / 255f);
    }

    IEnumerator DoRainbow() {
		if (!flag) {
            flag = true;

            R = 50; G = 130; B = 255;

            for (int i = 0; i < 120; i++) {
                G++;
                if (!rainbow)
                    break;
                yield return new WaitForSeconds(time);
            }

            for (int i = 0; i < 200; i++) {
                B--;
                if (!rainbow)
                    break;
                yield return new WaitForSeconds(time);
            }

            for (int i = 0; i < 120; i++) {
                G--;
                if (!rainbow)
                    break;
                yield return new WaitForSeconds(time);
            }

            for (int i = 0; i < 200; i++) {
                B++;
                if (!rainbow)
                    break;
                yield return new WaitForSeconds(time);
            }

            flag = false;
        }
	}
}

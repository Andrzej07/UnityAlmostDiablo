using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTextController : MonoBehaviour {

    public bool fade = true;
    public Transform attachTo;
    public float timeToLive = 2;
    public int value;
    public Sprite[] numberSprites;
    public Color color = Color.white;

    private float numberWidth = 0.5f;
    private float startYOffset = 2;
    private float endYOffset = 4;
    private float timeAlive = 0;
    private float xOffset;
	// Use this for initialization
	void Start () {
        int remainder = value;
        int count = 0;
        while(remainder > 0)
        {
            int currentVal = remainder % 10;
            remainder /= 10;
            GameObject number = new GameObject("CombatTextNumber");
            number.transform.parent = transform;
            number.transform.localScale = Vector3.one;
            number.AddComponent<SpriteRenderer>();
            SpriteRenderer spriteRenderer = number.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = numberSprites[currentVal];
            spriteRenderer.color = color;
            number.transform.localPosition = new Vector3(-numberWidth * count, 0, 0);
            count++;
            if(fade)
                StartCoroutine(FadeOutRoutine(spriteRenderer, color)); 
        }
        xOffset = (count-1) * numberWidth / 2 * transform.localScale.x;       
    }
	
	// Update is called once per frame
	void Update () {
        timeAlive += Time.deltaTime;
        Vector3 position = attachTo.position;
        position.y += startYOffset + (endYOffset - startYOffset) * timeAlive / timeToLive;
        position.x += xOffset;
        transform.position = position;
        transform.rotation = Camera.main.transform.rotation;
	}

    IEnumerator FadeOutRoutine(SpriteRenderer spriteRenderer, Color color)
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / timeToLive)
        {
            Color newColor = new Color(color.r, color.g, color.b, Mathf.Lerp(1, 0, t));
            spriteRenderer.color = newColor;
            yield return null;
        }
    }
}

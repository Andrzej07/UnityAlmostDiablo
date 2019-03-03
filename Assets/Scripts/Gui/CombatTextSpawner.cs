using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTextSpawner : MonoBehaviour {
    public bool fade = true;
    public float timeToLive = 2;
    public Vector3 textScale = new Vector3(0.5f, 0.5f, 0.5f);
    public Sprite[] numberSprites;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnText(Transform attachTo, int value)
    {
        SpawnText(attachTo, value, Color.white);
    }

    public void SpawnText(Transform attachTo, int value, Color color)
    {
        GameObject text = new GameObject("CombatTextInstance");
        text.transform.parent = transform;
        text.transform.localScale = textScale;
        CombatTextController combatTextController = text.AddComponent<CombatTextController>();
        combatTextController.numberSprites = numberSprites;
        combatTextController.timeToLive = timeToLive;
        combatTextController.value = value;
        combatTextController.attachTo = attachTo;
        combatTextController.color = color;
        combatTextController.fade = fade;
        Destroy(text, timeToLive);
    }
}

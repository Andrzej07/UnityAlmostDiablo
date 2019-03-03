using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraController : MonoBehaviour {

    public GameObject player;


    public float x = 0;
    public float y = 55;
    public float distance = 10;
    public float maxDistance = 20;
    public float minDistance = 10;
    
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void LateUpdate () {
        distance -= Input.mouseScrollDelta.y;
        if (distance > maxDistance)
            distance = maxDistance;
        else if (distance < minDistance)
            distance = minDistance;
        if(Input.GetKey(KeyCode.Q))
        {
            x = x - 1;
        }
        if(Input.GetKey(KeyCode.E))
        {
            x = x + 1;
        }
        Vector3 rotation = new Vector3(y, x, 0);
        transform.rotation = Quaternion.Euler(rotation);
        transform.position = transform.rotation * new Vector3(0, 0, -distance) + player.transform.position;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public Color color = Color.white;
    public float floatMagnitude = 0.25f;
    public float floatSpeed = 0.1f;
    public Effect pickupEffect;

    private float currentFloatOffset = 0;
    private Vector3 originalPosition;

    private void Awake()
    {
        GetComponent<Renderer>().material.color = color;
    }

    void Start()
    {
        originalPosition = transform.position;
        originalPosition.y = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        currentFloatOffset += Time.deltaTime * floatSpeed;
        if (Mathf.Abs(currentFloatOffset) > floatMagnitude)
        {
            floatSpeed *= -1;
        }
        Vector3 newPosition = originalPosition;
        newPosition.y += currentFloatOffset;
        transform.position = newPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pickupEffect.ApplyEffect(gameObject, other.gameObject);
            Destroy(gameObject);
        }
    }
}

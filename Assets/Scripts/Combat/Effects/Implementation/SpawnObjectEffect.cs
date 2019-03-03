using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/Spawn Object")]
public class SpawnObjectEffect : Effect
{
    public GameObject objectToSpawn;
    public float timeToLive;

    private GameObject instance;
    private GameObject parent;

    void OnEnable()
    {
        parent = GameObject.Find("SpawnedObjects");
        if (parent == null)
            parent = new GameObject("SpawnedObjects");
    }

    public override void ApplyEffect(GameObject source, GameObject target)
    {
        instance = Instantiate(objectToSpawn, parent.transform);
        instance.transform.position = target.transform.position;
        GameController.instance.StartCoroutine(DestroyAfterDelay());
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(timeToLive);
        ObjectDestructionController.Destroy2(instance);
    }
}

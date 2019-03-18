using System.Collections;
using UnityEngine;

namespace Demo.Abilities
{
    [RequireComponent(typeof(IAbilityTrigger))]
    public class DeactivateAfterCast : MonoBehaviour, IPostCastAction
    {
        public float secondsDelay = 0;

        IEnumerator Deactivate()
        {
            yield return new WaitForSeconds(secondsDelay);
            gameObject.SetActive(false);
        }

        public void Perform()
        {
            StartCoroutine(Deactivate());
        }
    }
}
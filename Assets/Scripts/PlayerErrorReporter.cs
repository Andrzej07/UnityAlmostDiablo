using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerErrorReporter : MonoBehaviour, IErrorReporter
{
    public Text errorLabel;
    private const float errorTimeout = 2;
    Coroutine coroutine;
    public void ReportError(string msg)
    {
        errorLabel.text = msg;
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(HideAfterDelay(errorTimeout));
    }

    IEnumerator HideAfterDelay(float delay)
    {
        Debug.Log("Started coroutine");
        yield return new WaitForSeconds(delay);
        Debug.Log("Hiding text");
        errorLabel.text = null;
    }

}
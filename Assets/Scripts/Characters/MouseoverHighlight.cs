using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseoverHighlight : MonoBehaviour
{
    private void OnMouseOver()
    {
        GetComponentInChildren<Renderer>().material.SetFloat("_Outline", 0.1f);
    }

    private void OnMouseExit()
    {
        GetComponentInChildren<Renderer>().material.SetFloat("_Outline", 0);
    }
}

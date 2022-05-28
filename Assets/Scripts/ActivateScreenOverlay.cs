using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateScreenOverlay : MonoBehaviour
{
    public GameObject _screenOverlay;
    void OnEnable()
    {
        _screenOverlay.SetActive(true);
    }
}

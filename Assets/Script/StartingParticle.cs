using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingParticle : MonoBehaviour
{
    void Start()
    {
        Invoke("DisableGameObject", 2f);
    }

    void DisableGameObject()
    {
        gameObject.SetActive(false);
    }


}

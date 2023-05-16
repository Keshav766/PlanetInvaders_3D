using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDistruct : MonoBehaviour
{
    [SerializeField] float timeTillDestroy = 3f;
    void Start()
    {
        Destroy(gameObject, timeTillDestroy);
    }
}

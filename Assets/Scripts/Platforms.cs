using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Platform", menuName = "Platforms", order = 1)]
public class Platforms : ScriptableObject
{
    [SerializeField]
    public float time;
    [SerializeField]
    public GameObject platform;

    public float Time
    {
        get
        {
            return time;
        }
    }

    public GameObject Platform
    {
        get
        {
            return platform;
        }
    }
}

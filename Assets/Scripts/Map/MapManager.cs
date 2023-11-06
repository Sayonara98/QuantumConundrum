using System;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    public Dictionary<int, Dictionary<int, string>> MapData = new();

    private void Awake()
    {
        Instance = this;
    }
}
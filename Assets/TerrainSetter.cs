using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSetter : MonoBehaviour
{
    void Start()
    {
        Terrain.activeTerrain.detailObjectDistance = 750;
    }

}

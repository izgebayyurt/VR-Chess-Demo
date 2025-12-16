using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPivot : MonoBehaviour
{
    BoardCreate3D bc;

    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponentInChildren<BoardCreate3D>();
        float x = bc.width* bc.scalar * bc.x_tile_count;
        float y = bc.height * bc.scalar * bc.y_tile_count;
        float z = bc.depth * bc.scalar * bc.z_tile_count;

        transform.GetChild(0).position = new Vector3(-x / 2, -y / 2, -z / 2);
    }

}

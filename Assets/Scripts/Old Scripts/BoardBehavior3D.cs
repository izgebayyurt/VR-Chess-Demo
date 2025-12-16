using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBehavior3D : MonoBehaviour
{

    BoardCreate3D bc;

    // Global bool for moving a piece
    public bool isMoving;

    [HideInInspector]
    public float x_increments, y_increments, z_increments;

    void Start()
    {
        bc = GetComponent<BoardCreate3D>();
        x_increments = bc.width * bc.scalar;
        y_increments = bc.height * bc.scalar;
        z_increments = bc.depth * bc.scalar;
    }

}

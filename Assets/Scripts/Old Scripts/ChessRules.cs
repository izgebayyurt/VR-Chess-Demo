using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessRules : MonoBehaviour
{
    GameObject[,,] pieceArray;

    // Start is called before the first frame update
    void Start()
    {
        pieceArray = GetComponent<BoardCreate3D>().pieceArray;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

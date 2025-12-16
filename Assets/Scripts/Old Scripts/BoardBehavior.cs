using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBehavior : MonoBehaviour
{
    private BoardCreate3D boardCreate3D;
    private TileBehavior tileBehavior;
    private GameObject[,,] board;

    private int currentLayer;
    private int lastLayer;

    // Assigns a different color to each layer
    public bool changeColor;

    // Start is called before the first frame update
    void Start()
    {
        boardCreate3D = GetComponent<BoardCreate3D>();
        tileBehavior = transform.GetChild(0).GetComponent<TileBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        board = boardCreate3D.board;

        lastLayer = UpdateTransparency(currentLayer);
    }

    int UpdateTransparency(int currentLayer)
    {
        if (tileBehavior.side == TileBehavior.sides.back || tileBehavior.side == TileBehavior.sides.front)
        {
            for (int i = 0; i < boardCreate3D.x_tile_count; i++)
            {
                for (int j = 0; j < boardCreate3D.y_tile_count; j++)
                {
                    board[i, j, currentLayer].transform.GetChild((int)tileBehavior.side).GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.25f);
                }
            }
        }
        else if (tileBehavior.side == TileBehavior.sides.right || tileBehavior.side == TileBehavior.sides.left)
        {
            for (int i = 0; i < boardCreate3D.x_tile_count; i++)
            {
                for (int j = 0; j < boardCreate3D.z_tile_count; j++)
                {
                    board[currentLayer, i, j].transform.GetChild((int)tileBehavior.side).GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.25f);
                }
            }
        }
        else
        {
            for (int i = 0; i < boardCreate3D.y_tile_count; i++)
            {
                for (int j = 0; j < boardCreate3D.z_tile_count; j++)
                {
                    board[i, currentLayer, j].transform.GetChild((int)tileBehavior.side).GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.25f);
                }
            }
        }

        return currentLayer;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCreate3D : MonoBehaviour
{

    // Tiles (Cubes)
    public GameObject dark_tile;
    public GameObject light_tile;

    [HideInInspector]
    public float width, height, depth;

    // xyz
    public int x_tile_count;
    public int y_tile_count;
    public int z_tile_count;

    // scale multiplier (put in the measurement of the model in blender)
    public float scalar = 4.9f;
    public float boardScale = .15f;
    public float tileScale = .2f;
    public float pieceScale = .2f;

    // Store the tiles in this array
    public GameObject[,,] board;

    // Store the pieces in this array
    public GameObject[,,] pieceArray;

    void Awake()
    {
        // Get the dimensions of the cube (it will be the same since its a cube)
        width = dark_tile.transform.localScale.x;
        height = dark_tile.transform.localScale.y;
        depth = dark_tile.transform.localScale.z;

        // Initialize the board array
        board = new GameObject[x_tile_count, y_tile_count, z_tile_count];
        pieceArray = new GameObject[x_tile_count, y_tile_count, z_tile_count];

        PlaceTiles();

    }

    void Start()
    {
        // this.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
        // this.transform.position = new Vector3(-10f, 20f, -7f);
    }

    // Creates the checkered tile grid by given x * y * z
    void PlaceTiles()
    {

        for (int i = 0; i < x_tile_count; i++)
        {
            for (int j = 0; j < y_tile_count; j++)
            {
                for (int k = 0; k < z_tile_count; k++)
                {
                    if ((i + j + k) % 2 == 0)
                    {
                        board[i, j, k] = Instantiate(light_tile, new Vector3(width * i, height * j, depth * k) * scalar*boardScale, Quaternion.identity);
                    }
                    else
                    {
                        board[i, j, k] = Instantiate(dark_tile, new Vector3(width * i, height * j, depth * k) * scalar*boardScale, Quaternion.identity);
                    }

                    board[i, j, k].transform.parent = gameObject.transform;
                    board[i,j,k].transform.localScale = Vector3.one*(boardScale*tileScale);
                }
            }
        }
    }

}

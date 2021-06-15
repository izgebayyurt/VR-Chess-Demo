﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCreate3D : MonoBehaviour
{

    // Tiles (Cubes)
    public GameObject dark_cube;
    public GameObject light_cube;

    float width;
    float height;
    float depth; 

    // xyz 
    public int x_tile_count;
    public int y_tile_count;
    public int z_tile_count;

    // scale multiplier (put in the measurement of the model in blender)
    public float scalar = 4.9f;

    // The board array
    public GameObject[,,] board;

    void Awake()
    {
        // Get the dimensions of the cube (it will be the same since its a cube)
        width = dark_cube.transform.localScale.x;
        height = dark_cube.transform.localScale.y;
        depth = dark_cube.transform.localScale.z;

        // Initialize the board array
        board = new GameObject[x_tile_count, y_tile_count, z_tile_count];

        PlaceTiles();

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
                        board[i, j, k] = Instantiate(light_cube, new Vector3(width * i, height * j, depth * k) * scalar, Quaternion.identity);
                    }
                    else
                    {
                        board[i, j, k] = Instantiate(dark_cube, new Vector3(width * i, height * j, depth * k) * scalar, Quaternion.identity);
                    }

                    board[i, j, k].transform.parent = gameObject.transform;
                    board[i, j, k].AddComponent<TileBehavior>();
                }
            }
        }
    }

}

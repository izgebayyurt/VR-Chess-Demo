using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCreate : MonoBehaviour
{

    // Tiles
    public GameObject dark_tile;
    public GameObject light_tile;
    float width;
    float height;

    // N x M board 
    public int horizontal_tile_count;
    public int vertical_tile_count;

    // The board array
    public GameObject[,] board;

    // Pieces
    public GameObject wPawn;
    public GameObject bPawn;
    public GameObject wKnight;
    public GameObject bKnight;
    public GameObject wBishop;
    public GameObject bBishop;
    public GameObject wRook;
    public GameObject bRook;
    public GameObject wQueen;
    public GameObject bQueen;
    public GameObject wKing;
    public GameObject bKing;

    // Place the pieces or not
    public bool placePieces;

    // Start is called before the first frame update
    void Awake()
    {
        width = dark_tile.transform.localScale.x;
        height = dark_tile.transform.localScale.y;
        board = new GameObject[horizontal_tile_count, vertical_tile_count];

        PlaceTiles();

        if (placePieces) { PlacePieces(); }
    }

    void PlaceTiles()
    {

        for(int i = 0; i < horizontal_tile_count; i++)
        {
            for (int j = 0; j < vertical_tile_count; j++)
            {
                if((i + j) % 2 == 0)
                {
                    board[i, j] = Instantiate(light_tile, new Vector3(width * j, 0, width * i), Quaternion.identity);
                    board[i, j].transform.parent = gameObject.transform;
                }
                else
                {
                    board[i, j] = Instantiate(dark_tile, new Vector3(width * j, 0, width * i), Quaternion.identity);
                    board[i, j].transform.parent = gameObject.transform;
                }
            }
        }
    }

    void PlacePieces()
    {   
        // Pawns
        for(int i = 0; i < vertical_tile_count; i++)
        {
            Instantiate(wPawn, board[i,1].transform.position, Quaternion.identity).transform.parent = gameObject.transform;
        }
        for (int i = 0; i < vertical_tile_count; i++)
        {
            Instantiate(bPawn, board[i, 6].transform.position, Quaternion.identity).transform.parent = gameObject.transform;
        }

        // Rooks
        Instantiate(wRook, board[0, 0].transform.position + new Vector3(0, (height / 2) + 0.1f, 0), Quaternion.identity).transform.parent = gameObject.transform;
        Instantiate(wRook, board[7, 0].transform.position + new Vector3(0, (height / 2) + 0.1f, 0), Quaternion.identity).transform.parent = gameObject.transform;
        Instantiate(bRook, board[0, 7].transform.position + new Vector3(0, (height / 2) + 0.1f, 0), Quaternion.identity).transform.parent = gameObject.transform;
        Instantiate(bRook, board[7, 7].transform.position + new Vector3(0, (height / 2) + 0.1f, 0), Quaternion.identity).transform.parent = gameObject.transform;

        // Knights
        Instantiate(wKnight, board[1, 0].transform.position + new Vector3(0, (height / 2) + 0.1f, 0), Quaternion.identity).transform.parent = gameObject.transform;
        Instantiate(wKnight, board[6, 0].transform.position + new Vector3(0, (height / 2) + 0.1f, 0), Quaternion.identity).transform.parent = gameObject.transform;
        Instantiate(bKnight, board[1, 7].transform.position + new Vector3(0, (height / 2) + 0.1f, 0), Quaternion.identity).transform.parent = gameObject.transform;
        Instantiate(bKnight, board[6, 7].transform.position + new Vector3(0, (height / 2) + 0.1f, 0), Quaternion.identity).transform.parent = gameObject.transform;

        // Bishops
        Instantiate(wBishop, board[2, 0].transform.position + new Vector3(0, (height / 2) + 0.1f, 0), Quaternion.identity).transform.parent = gameObject.transform;
        Instantiate(wBishop, board[5, 0].transform.position + new Vector3(0, (height / 2) + 0.1f, 0), Quaternion.identity).transform.parent = gameObject.transform;
        Instantiate(bBishop, board[2, 7].transform.position + new Vector3(0, (height / 2) + 0.1f, 0), Quaternion.identity).transform.parent = gameObject.transform;
        Instantiate(bBishop, board[5, 7].transform.position + new Vector3(0, (height / 2) + 0.1f, 0), Quaternion.identity).transform.parent = gameObject.transform;

        // Queens
        Instantiate(wQueen, board[3, 0].transform.position + new Vector3(0, (height / 2) + 0.1f, 0), Quaternion.identity).transform.parent = gameObject.transform;
        Instantiate(bQueen, board[3, 7].transform.position + new Vector3(0, (height / 2) + 0.1f, 0), Quaternion.identity).transform.parent = gameObject.transform;

        // Kings
        Instantiate(wKing, board[4, 0].transform.position + new Vector3(0, (height / 2) + 0.1f, 0), Quaternion.identity).transform.parent = gameObject.transform;
        Instantiate(bKing, board[4, 7].transform.position + new Vector3(0, (height / 2) + 0.1f, 0), Quaternion.identity).transform.parent = gameObject.transform;

    }

}

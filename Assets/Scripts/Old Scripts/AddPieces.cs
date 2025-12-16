using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPieces : MonoBehaviour
{

    BoardCreate3D bc;

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

    // xyz 
    int x_tile_count;
    int y_tile_count;
    int z_tile_count;

    // board
    GameObject[,,] board;

    // to subtract the diameter of the cells
    public Vector3 diameter;

    // Start is called before the first frame update
    void Start()
    {
        bc = gameObject.GetComponent<BoardCreate3D>();

        x_tile_count = bc.x_tile_count;
        y_tile_count = bc.y_tile_count;
        z_tile_count = bc.z_tile_count;

        board = bc.board;

        diameter = new Vector3(0, 0, bc.dark_tile.transform.localScale.z);

        PlacePieces();
    }

    void PlacePieces()
    {
        // Pawns
        for (int i = 0; i < x_tile_count; i++)
        {
            for (int k = 0; k < z_tile_count; k++)
            {
                bc.pieceArray[i, k, 1] = Instantiate(wPawn, board[i, k, 1].transform.position , EulerToQuaternion(90, 0, 0));
                bc.pieceArray[i, k, 1].transform.parent = gameObject.transform;

                bc.pieceArray[i, k, y_tile_count - 2] = Instantiate(bPawn, board[i, k, y_tile_count - 2].transform.position , EulerToQuaternion(-90, 0, 0));
                bc.pieceArray[i, k, y_tile_count - 2].transform.parent = gameObject.transform;
            }
               
        }

        // Kings
        bc.pieceArray[4, 4, 0] = Instantiate(wKing, board[4, 4, 0].transform.position , EulerToQuaternion(90, 0, 0));
        bc.pieceArray[4, 4, 0].transform.parent = gameObject.transform;

        bc.pieceArray[4, 4, y_tile_count - 1] = Instantiate(bKing, board[4, 4, y_tile_count - 1].transform.position , EulerToQuaternion(-90, 0, 0));
        bc.pieceArray[4, 4, y_tile_count - 1].transform.parent = gameObject.transform;

        // Queens
        for (int i = 3; i <= 5; i++)
        {
            for (int k = 3; k <= 5; k++)
            {
                if (i == 3 || i == 5 || k == 3 || k == 5)
                {
                    bc.pieceArray[i, k, 0] = Instantiate(wQueen, board[i, k, 0].transform.position , EulerToQuaternion(90, 0, 0));
                    bc.pieceArray[i, k, 0].transform.parent = gameObject.transform;

                    bc.pieceArray[i, k, y_tile_count - 1] = Instantiate(bQueen, board[i, k, y_tile_count - 1].transform.position , EulerToQuaternion(-90, 0, 0));
                    bc.pieceArray[i, k, y_tile_count - 1].transform.parent = gameObject.transform;
                }              
            }
        }

        // Bishops
        for (int i = 2; i <= 6; i++)
        {
            for (int k = 2; k <= 6; k++)
            {
                if (i == 2 || i == 6 || k == 2 || k == 6)
                {
                    bc.pieceArray[i, k, 0] = Instantiate(wBishop, board[i, k, 0].transform.position , EulerToQuaternion(90, 0, 0));
                    bc.pieceArray[i, k, 0].transform.parent = gameObject.transform;

                    bc.pieceArray[i, k, y_tile_count - 1] = Instantiate(bBishop, board[i, k, y_tile_count - 1].transform.position , EulerToQuaternion(-90, 0, 0));
                    bc.pieceArray[i, k, y_tile_count - 1].transform.parent = gameObject.transform;
                }
            }
        }

        // Knights
        for (int i = 1; i <= 7; i++)
        {
            for (int k = 1; k <= 7; k++)
            {
                if (i == 1 || i == 7 || k == 1 || k == 7)
                {
                    bc.pieceArray[i, k, 0] = Instantiate(wKnight, board[i, k, 0].transform.position , EulerToQuaternion(90, 0, 0));
                    bc.pieceArray[i, k, 0].transform.parent = gameObject.transform;

                    bc.pieceArray[i, k, y_tile_count - 1] = Instantiate(bKnight, board[i, k, y_tile_count - 1].transform.position , EulerToQuaternion(-90, 0, 0));
                    bc.pieceArray[i, k, y_tile_count - 1].transform.parent = gameObject.transform;
                }
            }
        }

        // Rooks
        for (int i = 0; i <= 8; i++)
        {
            for (int k = 0; k <= 8; k++)
            {
                if (i == 0 || i == 8 || k == 0 || k == 8)
                {
                    bc.pieceArray[i, k, 0] = Instantiate(wRook, board[i, k, 0].transform.position , EulerToQuaternion(90, 0, 0));
                    bc.pieceArray[i, k, 0].transform.parent = gameObject.transform;

                    bc.pieceArray[i, k, y_tile_count - 1] = Instantiate(bRook, board[i, k, y_tile_count - 1].transform.position , EulerToQuaternion(-90, 0, 0));
                    bc.pieceArray[i, k, y_tile_count - 1].transform.parent = gameObject.transform;
                }
            }
        }

    }

    public Quaternion EulerToQuaternion(int x, int y, int z)
    {
        Vector3 p = new Vector3(x, y, z);

        p.x *= Mathf.Deg2Rad;
        p.y *= Mathf.Deg2Rad;
        p.z *= Mathf.Deg2Rad;
        Quaternion q;
        float cy = Mathf.Cos(p.z * 0.5f);
        float sy = Mathf.Sin(p.z * 0.5f);
        float cr = Mathf.Cos(p.y * 0.5f);
        float sr = Mathf.Sin(p.y * 0.5f);
        float cp = Mathf.Cos(p.x * 0.5f);
        float sp = Mathf.Sin(p.x * 0.5f);
        q.w = cy * cr * cp + sy * sr * sp;
        q.x = cy * cr * sp + sy * sr * cp;
        q.y = cy * sr * cp - sy * cr * sp;
        q.z = sy * cr * cp - cy * sr * sp;
        return q;
    }

    public Vector3 QuaternionToEuler(Vector4 p)
    {
        Vector3 v;
        Vector4 q = new Vector4(p.w, p.z, p.x, p.y);
        v.y = Mathf.Atan2(2f * q.x * q.w + 2f * q.y * q.z, 1 - 2f * (q.z * q.z + q.w * q.w));
        v.x = Mathf.Asin(2f * (q.x * q.z - q.w * q.y));
        v.z = Mathf.Atan2(2f * q.x * q.y + 2f * q.z * q.w, 1 - 2f * (q.y * q.y + q.z * q.z));
        v *= Mathf.Rad2Deg;
        v.x = v.x > 360 ? v.x - 360 : v.x;
        v.x = v.x < 0 ? v.x + 360 : v.x;
        v.y = v.y > 360 ? v.y - 360 : v.y;
        v.y = v.y < 0 ? v.y + 360 : v.y;
        v.z = v.z > 360 ? v.z - 360 : v.z;
        v.z = v.z < 0 ? v.z + 360 : v.z;
        return v;
    }
}

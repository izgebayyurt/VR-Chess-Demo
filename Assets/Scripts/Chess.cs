using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Chess : MonoBehaviour
{
    #region Public Variables

    // Squares (Spheres)
    public GameObject darkSquare;
    public GameObject lightSquare;
    public static float BoardScale = 10f;
    [HideInInspector] public string turn;
    
    // Pieces (Prefabs)
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

    public static bool pieceMoving;

    #endregion
    
    #region Private Variables
    
    private Piece[,,] pieces;
    private GameObject[,,] squares;
    private static float width, height, depth;
    private FENReader fenReader;
    
    #endregion
    
    #region Unity Functions

    public void Awake()
    {
        width = darkSquare.transform.localScale.x;
        height = darkSquare.transform.localScale.y;
        depth = darkSquare.transform.localScale.z;
        squares = new GameObject[8,8,8];
        pieces = new Piece[8,8,8];
    }

    public void Start()
    {
        CreateBoard();
        fenReader = new FENReader(this);
        //StartCoroutine(NetworkedStartGame("http://imrelabpoincare.umeedu.maine.edu/3000/new"));
        fenReader.Load("8/8/8/8/8/8/PPPPPPPP/RNBQKBNR/8/8/8/8/8/8/PPPPPPPP/PPPPPPPP/8/8/8/8/8/8/8/8/8/8/8/8/8/8/8/8/8/8/8/8/8/8/8/8/8/8/8/8/8/8/8/8/pppppppp/pppppppp/8/8/8/8/8/8/rnbqkbnr/pppppppp/8/8/8/8/8/8 w KQkq - 0 1");
    }
    
    #endregion
    
    private IEnumerator NetworkedStartGame(string url)
    {
        UnityWebRequest uwr = UnityWebRequest.Post(url, new WWWForm());
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            fenReader.Load(uwr.downloadHandler.text);
        }
    }

    public void CreateBoard()
    {
        // Creates the checkered tile grid by given x * y * z
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                for (int k = 0; k < 8; k++)
                {
                    if ((i + j + k) % 2 == 0)
                    {
                        Debug.Log("Creating Square" + squares[0,0,0]);
                        squares[k,i,j] = Instantiate(lightSquare, new Vector3(-width * k, height * i, depth * j) * BoardScale, Quaternion.identity);
                    }
                    else
                    {
                        squares[k,i,j] = Instantiate(darkSquare, new Vector3(-width * k, height * i, depth * j) * BoardScale, Quaternion.identity);
                    }

                    squares[k,i,j].transform.parent = gameObject.transform;
                }
            }
        }
    }

    public void MovePiece(int from, int to)
    {
        int fRow = from & 0x007;
        int fCol = (from & 0x070) >> 4;
        int fLayer = (from & 0x700) >> 8;
        
        int tRow = to & 0x007;
        int tCol = (to & 0x070) >> 4;
        int tLayer = (to & 0x700) >> 8;
        
        pieces[fRow,fCol,fLayer].Move(to);
        pieces[tRow,tCol,tLayer] = pieces[fRow,fCol,fLayer];
        pieces[fRow,fCol,fLayer] = null;
    }
    
    public void SetPiece(int square, string type, string color)
    {
        Debug.Log(square + " " + type + " " + color);
        PieceType pieceType;
        if (type == "p")
            pieceType = PieceType.P;
        else if (type == "n")
            pieceType = PieceType.N;
        else if (type == "b")
            pieceType = PieceType.B;
        else if (type == "r")
            pieceType = PieceType.R;
        else if (type == "q")
            pieceType = PieceType.Q;
        else if (type == "k")
            pieceType = PieceType.K;
        else
            throw new System.Exception("Invalid piece type");
        
        PieceColor pieceColor;
        if (color == "w")
            pieceColor = PieceColor.W;
        else if (color == "b")
            pieceColor = PieceColor.B;
        else
            throw new System.Exception("Invalid piece color");
        

        GameObject o = InstantiatePiece(pieceType, pieceColor, square);
        Piece p = o.GetComponent<Piece>();
        
        int row = square & 0x007;
        int col = (square & 0x070) >> 4;
        int layer = (square & 0x700) >> 8;
        pieces[row,col,layer] = p;
    }

    private GameObject InstantiatePiece(PieceType type, PieceColor color, int square)
    {
        if (color == PieceColor.W)
        {
            if (type == PieceType.P)
            {
                return Instantiate(wPawn, GetSquarePosition(square), Quaternion.identity);
            }
            if (type == PieceType.N)
            {
                return Instantiate(wKnight, GetSquarePosition(square), Quaternion.identity);
            }
            if (type == PieceType.B)
            {
                return Instantiate(wBishop, GetSquarePosition(square), Quaternion.identity);
            }
            if (type == PieceType.R)
            {
                return Instantiate(wRook, GetSquarePosition(square), Quaternion.identity);
            }
            if (type == PieceType.Q)
            {
                return Instantiate(wQueen, GetSquarePosition(square), Quaternion.identity);
            }
            if (type == PieceType.K)
            {
                return Instantiate(wKing, GetSquarePosition(square), Quaternion.identity);
            }
        }
        else
        {
            if (type == PieceType.P)
            {
                return Instantiate(bPawn, GetSquarePosition(square), Quaternion.identity);
            }
            if (type == PieceType.N)
            {
                return Instantiate(bKnight, GetSquarePosition(square), Quaternion.identity);
            }
            if (type == PieceType.B)
            {
                return Instantiate(bBishop, GetSquarePosition(square), Quaternion.identity);
            }
            if (type == PieceType.R)
            {
                return Instantiate(bRook, GetSquarePosition(square), Quaternion.identity);
            }
            if (type == PieceType.Q)
            {
                return Instantiate(bQueen, GetSquarePosition(square), Quaternion.identity);
            }
            if (type == PieceType.K)
            {
                return Instantiate(bKing, GetSquarePosition(square), Quaternion.identity);
            }
        }
        throw new Exception("Invalid piece");
    }

    public static Vector3 GetSquarePosition(int square)
    {
        int row = square & 0x007;
        int col = (square & 0x070) >> 4;
        int layer = (square & 0x700) >> 8;
        return new Vector3(-width * row, height * layer, depth * col) * BoardScale;
    }
}

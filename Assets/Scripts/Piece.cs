using UnityEngine;

public class Piece : MonoBehaviour
{
    private PieceType type;
    private PieceColor color;
    private int square;

    public void Move(int sq)
    {
        this.square = sq;
        int row = square & 0x007;
        int col = (square & 0x070) >> 4;
        int layer = (square & 0x700) >> 8;
        transform.position = Chess.GetSquarePosition(square);
    }
}

public enum PieceType
{
    P,
    N,
    B,
    R,
    Q,
    K
}

public enum PieceColor
{
    W,
    B
}

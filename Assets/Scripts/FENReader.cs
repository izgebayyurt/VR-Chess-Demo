using System;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;

public class FENReader
{
  public FENReader(Chess chess)
  {
    this.chess = chess;
  }
  
  private Chess chess;
  public void Load(string fen)
  {
    string[] tokens = fen.Split(" ");

    string position = tokens[0];

    string[] rows = position.Split('/');

    // 64 rows: for each layer a..h, rows from a1x..h1x up to a8x..h8x
    // Iterate layers l = 0..7 (a..h)
    int rowIndex = 0;
    int square = 0;
    for (int l = 0; l < 8; l++) {
      for (int rankFromBottom = 0; rankFromBottom < 8; rankFromBottom++)
      {
        string row = rows[rowIndex++];
        for (int k = 0; k < row.Length; k++)
        {
          char ch = row[k];

          if (IsDigit(ch)) {
            square += int.Parse(ch.ToString());
          } else
          {
            char color = ch < 'a' ? 'w' : 'b';
            chess.SetPiece(square, ch.ToString().ToLower(), color.ToString());
            square += 1;
          }
        }

        square += 8;
      }

      square += 128;
    }

    /*
    chess._turn = tokens[1] as Color

    if (tokens[2].indexOf('K') > -1) {
      this._castling.w |= BITS.KSIDE_CASTLE
    }
    if (tokens[2].indexOf('Q') > -1) {
      this._castling.w |= BITS.QSIDE_CASTLE
    }
    if (tokens[2].indexOf('k') > -1) {
      this._castling.b |= BITS.KSIDE_CASTLE
    }
    if (tokens[2].indexOf('q') > -1) {
      this._castling.b |= BITS.QSIDE_CASTLE
    }

    // initialize dynamic castling anchors based on loaded position
    this._initCastlingStart()
    // ensure rights are consistent with actual pieces
    this._updateCastlingRights()

    this._epSquare = tokens[3] === '-' ? EMPTY : Ox888[tokens[3] as Square]
    this._fenEpSquare = this._epSquare
    this._halfMoves = parseInt(tokens[4], 10)
    this._moveNumber = parseInt(tokens[5], 10)

    this._hash = this._computeHash()
    this._updateSetup(fen)
    this._incPositionCount()*/
  }
  
  private bool IsDigit(char c)
  {
    return "0123456789".Contains(c);
  }
    
}

// Taken from https://www.patreon.com/posts/unity-3d-drag-22917454

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class DragAndDropPiece : MonoBehaviour

{
    private Vector3 mOffset;
    private float mZCoord;

    // scripts
    Outline outline;
    BoardBehavior3D bb;
    BoardCreate3D bc;
    AddPieces ap;

    // whether mouse is being pressed or not
    // (to avoid highlighting more than one piece at a time)
    bool mouseDown = false;

    // position of the piece
    // (in case we need to return it when dropped out of bounds)
    Vector3 objectPos;
	Quaternion objectRot;

    // the last highlighted tile's coordinates (to turn it back to non-highlighted)
    int lastTile_x = -1; int lastTile_y = -1; int lastTile_z = -1;

    // color of the highlighted / non-highlighted tiles
    Color tileHighlightColor = Color.green;
    Color tileOriginalColor = Color.white;

    // Set usingOutline to true if using keyboard and mouse
    bool usingOutline = false;

    void Start()
    {
        // get scripts
        outline = GetComponent<Outline>();
        bb = GetComponentInParent<BoardBehavior3D>();
        bc = GetComponentInParent<BoardCreate3D>();
        ap = GetComponentInParent<AddPieces>();
    }

    public void OnMouseEnter()
    {
        if (!bb.isMoving)
        {
            if (usingOutline){
                outline.color = 0;
                outline.enabled = true;
            }
        }
    }

    public void OnMouseExit()
    {
        if (!mouseDown)
        {
            if (usingOutline)
                outline.enabled = false;
        }
    }

    public void OnMouseUp()
    {
        // when mouse is lifted, drop the piece and adjust the parameters
        DropPiece();

        mouseDown = false;
        bb.isMoving = false;
        if (usingOutline)
            outline.color = 0;
    }

    public void OnMouseDown()
    {
        // mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z; // Mouse controls

        // // Store offset = gameobject world pos - mouse world pos
        // mOffset = gameObject.transform.position - GetMouseAsWorldPoint();

        // store gameObject pos
        objectPos = gameObject.transform.position;
	objectRot = gameObject.transform.rotation;

        // set the parameters
        mouseDown = true;
        bb.isMoving = true;
    }

    public void OnMouseDrag()
    {
        // transform.position = GetMouseAsWorldPoint() + mOffset; // Mouse controls

        // reset color
        if (usingOutline)
            outline.color = 1;

        HighlightTile();
    }


    // Mouse controls
    // private Vector3 GetMouseAsWorldPoint()
    // {
    //     // Pixel coordinates of mouse (x,y)
    //     Vector3 mousePoint = Input.mousePosition;

    //     // z coordinate of game object on screen
    //     mousePoint.z = mZCoord;

    //     // Convert it to world points
    //     return Camera.main.ScreenToWorldPoint(mousePoint);
    // }

    /*  Called when the dragged piece is dropped by releasing the mouse. Checks
        whether the piece's world coordinates correspond to a valid board
        coordinate. If so, moves the piece accordingly.
     */
    void DropPiece() {

        int[] pos = GetCoordinates(transform.position);
        int x_pos = pos[0];
        int y_pos = pos[1];
        int z_pos = pos[2];

        Debug.Log(x_pos + " " + y_pos + " " + z_pos);

        // check if the piece is in the board boundries
        if (0 <= x_pos && x_pos < bc.x_tile_count &&
            0 <= y_pos && x_pos < bc.y_tile_count &&
            0 <= z_pos && x_pos < bc.z_tile_count)
        {
            // if its in the board boundries, check if its legally valid
            if (CheckPiecePositionValid(x_pos, y_pos, z_pos))
            {
                gameObject.transform.position = bc.board[x_pos, y_pos, z_pos].transform.position;
		gameObject.transform.rotation = objectRot;
                bc.board[x_pos, y_pos, z_pos].GetComponent<MeshRenderer>().material.SetColor("_Color", GetOriginalTileColor(x_pos, y_pos, z_pos));

            }
            else
            {
                // if not, take the piece back to its position
                gameObject.transform.position = objectPos;
		gameObject.transform.rotation = objectRot;

                // if we highlighted a invalid position (like trying to capture
                // our piece, disable highlight when we drop the piece
                // since the piece will go back to its original place.

                if (usingOutline){
                    if (bc.pieceArray[lastTile_x, lastTile_y, lastTile_z].GetComponent<Outline>())
                    {
                        bc.pieceArray[lastTile_x, lastTile_y, lastTile_z].GetComponent<Outline>().enabled = false;
                    }
                }
            }
        }
        else
        {
            gameObject.transform.position = objectPos;
		gameObject.transform.rotation = objectRot;

        }
    }

    /*  Uses the current world position of the dragged piece to highlight the
        nearest tile. If the dragged piece is too far away from a tile, it does
        nothing
     */
    void HighlightTile()
    {
        int[] pos = GetCoordinates(transform.position);
        int x_pos = pos[0];
        int y_pos = pos[1];
        int z_pos = pos[2];

        if (
           !(x_pos == lastTile_x && y_pos == lastTile_y && z_pos == lastTile_z) &&
           0 <= x_pos   &&  x_pos < bc.x_tile_count  &&
           0 <= y_pos   &&  y_pos < bc.y_tile_count  &&
           0 <= z_pos   &&  z_pos < bc.z_tile_count
           )
        {
            ChangeTileColor(x_pos, y_pos, z_pos);
        }
    }

    /*  Turns a world coordinate into a integer coordinate for board indexing
        Parameters:
        A world position to translate into coordinates
     */
    int[] GetCoordinates(Vector3 worldPosition)
    {
        int x_pos = Mathf.RoundToInt(worldPosition.x / bb.x_increments);
        int y_pos = Mathf.RoundToInt(worldPosition.y / bb.y_increments);
        int z_pos = Mathf.RoundToInt(worldPosition.z / bb.z_increments);

        return new int[] {x_pos,y_pos,z_pos};
    }
    /*  After grabbing a piece and moving it around, changes the color of the
        destination tile to indicate where the piece will be placed. If there
        is no piece on the destination tile, it changes the tile color.
        Otherwise, it highlights the piece about to be captured.

        Parameters:
        x_pos, y_pos, z_pos: xyz coordinates of the destination tile
     */

    void ChangeTileColor(int x_pos, int y_pos, int z_pos)
    {
        // get the coordinates of the starting position of the piece
        int[] start_pos = GetCoordinates(objectPos);
        int start_x_pos = start_pos[0];
        int start_y_pos = start_pos[1];
        int start_z_pos = start_pos[2];

        if ((start_x_pos == x_pos && start_y_pos == y_pos && start_z_pos == z_pos) || bc.pieceArray[x_pos, y_pos, z_pos] == null)
        {
            // change the color of the tile
            bc.board[x_pos, y_pos, z_pos].GetComponent<MeshRenderer>().material.SetColor("_Color", tileHighlightColor);
        }
        else
        {
            // enable highlight for the given piece and set the color to 3rd color

            if (usingOutline){
                bc.pieceArray[x_pos, y_pos, z_pos].GetComponent<Outline>().color = 2;
                bc.pieceArray[x_pos, y_pos, z_pos].GetComponent<Outline>().enabled = true;
            }

        }

        // disable the startious highlight since we highlighted a new tile
        if (lastTile_x >= 0)
        {
            GameObject piece = bc.board[lastTile_x, lastTile_y, lastTile_z];

            piece.GetComponent<MeshRenderer>().material.SetColor("_Color", GetOriginalTileColor(lastTile_x, lastTile_y, lastTile_z));

            // this prevents the highlight bug when a piece is picked up and put in the same location
            // if (!(start_x_pos == lastTile_x && start_y_pos == lastTile_y && start_z_pos == lastTile_z) && piece != null)
            // {
            //     piece.GetComponent<Outline>().color = 0;
            //     piece.GetComponent<Outline>().enabled = false;
            // }

        }

        // update the position of the last tile
        lastTile_x = x_pos; lastTile_y = y_pos; lastTile_z = z_pos;
    }

    // Checks whether a target destination for a piece is valid or not
    bool CheckPiecePositionValid(int tar_x, int tar_y, int tar_z)
    {
        // Get the current position
        int[] cur_pos = GetCoordinates(objectPos);
        int cur_x = cur_pos[0];
        int cur_y = cur_pos[1];
        int cur_z = cur_pos[2];

        // If the current position has a piece there
        if (bc.pieceArray[tar_x, tar_y, tar_z] != null)
        {
            /* Check if the piece is the same color. If so, you can't capture your piece
             *  If otherwise, capture the piece
             */

            if (CompareTag("dark") && bc.pieceArray[tar_x, tar_y, tar_z].CompareTag("light"))
            {
                ModifyBoardPosition(cur_x, cur_y, cur_z, tar_x, tar_y, tar_z);
                return true;
            }

            if (CompareTag("light") && bc.pieceArray[tar_x, tar_y, tar_z].CompareTag("dark"))
            {
                ModifyBoardPosition(cur_x, cur_y, cur_z, tar_x, tar_y, tar_z);
                return true;
            }

            return false;
        }

        ModifyBoardPosition(cur_x, cur_y, cur_z, tar_x, tar_y, tar_z);
        return true;
    }

    /*  Modifies the piece array (the 2D array where pieces are stored) according
        to the current piece position and target piece position.
        Captures and removes a piece if its there. If not, just moves the piece.
     */

    void ModifyBoardPosition(int cur_x, int cur_y, int cur_z, int tar_x, int tar_y, int tar_z)
    {
        if (bc.pieceArray[tar_x, tar_y, tar_z] != null)
        {
            Destroy(bc.pieceArray[tar_x, tar_y, tar_z]);
            bc.pieceArray[tar_x, tar_y, tar_z] = gameObject;
            bc.pieceArray[cur_x, cur_y, cur_z] = null;
        }
        else
        {
            bc.pieceArray[tar_x, tar_y, tar_z] = gameObject;
            bc.pieceArray[cur_x, cur_y, cur_z] = null;
        }
    }


    /*  Gets the original color of the tile
        This is currently just white as the material is white but the color
        comes from emission
     */
    Color GetOriginalTileColor(int x, int y, int z)
    {
        if ((x + y + z) % 2 == 0)
        {
            return bc.light_tile.GetComponent<MeshRenderer>().sharedMaterial.color;
        }
        else
        {
            return bc.dark_tile.GetComponent<MeshRenderer>().sharedMaterial.color;
        }
    }

}

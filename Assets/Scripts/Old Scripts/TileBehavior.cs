using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehavior : MonoBehaviour
{

    public enum sides
    {
        back,
        bottom,
        front,
        left,
        right,
        top
    }

    public sides side;

    // Start is called before the first frame update
    void Start()
    {
        side = sides.bottom;
        AdjustTiles(1);
    }

    // Update is called once per frame
    void Update()
    {
        SideSelection();
        
    }

    void SideSelection()
    {
        if (Input.GetKeyDown("1")) { side = sides.back;         AdjustTiles(0); }
        else if (Input.GetKeyDown("2")) { side = sides.bottom;  AdjustTiles(1); }
        else if (Input.GetKeyDown("3")) { side = sides.front;   AdjustTiles(2); }
        else if (Input.GetKeyDown("4")) { side = sides.left;    AdjustTiles(3); }
        else if (Input.GetKeyDown("5")) { side = sides.right;   AdjustTiles(4); }
        else if (Input.GetKeyDown("6")) { side = sides.top;     AdjustTiles(5); }
    }

    void AdjustTiles(int tile) {

        for (int i = 0; i < transform.childCount; i++)
        {
            if (i != tile)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            
        }
    }
}

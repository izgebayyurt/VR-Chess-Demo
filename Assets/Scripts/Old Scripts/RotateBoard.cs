using UnityEngine;
using System.Collections;

public class RotateBoard : MonoBehaviour
{

    public float _sensitivity;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation;
    private bool _isRotating;

    void Start()
    {
        _sensitivity = 0.4f;
        _rotation = Vector3.zero;
    }

    void Update()
    {
        

        if (Input.GetMouseButtonDown(0)){
            // rotating flag
            _isRotating = true;

            // store mouse
            _mouseReference = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            // rotating flag
            _isRotating = false;
        }



        if (_isRotating)
        {
            // offset
            _mouseOffset = (Input.mousePosition - _mouseReference);

            _rotation.y = -_mouseOffset.y * _sensitivity;
            _rotation.x = -_mouseOffset.x * _sensitivity;

            // store mouse
            _mouseReference = Input.mousePosition;
        }



        // rotate
        transform.Rotate(_rotation);

        // slowly reset rotation
        _rotation = Vector3.Lerp(_rotation, Vector3.zero, 0.13f);

        // apply rotation
        Debug.Log(_rotation);
    }

    //void OnMouseDown()
    //{
    //    // rotating flag
    //    _isRotating = true;

    //    // store mouse
    //    _mouseReference = Input.mousePosition;
    //}

    //void OnMouseUp()
    //{
    //    // rotating flag
    //    _isRotating = false;
    //}

}
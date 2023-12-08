using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField] private Texture2D cursorDefault;
    [SerializeField] private Texture2D cursorChenged;
    public Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        //Cursor.SetCursor(cursorDefault, hotSpot, CursorMode.ForceSoftware);
    }

    private void OnMouseEnter()
    {
        //Cursor.SetCursor(cursorChenged, hotSpot, CursorMode.ForceSoftware);
    }

    private void OnMouseExit()
    {
        //Cursor.SetCursor(cursorDefault, hotSpot, CursorMode.ForceSoftware);
    }
}

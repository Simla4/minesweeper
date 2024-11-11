using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Variables

    private bool isTouching = false;
    private bool isFirstTouching = true;
    private float touchStartTime = 0f;
    private float holdThreshold = 0.5f;

    public static Action<TileBase> OnFirstClicked;

    #endregion

    #region Callbacks

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            HandleTouch(touch);
        }

        if (Input.GetMouseButtonDown(0))
        {
            isTouching = true;
            touchStartTime = Time.time;
        }

        if (Input.GetMouseButton(0) && isTouching)
        {
            if (Time.time - touchStartTime >= holdThreshold)
            {
                HandleHold(Input.mousePosition);
                isTouching = false;
            }
        }

        if (Input.GetMouseButtonUp(0) && isTouching)
        {
            HandleClick(Input.mousePosition);
            isTouching = false;
        }
    }

    void HandleTouch(Touch touch)
    {
        if (touch.phase == TouchPhase.Began)
        {
            isTouching = true;
            touchStartTime = Time.time;
        }

        if (touch.phase == TouchPhase.Stationary && isTouching)
        {
            if (Time.time - touchStartTime >= holdThreshold)
            {
                HandleHold(touch.position);
            }
        }

        if (touch.phase == TouchPhase.Ended && isTouching)
        {
            HandleClick(touch.position);
            isTouching = false;
        }
    }

    void HandleClick(Vector3 screenPosition)
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

        if (hit.collider != null)
        {
            TileBase tile = hit.collider.GetComponent<TileBase>();
            if (tile != null)
            {
                if (isFirstTouching)
                {
                    isFirstTouching = false;
                    OnFirstClicked?.Invoke(tile);
                    return;
                }

                if (!tile.Opened)
                {
                    tile.OnTileClicked(); 
                }
            }
        }
    }

    void HandleHold(Vector3 screenPosition)
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

        if (hit.collider != null)
        {
            TileBase tile = hit.collider.GetComponent<TileBase>();
            if (tile != null)
            {
                if (!tile.Flagged)
                {
                    tile.OnTileHeld();
                }
            }
        }
    }

    #endregion
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Variables

    private bool isTouching = false;
    private float touchStartTime = 0f;
    private float holdThreshold = 0.5f;

    #endregion

    #region Callbacks

    // Basılı tutma süresi (0.5 saniye)

    void Update()
    {
        // Mobil için dokunma kontrolü
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            HandleTouch(touch);
        }

        // PC için fare kontrolü
        if (Input.GetMouseButtonDown(0))
        {
            isTouching = true;
            touchStartTime = Time.time; // Basılı tutma süresinin başlangıcı
        }

        if (Input.GetMouseButton(0) && isTouching)
        {
            if (Time.time - touchStartTime >= holdThreshold)
            {
                HandleHold(Input.mousePosition); // Basılı tutuldu
                isTouching = false; // Bir kez basılı tutmayı yakalayalım
            }
        }

        if (Input.GetMouseButtonUp(0) && isTouching)
        {
            HandleClick(Input.mousePosition); // Tıklama olayı
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
                HandleHold(touch.position); // Basılı tutuldu
                isTouching = false;
            }
        }

        if (touch.phase == TouchPhase.Ended && isTouching)
        {
            HandleClick(touch.position); // Tıklama olayı
            isTouching = false;
        }
    }

    void HandleClick(Vector3 screenPosition)
    {
        // Tıklama işlemi
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

        if (hit.collider != null)
        {
            TileBase tile = hit.collider.GetComponent<TileBase>();
            if (tile != null)
            {
                tile.OnTileClicked(); // Tıklama işlemi
            }
        }
    }

    void HandleHold(Vector3 screenPosition)
    {
        // Basılı tutma işlemi
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

        if (hit.collider != null)
        {
            TileBase tile = hit.collider.GetComponent<TileBase>();
            if (tile != null)
            {
                tile.OnTileHeld(); // Basılı tutma işlemi
            }
        }
    }

    #endregion
}

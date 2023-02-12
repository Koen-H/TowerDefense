using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ==DEPRECATED==
/// Tries to make gameobject stay within the canvas of the screen
/// </summary>
public class StayInCanvas : MonoBehaviour
{
    Rect rect;

    private void Awake()
    {
        if (TryGetComponent(out RectTransform rectTR))
        {
            rect = rectTR.rect;
        }
        else if (TryGetComponent(out Sprite sprite)){
            rect = sprite.rect;
        }
        //rectTR = this.GetComponent<RectTransform>();
    }
    private void Start()
    {
        transform.position = PathGenerator.Instance.startNode.gameObject.transform.position;

    }

    private void Update()
    {
        //GetInView();
    }

    void GetInView()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPos.x - (rect.width / 2f) < 0)
        {
            screenPos.x = screenPos.x + ((rect.width / 2f) - screenPos.x);
            screenPos = Camera.main.ScreenToWorldPoint(screenPos);
            transform.position = new Vector3(screenPos.x, screenPos.y, 0);
        }
        else if (screenPos.x + (rect.width / 2f) > Camera.main.pixelWidth)
        {
            screenPos.x = screenPos.x - ((rect.width / 2f) - (Camera.main.pixelWidth - screenPos.x));
            screenPos = Camera.main.ScreenToWorldPoint(screenPos);
            transform.position = new Vector3(screenPos.x, screenPos.y, 0);
        }
        else if (screenPos.y - (rect.height / 2f) < 0)
        {
            screenPos.y = screenPos.y + ((rect.height / 2f) - screenPos.y);
            screenPos = Camera.main.ScreenToWorldPoint(screenPos);
            transform.position = new Vector3(screenPos.x, screenPos.y, 0);
        }
        else if (screenPos.y + (rect.height / 2f) > Camera.main.pixelHeight)
        {
            screenPos.y = screenPos.y - ((rect.height / 2f) - (Camera.main.pixelHeight - screenPos.y));
            screenPos = Camera.main.ScreenToWorldPoint(screenPos);
            transform.position = new Vector3(screenPos.x, screenPos.y, 0);
        }
    }
}

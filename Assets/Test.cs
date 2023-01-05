using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    RectTransform rectTR;

    private void Awake()
    {
        rectTR = this.GetComponent<RectTransform>();
    }

    private void Update()
    {
        GetInView();
    }

    void GetInView()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPos.x - (rectTR.rect.width / 2f) < 0)
        {
            screenPos.x  = screenPos.x + ((rectTR.rect.width / 2f) - screenPos.x);
            screenPos = Camera.main.ScreenToWorldPoint(screenPos);
            transform.position = new Vector3(screenPos.x, screenPos.y,0);
        }
        else if(screenPos.x + (rectTR.rect.width / 2f) > Camera.main.pixelWidth)
        {
            screenPos.x = screenPos.x - ((rectTR.rect.width / 2f) - (Camera.main.pixelWidth - screenPos.x));
            screenPos = Camera.main.ScreenToWorldPoint(screenPos);
            transform.position = new Vector3(screenPos.x, screenPos.y, 0);
        }
        else if (screenPos.y - (rectTR.rect.height / 2f) < 0)
        {
            screenPos.y = screenPos.y + ((rectTR.rect.height / 2f) - screenPos.y);
            screenPos = Camera.main.ScreenToWorldPoint(screenPos);
            transform.position = new Vector3(screenPos.x, screenPos.y, 0);
        }
        else if (screenPos.y + (rectTR.rect.height / 2f) > Camera.main.pixelHeight)
        {
            screenPos.y = screenPos.y - ((rectTR.rect.height / 2f) - (Camera.main.pixelHeight - screenPos.y));
            screenPos = Camera.main.ScreenToWorldPoint(screenPos);
            transform.position = new Vector3(screenPos.x, screenPos.y, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallPlatformHandle : MonoBehaviour
{
    public TextMeshPro countText;

    float x, y;
    int countMultiple;
    private void Awake()
    {
        countMultiple = Constants.platformCount;
        UpdateCountText();
        x = this.transform.position.y;
        y = this.transform.position.y;
    }

    private void OnMouseDrag()
    {
        if(BallSpawner.spawnBallCount > 0)
            this.transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, y);
    }

    void UpdateCountText ()
    {
        countText.text = "x" + countMultiple;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ball")
        {
            countMultiple--;
            if (countMultiple <= 0)
            {
                Destroy(this.gameObject);
            }
            else
            {
                UpdateCountText();
            }
        }
    }

}

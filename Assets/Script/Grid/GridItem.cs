using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GridItem : MonoBehaviour
{
    public TextMeshPro countText;
    public SpriteRenderer spriteGrid;
    int count;
    int value;
    int row;
    int column;
    public void InitGridValue (Grid gridData)
    {
        row = gridData.row;
        column = gridData.column;
        value = count = gridData.totalValue;
        UIManager.instance.UpdateGridCount(+1);
        UpdateCountValue();
    }

    void UpdateCountValue ()
    {
        countText.text = count.ToString();
        spriteGrid.color = GetSpriteColor();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ball")
        {
            count--;
            if (count > 0)
            {
                UpdateCountValue();
                UIManager.instance.UpdateScore(+1);
            }
            else
            {
                OnComplete();
            }
        }
    }

    void OnComplete()
    {
        if (count == 0)
        {
            UIManager.instance.UpdateGridCount(-1, true);
            Destroy(this.gameObject);
        }
    }

    Color32 GetSpriteColor()
    {
        Color color = Color.green;
        if (count > 0 && count < 10)
        {
            color = Color.green;
        }
        else if (count >= 10 && count < 20)
        {
            color = Color.blue;
        }
        else if (count >= 20 && count < 30)
        {
            color = Color.cyan;
        }
        else if (count >= 30 && count < 40)
        {
            color = Color.yellow;
        }
        else if (count >= 40 && count < 60)
        {
            color = Color.blue;
        }
        else if (count >= 60)
        {
            color = Color.red;
        }
        return color;
    }
}

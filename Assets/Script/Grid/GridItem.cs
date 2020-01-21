using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GridItem : MonoBehaviour
{
    public TextMeshPro countText;
    public SpriteRenderer spriteGrid;
    int value;
    int row;
    int column;
    public void InitGridValue (Grid gridData)
    {
        row = gridData.row;
        column = gridData.column;
        value = gridData.totalValue;
        UpdateCountValue();
    }

    void UpdateCountValue ()
    {
        countText.text = value.ToString();
        spriteGrid.color = GetSpriteColor();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ball")
        {
            value--;
            if (value > 0)
                UpdateCountValue();
            else
                Destroy(this.gameObject);
        }
    }

    Color32 GetSpriteColor()
    {
        Color color = Color.green;
        if (value > 0 && value < 10)
        {
            color = Color.green;
        }
        else if (value >= 10 && value < 20)
        {
            color = Color.blue;
        }
        else if (value >= 20 && value < 30)
        {
            color = Color.cyan;
        }
        else if (value >= 30 && value < 40)
        {
            color = Color.yellow;
        }
        else if (value >= 40 && value < 60)
        {
            color = Color.blue;
        }
        else if (value >= 60)
        {
            color = Color.red;
        }
        return color;
    }
}

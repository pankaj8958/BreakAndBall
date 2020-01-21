using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Grid[,] gridArray;
    private int row;
    private int column;
    private int vertical;
    private int horizontal;
    
    private GameObject gridPrefab;
    
    public List<Grid> currentLevelGrids;

    private void Awake()
    {
        gridPrefab = Resources.Load(Constants.gridObjectPath) as GameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitCurrentLevelGridData();
        InitGridArray();
        InitGridsData();
        SpawnGrids();
    }

    void InitCurrentLevelGridData ()
    {
        if(GameManager.instance.levelDataList != null 
            && GameManager.instance.levelDataList.Count >= GameManager.instance.currentLevel)
        {
            if(GameManager.instance.levelDataList[GameManager.instance.currentLevel - 1].levelGrids != null 
                && GameManager.instance.levelDataList[GameManager.instance.currentLevel - 1].levelGrids.Count > 0)
            {
                if (currentLevelGrids == null)
                    currentLevelGrids = new List<Grid>();
                currentLevelGrids = GameManager.instance.levelDataList[GameManager.instance.currentLevel - 1].levelGrids;
            }
        }
    }

    void InitGridArray()
    {
        vertical = (int)Camera.main.orthographicSize + Constants.gridNumberOffset_Row;
        horizontal = (int)(vertical * Camera.main.aspect) + Constants.gridNumberOffset_Column;
        column = horizontal * 2;
        row = vertical * 2;
        gridArray = new Grid[column,row];
        Debug.Log("Row: " + row + " Column: " + column);
    }

    void InitGridsData()
    {
        if (gridArray != null)
        {
            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    gridArray[i, j].totalValue = Random.Range(1, 40);
                    gridArray[i, j].column = i;
                    gridArray[i, j].row = j;
                }
            }
        }
    }

    void SpawnGrids()
    {
        if(currentLevelGrids != null && currentLevelGrids.Count > 0)
        {
            for (int i = 0; i < currentLevelGrids.Count; i++)
            {
                GridPosition(currentLevelGrids[i]);
            }
        }
        
    }


    void GridPosition (Grid grid)
    {
        
        GameObject gridObject = Instantiate(gridPrefab);
        gridObject.name = (grid.column + "," + grid.row);
        gridObject.transform.position = new Vector2(
                (grid.column - (horizontal - 0.5f))/Constants.gridPositionOffset, 
                (grid.row - (vertical - 0.5f))/Constants.gridPositionOffset
                );
        gridObject.GetComponent<GridItem>().InitGridValue(gridArray[grid.column, grid.row]);
    }

    void GridPosition(int x, int y)
    {

        GameObject gridObject = Instantiate(gridPrefab);
        gridObject.name = (x + "," + y);
        gridObject.transform.position = new Vector2(
                (x - (horizontal - 0.5f)) / Constants.gridPositionOffset,
                (y - (vertical - 0.5f)) / Constants.gridPositionOffset
                );
        
    }


}

[System.Serializable]
public struct Grid
{

    public int totalValue;
    public int row;
    public int column;
}

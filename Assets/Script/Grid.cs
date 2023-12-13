using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Grid<TGridObject>
{

    int width;
    int height;
    TGridObject[,] gridArray;
    float cellSize;
    Vector3 originPosition;

    TextMesh[,] textMeshArray;

    public delegate void OnGridElementChange(int x, int y);
    public event OnGridElementChange onElementChangeEvent;

    public delegate TGridObject CreateGrigObject(int x, int y, Grid<TGridObject> gridArrRef);

    public Grid(int width, int height, float cellSize, Vector3 originPosition, CreateGrigObject createGrigObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];
        textMeshArray = new TextMesh[width, height];


        for (var x = 0; x < gridArray.GetLength(0); x++)
        {
            for (var y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGrigObject(x,y, this);
            }
        }

        bool debug = false;
        if (debug)
        {
            for (var x = 0; x < gridArray.GetLength(0); x++)
            {
                for (var y = 0; y < gridArray.GetLength(1); y++)
                {
                    textMeshArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y]?.ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 8, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                }
            }

            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        }
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    public void SetValue(int x, int y, TGridObject value)
    {
        if( x>=0 && x<width && y>=0 && y<height)
        {
            gridArray[x, y] = value;
            textMeshArray[x, y].text = gridArray[x, y].ToString();
        }
    }

    //TODO set event and delete this method
    public void StrOutValue(int x, int y, TGridObject value)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            textMeshArray[x, y].text = gridArray[x, y].ToString();
        }
    }

    public void SetValue(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }
    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        //TODO внести в ENOT
        x = Mathf.FloorToInt( (worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt( (worldPosition - originPosition).y / cellSize);

        if (x < 0 || x >= width || y < 0 || y >= height)
        {
            x = -1; y = -1;
        }
    }

    public TGridObject GetValue(int x, int y)
    {
        if(x >= 0 && x < width && y >= 0 && y < height){
            return gridArray[x, y];
        }
        else
        {
            return default(TGridObject);
        }
    }  

    public TGridObject GetValue(Vector3 worldPosition)
    {
        int x, y; 
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public float GetCellSize()
    {
        return cellSize;
    }

    public void RedrawDebugElement(int x, int y)
    {
        textMeshArray[x, y].text = gridArray[x, y].ToString();
    }

    public void OnElementChangeEvent(int x, int y)
    {
        onElementChangeEvent?.Invoke(x,y);
    }

    public void IndexesOfElement(TGridObject obj, out int x, out int y )
    {
        x = -1;
        y = -1;

        for (int w = 0; w < width; ++w)
        {
            for (int h = 0; h < height; ++h)
            {
                if (GameObject.ReferenceEquals(gridArray[w, h], obj))
                {
                    x = w;
                    y = h;
                    return;
                }
            }
        }
    }
}

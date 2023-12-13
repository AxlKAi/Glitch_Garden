using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTest : MonoBehaviour
{
    Grid<HeatMapObject> gridArray;


    // Start is called before the first frame update
    void Start()
    {
        gridArray = new Grid<HeatMapObject>(5, 5, 1f, new Vector3(-3,-3,0), (int x,int y, Grid<HeatMapObject> heatObj) => new HeatMapObject() );

        gridArray.onElementChangeEvent += gridArray.RedrawDebugElement;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            HeatMapObject heatObj = gridArray.GetValue(worldPosition);
            if (heatObj != null)
            {
                int x, y;
                gridArray.GetXY(worldPosition, out x, out y);
                heatObj.AddValue(5);

                gridArray.OnElementChangeEvent(x,y);
                   
            }             
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(gridArray.GetValue(worldPosition));
        }
    }

    private class HeatMapObject
    {
        public int value;

        public void AddValue(int difference)
        {
            value += difference;           
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}
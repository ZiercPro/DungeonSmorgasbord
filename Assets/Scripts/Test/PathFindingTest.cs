using UnityEngine;
using Runtime.PathFinding.Grid;

public class PathFindingTest : MonoBehaviour
{
    private Grid<int> _grid;

    private void Awake()
    {
        _grid = new Grid<int>(20, 20, 1f, new Vector3(-9, -5));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _grid.SetValue(mousePosition, 10);
        }
    }
}
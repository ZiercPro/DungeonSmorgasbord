using Runtime.Helper;
using UnityEngine;
using Runtime.PathFinding.Grid;

public class PathFindingTest : MonoBehaviour
{
    private Grid<int> _grid;

    private Vector3 _startPosition;
    private EditableDictionary<string, int> testDic;

    private void Awake()
    {
        _startPosition = new Vector3(-9, -5);
      //  _grid = new Grid<int>(20, 20, 1f, _startPosition);
        AudioPlayerManager.Instance.PlayAudio(AudioName.MenuBgm);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _grid.SetGridObject(mousePosition, 10);
        }
    }
}
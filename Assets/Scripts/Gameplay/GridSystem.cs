using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] int xCount;
    [SerializeField] int yCount;
    [SerializeField] float size;
    [SerializeField] Vector2 offset;
    [SerializeField] float moveTime;
    [SerializeField] InputHandler inputHandler;

    public Dictionary<Vector2Int, GameObject> gridCells = new Dictionary<Vector2Int, GameObject>();
    public Action OnCheck;

    private void Awake() {
        inputHandler.OnInput += OnInputHandler;
    }

    // Xử lý đầu vào trên các đối tượng
    void OnInputHandler(InputType type) {
        int xStart = 0, xEnd = xCount, xStep = 1;
        int yStart = 0, yEnd = yCount, yStep = 1;

        if (type == InputType.Right) {
            xStart = xCount - 1; xEnd = -1; xStep = -1;
        }
        if (type == InputType.Up) {
            yStart = yCount - 1; yEnd = -1; yStep = -1;
        }

        for (int x = xStart; x != xEnd; x += xStep) {
            for (int y = yStart; y != yEnd; y += yStep) {
                Vector2Int currentPos = new Vector2Int(x, y);
                if (!IsEmptyCell(currentPos)) {
                    Orange orange = gridCells[currentPos].GetComponent<Orange>();
                    if (orange != null) {
                        orange.SetCoordinate(currentPos);
                        Vector2Int neighborOffset = Vector2Int.zero;

                        switch (type) {
                            case InputType.Left: neighborOffset = Vector2Int.left; break;
                            case InputType.Right: neighborOffset = Vector2Int.right; break;
                            case InputType.Up: neighborOffset = Vector2Int.up; break;
                            case InputType.Down: neighborOffset = Vector2Int.down; break;
                        }

                        Vector2Int neighborPos = currentPos + neighborOffset;

                        if (IsInGrid(neighborPos)) {
                            if (!gridCells.ContainsKey(neighborPos)) {
                                GameObject obj = gridCells[currentPos];

                                gridCells.Add(neighborPos, obj);
                                StartCoroutine(MovingToCell(obj, currentPos, neighborPos));
                                orange.SetCoordinate(neighborPos);
                                gridCells.Remove(currentPos);
                            }
                        }
                    }
                }
            }
        }

        OnCheck?.Invoke();
    }

    public void PlaceObject(GameObject obj, Vector2Int coordinate) {
        if (IsEmptyCell(coordinate)) {
            gridCells.Add(coordinate, obj);
            obj.transform.position = GetCellWorldPos(coordinate);
        }
    }

    private IEnumerator MovingToCell(GameObject obj, Vector2Int fromCoor, Vector2Int toCoor) {
        float timer = 0f;

        Vector2 startPos = GetCellWorldPos(fromCoor);
        Vector2 targetPos = GetCellWorldPos(toCoor);

        while (timer < moveTime) {
            timer += Time.deltaTime;
            obj.transform.position = Vector2.Lerp(startPos, targetPos, timer / moveTime);

            yield return null;
        }

        obj.transform.position = targetPos;
    }

    public Vector2 GetCellWorldPos(Vector2Int coordinate) {
        return new Vector2(coordinate.x * size + transform.position.x + offset.x, coordinate.y * size + transform.position.y + offset.y);
    }

    private bool IsEmptyCell(Vector2Int coordinate) {
        return !gridCells.ContainsKey(coordinate);
    }

    public bool IsInGrid(Vector2 coordinate) {
        return coordinate.x >= 0 && coordinate.x < xCount && coordinate.y >= 0 && coordinate.y < yCount;
    }

    public float GetCellSize() => size;

    public void ResetGrid() {
        gridCells.Clear();
    }

    private void OnDrawGizmos() {
        for (int i = 0; i < xCount; i++) {
            for (int j = 0; j < yCount; j++) {
                Gizmos.DrawWireCube(GetCellWorldPos(new Vector2Int(i, j)), new Vector3(size, size, size));
            }
        }
    }
}

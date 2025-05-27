using UnityEngine;

public class Orange : GridElement
{
    public Vector2Int coordinate { get; private set; }

    public void SetCoordinate(Vector2Int coordinate) {
        this.coordinate = coordinate;
    }
}

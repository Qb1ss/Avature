using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Maker : MonoBehaviour
{

    public LineRenderer LineRenderer;
    public EdgeCollider2D EdgeCol;

    private List<Vector2> _points;

    public void UpdateLine(Vector2 mousePos)
    {
        if (_points == null)
        {
            _points = new List<Vector2>();
            SetPoint(mousePos);
            return;
        }

        if (Vector2.Distance(_points.Last(), mousePos) > .1f)
            SetPoint(mousePos);
    }

    private void SetPoint(Vector2 point)
    {
        _points.Add(point);

        LineRenderer.positionCount = _points.Count;
        LineRenderer.SetPosition(_points.Count - 1, point);

        if (_points.Count > 1)
            EdgeCol.points = _points.ToArray();
    }
}
//By Bortsov "@Qb1ss" Gleb🏂//
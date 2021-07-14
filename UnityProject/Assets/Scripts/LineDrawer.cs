using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lr;

}

public class LineDrawer : MonoBehaviour
{
    public List<Point> points;
    private (int, int)[] connections = new (int, int)[]{ (3, 4), (0, 5), (17, 18), (0, 17), (13, 14), (13, 17), (18, 19), (5, 6), (5, 9), (14, 15), (0, 1), (9, 10), (1, 2), (10, 11), (9, 13), (19, 20), (6, 7), (15, 16), (2, 3), (11, 12), (7, 8) };
    private List<Line> lines;
    // Start is called before the first frame update
    void OnEnable()
    {
        lines = new List<Line>();
        foreach (var item in connections)
        {
            var from = item.Item1;
            var to = item.Item2;
            GameObject myLine = new GameObject();
            var lr = myLine.AddComponent<LineRenderer>();
            var line = myLine.AddComponent<Line>();
            lr.startColor = Color.red;
            lr.endColor = Color.red;
            lr.startWidth = 0.06f;
            lr.endWidth = 0.06f;
            lr.SetPosition(0, points[from].transform.position);
            lr.SetPosition(1, points[to].transform.position);
            line.lr = lr;
            lines.Add(line);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < connections.Length && connections.Length == 21; i++)
        {
            lines[i].lr.SetPosition(0, points[connections[i].Item1].transform.position);
            lines[i].lr.SetPosition(1, points[connections[i].Item2].transform.position);
        }
    }
    private void OnDestroy()
    {
        foreach (var line in lines)
        {
            Destroy(line);
        }
    }
}

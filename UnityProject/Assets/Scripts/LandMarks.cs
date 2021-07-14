using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMarks : MonoBehaviour
{
    [SerializeField] public ELandMark _landMark;
    [SerializeField] public List<Point> _points;
    [SerializeField] private LineDrawer lineDrawer;

    public void ActivateLines()
    {
        if (lineDrawer)
        {
            lineDrawer.points = _points;
            lineDrawer.gameObject.SetActive(true);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Receiver : MonoBehaviour
{
    [SerializeField] private List<LandMarks> _landMarks;
    [SerializeField] private float _handMultiplier = 1f;
    private HandListener _handListener;
    private Dictionary<ELandMark, List<Point>> _points;

    private event Action mainThreadQueuedCallbacks;
    private event Action eventsClone; // Use this when executing the queued callbacks to prevent race conditions from queueing callbacks while existing callbacks are being executed

    void Update()
    {
        if (mainThreadQueuedCallbacks != null)
        {
            eventsClone = mainThreadQueuedCallbacks;
            mainThreadQueuedCallbacks = null;
            eventsClone.Invoke();
            eventsClone = null;
        }
    }
    void Start()
    {
        _points = new Dictionary<ELandMark, List<Point>>();
        foreach (var landmark in _landMarks)
        {
            _points[landmark._landMark] = landmark._points;
        }

        _handListener = new HandListener();
        _handListener.Start();
        RegisterToListener();
    }

    private void OnDestroy()
    {
        UnRegisterToListener();
        _handListener.Stop();
    }
    private void RegisterToListener()
    {
        _handListener.onPointChange += onPointChange;
    }
    private void UnRegisterToListener()
    {
        _handListener.onPointChange -= onPointChange;
    }

    void onPointChange(ELandMark landMark ,int idx, Vector3 pos)
    {
        pos.x *= -1;
        mainThreadQueuedCallbacks += () => { _points[landMark][idx].SetPosition(-pos * _handMultiplier); };
        Debug.Log($"{idx} - {pos}");
    }

}

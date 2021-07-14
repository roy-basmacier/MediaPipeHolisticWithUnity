using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Receiver : MonoBehaviour
{
    [Range(0, 1.5f), SerializeField] private float _depthRange;
    [SerializeField] private List<LandMarks> _landMarks;
    [SerializeField] private float _handMultiplier = 1f;
    private HandListener _handListener;
    private Dictionary<ELandMark, List<Point>> _points;

    private event Action mainThreadQueuedCallbacks;
    private event Action eventsClone; // Use this when executing the queued callbacks to prevent race conditions from queueing callbacks while existing callbacks are being executed

    private Vector3 _depthVector;
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
        _handListener.onPointChange += OnPointChange;
        _handListener.depthCalculation += OnCalculateDepth;
    }
    private void UnRegisterToListener()
    {
        _handListener.onPointChange -= OnPointChange;
        _handListener.depthCalculation -= OnCalculateDepth;
    }

    private void OnCalculateDepth(ELandMark landMark, Vector3 origin, Vector3 left, Vector3 right)
    {
        var dist1 = (origin - left).magnitude;
        var dist2 = (origin - right).magnitude;
        Debug.Log($"distance: {dist1} - {dist2}");
        var average = (dist1 + dist2) / 2f;
        var t = Mathf.InverseLerp(0f, 0.3f, average);
        _depthVector = Vector3.Lerp(-Vector3.forward * _depthRange, Vector3.forward * _depthRange, t);
    }
    private void OnPointChange(ELandMark landMark, int idx, Vector3 pos)
    {
        pos.x *= -1;
        mainThreadQueuedCallbacks += () => 
        { 
            _points[landMark][idx].SetPosition(-pos * _handMultiplier + _depthVector);
        };
        //Debug.Log($"{idx} - {pos}");
    }

}

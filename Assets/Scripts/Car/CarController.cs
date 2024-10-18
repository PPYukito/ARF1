using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using DG.Tweening;

public class CarController : MonoBehaviour
{
    public enum CarSide
    {
        Middle = 0,
        Left = 1,
        Right = 2
    }

    public delegate void CarFinishedRace();
    public CarFinishedRace carFinishedRace;

    [Header("Race Setting")]
    public List<Transform> waypoints;
    public float cubicBezierDistance = 1.0f;
    public Rigidbody rigid;
    //public CarSide carSide;
    //public float carSideThreshold = 0.15f;

    [Header("CarFeed")]
    public Color LineColor;
    public VideoClip video;

    private List<Vector3> arrayOfWaypoints = new List<Vector3>();
    private Vector3 currentWayPoint;
    private Vector3 pastWayPoint;
    private Vector3 startPoint;
    private float finishRaceIn;

    private void Start()
    {
        // They suggest to do this manually before you call DoTween api.
        DOTween.Init();
        startPoint = transform.position;

        //switch (carSide)
        //{
        //    case CarSide.Middle:
        //        carSideThreshold = 0;
        //        break;
        //    case CarSide.Left:
        //        carSideThreshold *= -1;
        //        break;
        //    case CarSide.Right:
        //        break;
        //}

        arrayOfWaypoints.Clear();
        // arrayOfWaypoints = CalculatWayPoints(waypoints);
        foreach (Transform waypoint in waypoints)
        {
            arrayOfWaypoints.Add(waypoint.position);
        }
        currentWayPoint = arrayOfWaypoints[0];
    }

    private void Update()
    {
        //UpdateLookAt();
    }

    public void SetUpCarFinishCallBack(CarFinishedRace sentCallback)
    {
        carFinishedRace = sentCallback;
    }

    public void MoveCar()
    {
        TurnCar();
        finishRaceIn = UnityEngine.Random.Range(35f, 37f);

        transform.DOPath(arrayOfWaypoints.ToArray(), finishRaceIn, PathType.CatmullRom, PathMode.Full3D, 30, Color.red)
            .SetOptions(true)
            .OnWaypointChange(OnWayPointChanged)
            .OnComplete(OnTrackComplete);
    }

    private void OnWayPointChanged(int waypointIndex)
    {
        pastWayPoint = currentWayPoint;
        currentWayPoint = arrayOfWaypoints[waypointIndex];

        TurnCar();
    }

    //private void UpdateLookAt()
    //{
    //    if (currentWayPoint == arrayOfWaypoints[arrayOfWaypoints.Count - 1])
    //    {
    //        currentWayPoint = startPoint;
    //    }

    //    if (currentWayPoint != pastWayPoint)
    //    {
    //        TurnCar();
    //        transform.LookAt(currentWayPoint);
    //    }
    //}

    private void TurnCar()
    {
        transform.DOLookAt(currentWayPoint, 0.3f);
    }

    public CarFeed GetCarFeed()
    {
        return new CarFeed { lineRendererColor = LineColor , carVideo = video };
    }

    private void OnTrackComplete()
    {
        finishRaceIn = 0;
        carFinishedRace?.Invoke();
    }

    //It worked but too small to use;
    //private List<Vector3> CalculatWayPoints(List<Transform> waypoints)
    //{
    //    for (int i = 0; i < waypoints.Count; i++)
    //    {
    //        if (i < waypoints.Count - 1)
    //        {
    //            Vector3 nextPoint = waypoints[i + 1].position;
    //            calculatedWaypoints.Add(nextPoint);

    //            Vector3 thisOutData = waypoints[i].forward / cubicBezierDistance;
    //            calculatedWaypoints.Add(thisOutData);

    //            Vector3 nextInData = waypoints[i + 1].forward / -cubicBezierDistance;
    //            calculatedWaypoints.Add(nextInData);
    //        }
    //    }

    //    foreach (Vector3 value in calculatedWaypoints)
    //    {
    //        Debug.LogWarning($"{value}");
    //    }

    //    return calculatedWaypoints;
    //}
}

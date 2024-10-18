using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class RaceSceneManager : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject codeScanner;
    public GameObject trackObject;
    public GameObject buttomPanel;
    public GameObject sidePanel;

    [Header("Cars")]
    public List<CarController> Cars;

    [Header("Manager")]
    public ARTrackedImageManager arTrackedImageManager;
    public RaceSceneScreenController raceSceneScreenController;
    public RaceScreenComponentsController raceScreenComponentsController;

    [Header("Test Mode")]
    public bool testMode;
    public GameObject testEmptyObject;

    private int carCount;
    private CarController currentTargetCar;
    private int currentCarIndex;
    private int finishedRace = 0;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        raceSceneScreenController.SetActiveCarPovScreen(false);
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (testMode)
        {
            SetupTrackObjectToInstantiatedObject();
            codeScanner.SetActive(false);
        }
        else
        {
            buttomPanel.SetActive(false);
            sidePanel.SetActive(false);
            trackObject.SetActive(false);
            testEmptyObject.SetActive(false);
        }

        SetUpCars();
    }

    private void SetUpCars()
    {
        carCount = Cars.Count;
        currentTargetCar = Cars[0];
        currentCarIndex = 0;

        foreach (CarController car in Cars)
        {
            car.SetUpCarFinishCallBack(CarFinishedRaceCallBack);
        }

        raceScreenComponentsController.SetUpTarget(currentTargetCar);
    }

    private void OnEnable()
    {
        arTrackedImageManager.trackedImagesChanged += OnChanged;
    }

    private void OnDisable()
    {
        arTrackedImageManager.trackedImagesChanged -= OnChanged;
    }

    private void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            codeScanner.SetActive(false);
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            SetupTrackObjectToInstantiatedObject();
            buttomPanel.SetActive(true);
            sidePanel.SetActive(true);
        }

        //foreach (var updatedImage in eventArgs.updated)
        //{
        //    // Handle updated event
        //}

        //foreach (var removedImage in eventArgs.removed)
        //{
        //    // Handle removed event
        //}
    }

    private void SetupTrackObjectToInstantiatedObject()
    {
        trackObject.SetActive(true);

        GameObject emptySpace;
        if (testMode)
            emptySpace = testEmptyObject;
        else
            emptySpace = GameObject.FindWithTag("EmptySpace");

        if (emptySpace)
        {
            trackObject.transform.SetParent(emptySpace.transform, true);
        }
    }

    public void StartRace()
    {
        finishedRace = 0;
        buttomPanel.SetActive(false);
        MoveCars();
    }

    private void MoveCars()
    {
        foreach (CarController car in Cars)
        {
            car.MoveCar();
        }
    }

    public void ChangeCarTargetVideo()
    {
        if (carCount > 1 && currentCarIndex >= carCount - 1)
        {
            currentCarIndex = 0;
        }
        else
            currentCarIndex += 1;

        currentTargetCar = Cars[currentCarIndex];
        raceScreenComponentsController.SetUpTarget(currentTargetCar);
    }

    private void CarFinishedRaceCallBack()
    {
        finishedRace++;
        if (finishedRace >= carCount)
        {
            Debug.Log("both cars have finished!!!");
            buttomPanel.SetActive(true);
        }
    }
}

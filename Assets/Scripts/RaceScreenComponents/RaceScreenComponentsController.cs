using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class RaceScreenComponentsController : MonoBehaviour
{
    [Header("CarVideos Player")]
    public VideoPlayer carFeedVideoPlayer;

    [Header("POV Panel")]
    public GameObject kartFeedPanel;
    public Vector3 panelThreshold;

    [Header("LineRenderer")]
    public LineRenderer lineRenderer;

    [Header("TargetObject")]
    public GameObject targetObject;

    public GameObject changeCarVideoButton;

    private bool openPanelAndLine = false;
    private List<Vector3> linePoints;

    // Start is called before the first frame update
    private void Start()
    {
        linePoints = new List<Vector3>();

        SetActiveKartFeedPanel(false);
        lineRenderer.enabled = false;
        changeCarVideoButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (openPanelAndLine)
        {
            SetLineRendererPoints();
            lineRenderer.SetPositions(linePoints.ToArray());
        }
    }

    private void SetLineRendererPoints()
    {
        linePoints.Clear();
        linePoints.Add(kartFeedPanel.transform.position + panelThreshold);
        linePoints.Add(targetObject.transform.position);
    }

    public void OpenAndCloseCarFeedPanel()
    {
        openPanelAndLine = !openPanelAndLine;
        lineRenderer.enabled = openPanelAndLine;
        changeCarVideoButton.SetActive(openPanelAndLine);

        if (openPanelAndLine)
            carFeedVideoPlayer.Play();
        else
            carFeedVideoPlayer.Pause();

        SetActiveKartFeedPanel(openPanelAndLine);
    }

    private void SetActiveKartFeedPanel(bool active)
    {
        kartFeedPanel.SetActive(active);
    }

    public void SetUpTarget(CarController TargetCar)
    {
        carFeedVideoPlayer.Stop();

        targetObject = TargetCar.gameObject;
        CarFeed carFeed = TargetCar.GetCarFeed();

        lineRenderer.material.color = carFeed.lineRendererColor;
        carFeedVideoPlayer.clip = carFeed.carVideo;
        carFeedVideoPlayer.Play();
    }
}

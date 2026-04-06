using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaypointUIManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private RectTransform _markersParent;
    [SerializeField] private MiniGamePlayerController _playerController;

    private const float MarkerSize = 50f;
    private const float MarkerOffsetY = 1.5f;

    private MiniGameWaypoint[] _waypoints;
    private readonly List<RectTransform> _markers = new();

    private void Start()
    {
        if (_camera == null)
            _camera = Camera.main;

        _waypoints = FindObjectsByType<MiniGameWaypoint>(FindObjectsSortMode.None);
        SpawnMarkers();
    }

    /// <summary>
    /// Instantiates one clickable UI marker per waypoint found in the scene.
    /// </summary>
    private void SpawnMarkers()
    {
        for (int i = 0; i < _waypoints.Length; i++)
        {
            GameObject markerGO = new GameObject($"Marker_{i + 1}");
            markerGO.transform.SetParent(_markersParent, false);

            RectTransform rt = markerGO.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(MarkerSize, MarkerSize);

            Image bg = markerGO.AddComponent<Image>();
            bg.color = new Color(0.15f, 0.55f, 1f, 0.9f);

            int idx = i;
            Button btn = markerGO.AddComponent<Button>();
            btn.onClick.AddListener(() => _playerController.MoveTo(_waypoints[idx].transform.position));

            GameObject labelGO = new GameObject("Label");
            labelGO.transform.SetParent(markerGO.transform, false);

            RectTransform labelRT = labelGO.AddComponent<RectTransform>();
            labelRT.anchorMin = Vector2.zero;
            labelRT.anchorMax = Vector2.one;
            labelRT.offsetMin = Vector2.zero;
            labelRT.offsetMax = Vector2.zero;

            TextMeshProUGUI label = labelGO.AddComponent<TextMeshProUGUI>();
            label.text = (i + 1).ToString();
            label.alignment = TextAlignmentOptions.Center;
            label.fontSize = 20;
            label.fontStyle = FontStyles.Bold;
            label.color = Color.white;
            label.raycastTarget = false;

            _markers.Add(rt);
        }
    }

    private void Update()
    {
        for (int i = 0; i < _markers.Count; i++)
        {
            Vector3 worldPos = _waypoints[i].transform.position + Vector3.up * MarkerOffsetY;
            Vector3 screenPos = _camera.WorldToScreenPoint(worldPos);

            bool isVisible = screenPos.z > 0;
            _markers[i].gameObject.SetActive(isVisible);

            if (isVisible)
                _markers[i].position = new Vector3(screenPos.x, screenPos.y, 0f);
        }
    }
}

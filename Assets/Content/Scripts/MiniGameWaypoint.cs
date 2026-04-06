using UnityEngine;

/// <summary>
/// Marks a GameObject as a clickable waypoint in the mini-game.
/// Requires a Collider on this GameObject for raycast detection.
/// </summary>
[RequireComponent(typeof(Collider))]
public class MiniGameWaypoint : MonoBehaviour { }

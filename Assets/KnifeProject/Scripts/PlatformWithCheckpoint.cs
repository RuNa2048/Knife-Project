using UnityEngine;

public class PlatformWithCheckpoint : Platform
{
	[Header("Player Settings")]
	[SerializeField] private Transform _checkpointPos;
		
	public Transform CheckpointPosition{ get { return _checkpointPos; } private set { } }



}

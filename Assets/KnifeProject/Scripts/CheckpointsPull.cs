using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointsPull: MonoBehaviour
{
    public Vector3 LastCheckpointPos { get; private set; }

    public void SavingPosition(Vector3 pos)
    {
        if (LastCheckpointPos == pos)
            return;


        LastCheckpointPos = pos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevel : MonoBehaviour
{
    public CallerBehaviour caller;

    private void Start() {
        caller.OnCall += Exit;
    }

    private void Exit() {

    }
}

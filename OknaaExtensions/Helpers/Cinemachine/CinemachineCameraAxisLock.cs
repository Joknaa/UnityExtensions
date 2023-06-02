using UnityEngine;
using Cinemachine.Utility;
using Cinemachine;

/// <summary>
/// An add-on module for Cinemachine Virtual Camera that Locks the Camera position to one or more axis
/// </summary>
[AddComponentMenu("")] // Hide in menu
#if UNITY_2018_3_OR_NEWER
[ExecuteAlways]
#else
[ExecuteInEditMode]
#endif
[SaveDuringPlay]
public class CinemachineCameraAxisLock : CinemachineExtension {
    /// <summary>
    /// Lock Camera position to this axis
    /// </summary>
    public bool _lockX = false;
    public bool _lockY = false;
    public bool _lockZ = false;

    /// <summary>
    /// Applies the specified offset to the camera state
    /// </summary>
    /// <param name="vcam">The virtual camera being processed</param>
    /// <param name="stage">The current pipeline stage</param>
    /// <param name="state">The current virtual camera state</param>
    /// <param name="deltaTime">The current applicable deltaTime</param>
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state,
        float deltaTime) {
        if (stage != CinemachineCore.Stage.Body) return;
        var pos = state.RawPosition;
        if (_lockX) pos.x = 0;
        if (_lockY) pos.y = 0;
        if (_lockZ) pos.z = 0;
        state.RawPosition = pos;
    }
}
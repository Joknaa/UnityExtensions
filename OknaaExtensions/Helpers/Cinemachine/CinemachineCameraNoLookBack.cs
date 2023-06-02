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
public class CinemachineCameraNoLookBack : CinemachineExtension {
    private CinemachineTransposer _transposer;
    private Transform _playerTransform;
    private Vector3 _originalPosition;
    private Vector3 _transposerOffset;
    private float _playerLastPositionZ;
    private float _playerDeltaPositionZ;
    private float _playerCurrentPositionZ;
    private bool _CameraIsLocked;

    protected override void Awake() {
        base.Awake();
        _transposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>();
        _transposerOffset = _transposer.m_FollowOffset;
        _playerTransform = GameObject.FindWithTag("Player").transform;
        _playerLastPositionZ = _playerTransform.position.z;
        _originalPosition = transform.position;
    }
    
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state,
        float deltaTime) {
        if (stage != CinemachineCore.Stage.Body) return;
        
        _playerCurrentPositionZ = _playerTransform.position.z;
        _playerDeltaPositionZ = _playerCurrentPositionZ - _playerLastPositionZ;
        _playerLastPositionZ = _playerCurrentPositionZ;
        var playerMovedBackwards = _playerDeltaPositionZ < 0;
        var playerMovedForwards = _playerDeltaPositionZ > 0;

        if (playerMovedBackwards) LockCamera();
        else if (playerMovedForwards) {
            if (!_CameraIsLocked) return;
            
            var distanceToPlayerZ = Mathf.Abs(_playerTransform.position.z - state.RawPosition.z);
            if (Mathf.Abs(distanceToPlayerZ - Mathf.Abs(_transposerOffset.z)) < 0.1f) {
                UnlockCamera();
            }
            else LockCamera();
            
        } 
    }

    private void LockCamera() {
        if (_CameraIsLocked) return;
        _CameraIsLocked = true;
        _transposer.enabled = false;
        transform.position -= _transposerOffset;
    }

    private void UnlockCamera() {
        if (!_CameraIsLocked) return;
        _CameraIsLocked = false;
        _transposer.enabled = true;
        transform.position = _originalPosition;
        _transposer.m_FollowOffset = _transposerOffset;
    }
}
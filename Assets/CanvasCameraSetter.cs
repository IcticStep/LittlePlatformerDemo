using UnityEngine;
using Zenject;

[RequireComponent(typeof(Canvas))]
public class CanvasCameraSetter : MonoBehaviour
{
    [Inject] private Camera _camera;
    private Canvas _canvas;

    private void Awake() => 
        _canvas = GetComponent<Canvas>();

    private void Start() => 
        _canvas.worldCamera = _camera;
}

using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private LayerMask _layer;

    private Camera _camera;

    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }

    private void Start() => _camera = Camera.main;

    private void Update()
    {
        Horizontal = _joystick.Horizontal;
        Vertical = _joystick.Vertical;

        //if(Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        //    if (Physics.Raycast(ray, out RaycastHit hit, _layer))
        //    {
        //        Debug.Log(hit);
        //        Game.Locator.Factory.SpawnLightball(hit.point);
        //    }

        //}
    }
}
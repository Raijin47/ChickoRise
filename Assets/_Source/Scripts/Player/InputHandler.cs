using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private LayerMask _layer;

    private readonly float MaxDistance = 300f;
    private Camera _camera;

    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }

    private void Start()
    {
        _camera = Camera.main;
    }
        
    private void Update()
    {
        Horizontal = _joystick.Horizontal;
        Vertical = _joystick.Vertical;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, MaxDistance, _layer))
            {
                if (hit.collider.TryGetComponent(out Toadstool _))
                    Game.Locator.Factory.SpawnLightball(hit.point);
            }
        }
    }
}
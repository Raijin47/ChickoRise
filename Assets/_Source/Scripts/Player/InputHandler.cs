using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private readonly string AxisHorizontal = "Horizontal";
    private readonly string AxisVertical = "Vertical";

    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }

    private void Update()
    {
        Horizontal = Input.GetAxis(AxisHorizontal);
        Vertical = Input.GetAxis(AxisVertical);
    }
}
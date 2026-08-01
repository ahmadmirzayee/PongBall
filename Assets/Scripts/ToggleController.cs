using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    [Header("Components")]
    public GameObject knob;
    private Toggle toggle;
    private Vector3 knobPosition;

    // Initialize the toggle and knob position
    private void Start()
    {
        toggle = GetComponent<Toggle>();
        knobPosition = knob.transform.position;
    }

    // Update the knob's position based on the toggle's state
    private void Update()
    {
        if (toggle.isOn)
        {
            knob.transform.position = new Vector3(knobPosition.x, knobPosition.y, 0);
        }
        else
        {
            knob.transform.position = new Vector3(knobPosition.x - 17.4f, knobPosition.y, 0);
        }
    }
}

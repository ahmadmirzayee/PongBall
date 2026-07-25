using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    [Header("Components")]
    public GameObject knob;
    private Toggle toggle;
    private Vector3 knobPosition;

    void Start()
    {
        toggle = GetComponent<Toggle>();
        knobPosition = knob.transform.position;
    }

    void Update()
    {
        if (toggle.isOn)
        {
            knob.transform.position = new Vector3(knobPosition.x, knobPosition.y, 0);
        }
        else
        {
            knob.transform.position = new Vector3(-knobPosition.x, knobPosition.y, 0);
        }
    }
}

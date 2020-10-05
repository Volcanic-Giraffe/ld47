using UnityEngine;

public class Turret : MonoBehaviour
{
    public float RotationSpeed = 0.5f;

    private Camera _camera;
    private bool _inputDisabled;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputDisabled) return;
        
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 50, ~LayerMask.NameToLayer("BasePlane")))
        {
            var lookPos = hit.point - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = rotation; //feels shitty  Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * RotationSpeed);
        }

        if (Input.GetMouseButton(0))
        {
            SendMessage("Fire");
        }
    }

    public void DisableInput()
    {
        _inputDisabled = true;
    }
}

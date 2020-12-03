using UnityEngine;

public class Skybox_CamUpdate : MonoBehaviour
{
    [SerializeField]
    float x_RotationValue = 0,
        y_RotationValue = 0,
        z_RotationValue = 0;
    float x_Axis, y_Axis, z_Axis;
    // Update is called once per frame
    void Update()
    {
        x_Axis = x_RotationValue * Time.deltaTime;
        y_Axis = y_RotationValue * Time.deltaTime;
        z_Axis = z_RotationValue * Time.deltaTime;
        this.gameObject.transform.Rotate(new Vector3(x_Axis, y_Axis, z_Axis));
    }
}

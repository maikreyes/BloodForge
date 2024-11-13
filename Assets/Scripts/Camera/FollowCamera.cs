using UnityEngine;

public class FollowCamara : MonoBehaviour
{
    public GameObject Target;
    public GameObject T;
    public float speed = 1.5f;

    void FixedUpdate()
    {
        this.transform.LookAt(Target.transform);
        float car_Move = Mathf.Abs(Vector3.Distance(this.transform.position, T.transform.position) * speed);
        this.transform.position = Vector3.MoveTowards(this.transform.position, T.transform.position, car_Move * Time.deltaTime);
    }
}
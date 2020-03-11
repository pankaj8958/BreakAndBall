using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionDirectionBehaviour : MonoBehaviour
{

    Vector2 initialPosition;
    Vector2 currentPosition;

    private void OnMouseDown()
    {
        initialPosition = Input.mousePosition;
        Camera.main.ScreenToWorldPoint(initialPosition);
    }

    private void OnMouseDrag()
    {
        currentPosition = Input.mousePosition;
        Camera.main.ScreenToWorldPoint(currentPosition);
        Vector3 ballDirection = (currentPosition - initialPosition).normalized;
        float angle = Mathf.Atan2(ballDirection.y, ballDirection.x ) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);
        rotation.z = -rotation.z;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, 1f);
    }

}

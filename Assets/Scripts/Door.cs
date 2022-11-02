using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsOpen = false;
    [SerializeField] private bool OpenToOtherSide = false;
    [SerializeField] private bool IsRotatingDoor = false;
    [SerializeField] private float speed = 1f;

    [Header("Rotation Configs")] [SerializeField]
    private float RotationAmount = 90f;

    [SerializeField] private float ForwardDirection = 0f;

    private Vector3 StartRotation;
    private Vector3 Forward;

    private Coroutine AnimationCoroutine;

    private void Awake()
    {
        StartRotation = transform.rotation.eulerAngles;
        Forward = transform.right;
    }

    public void Open(Vector3 UserPosition)
    {
        Debug.Log("Open");
        if (!IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            if (IsRotatingDoor)
            {
                float dot = Vector3.Dot(Forward, (UserPosition - transform.position).normalized);
                AnimationCoroutine = StartCoroutine(DoRotationOpen(dot));
            }
        }
    }

    private IEnumerator DoRotationOpen(float ForwartAmount)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        if (ForwartAmount >= ForwardDirection)
        {
            endRotation = !OpenToOtherSide
                ? Quaternion.Euler(startRotation * new Vector3(0f, startRotation.y - RotationAmount, 0f))
                : Quaternion.Euler(startRotation * new Vector3(0f, startRotation.y - RotationAmount, 0f) * -1);
        }
        else
        {
            endRotation = !OpenToOtherSide
                ? Quaternion.Euler(startRotation * new Vector3(0f, startRotation.y + RotationAmount, 0f))
                : Quaternion.Euler(startRotation * new Vector3(0f, startRotation.y + RotationAmount, 0f) * -1);
        }

        IsOpen = true;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }

    public void Close()
    {
        if (IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            if (IsRotatingDoor)
            {
                AnimationCoroutine = StartCoroutine(DoRotationClose());
            }
        }
    }

    private IEnumerator DoRotationClose()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(StartRotation);

        IsOpen = false;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }
}
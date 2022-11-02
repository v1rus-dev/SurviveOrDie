using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerActions : MonoBehaviour
    {
        [SerializeField] private TextMeshPro UseText;

        [SerializeField] private Transform LookAt;
        [SerializeField] private float MaxUseDistance = 3f;

        private void Update()
        {
            if (Physics.Raycast(LookAt.position, LookAt.forward, out var hit1, MaxUseDistance) &&
                hit1.collider.TryGetComponent(out Door door1))
            {
                if (door1.IsOpen)
                {
                    UseText.SetText("Close \"E\"");
                }
                else
                {
                    UseText.SetText("Open \"E\"");
                }
                UseText.gameObject.SetActive(true);
                UseText.transform.position = hit1.point - (hit1.point - LookAt.position).normalized * 0.3f;
                UseText.transform.rotation = Quaternion.LookRotation((hit1.point - LookAt.position).normalized);
            }
            else
            {
                UseText.gameObject.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Physics.Raycast(LookAt.position, LookAt.forward, out var hit, MaxUseDistance))
                {
                    if (hit.collider.TryGetComponent(out Door door))
                    {
                        if (door.IsOpen)
                        {
                            door.Close();
                        }
                        else
                        {
                            door.Open(transform.position);
                        }
                    }
                }
            }
        }
    }
}
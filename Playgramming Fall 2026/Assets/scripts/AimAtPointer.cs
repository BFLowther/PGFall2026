using UnityEngine;
using UnityEngine.InputSystem;

public class AimAtPointer : MonoBehaviour
{
	[SerializeField] private Camera cam;
	[SerializeField] private float angleOffset = 0f;

	void Start()
	{
		if (cam == null) cam = Camera.main;
	}

	void Update()
	{
		Vector2 mouseScreen = Mouse.current.position.ReadValue();
		Vector3 mouseWorld = cam.ScreenToWorldPoint(new Vector3(mouseScreen.x, mouseScreen.y, -cam.transform.position.z));

		Vector2 direction = mouseWorld - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

		transform.rotation = Quaternion.Euler(0f, 0f, angle + angleOffset);
	}
}
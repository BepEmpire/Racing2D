using UnityEngine;

public class Boundaries : MonoBehaviour
{
	private float _xRange = 1.175f;

	private void Update()
	{
		if (transform.position.x < -_xRange)
		{
			transform.position = new Vector3(-_xRange, transform.position.y, transform.position.z);
		}

		if (transform.position.x > _xRange)
		{
			transform.position = new Vector3(_xRange, transform.position.y, transform.position.z);
		}
	}
}
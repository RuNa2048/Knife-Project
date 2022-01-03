using UnityEngine;

public class PressurePlate : MonoBehaviour
{
	private void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.TryGetComponent<FlipperKnife>(out var knife))
		{
			knife.Destruction();
		}
	}
}

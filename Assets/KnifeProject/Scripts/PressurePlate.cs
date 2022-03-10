using UnityEngine;

public class PressurePlate : MonoBehaviour
{
	private void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.TryGetComponent<Knife>(out var knife))
		{
			knife.Destructed();
		}
	}
}

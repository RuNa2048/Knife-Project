using UnityEngine;

public abstract class Platform : MonoBehaviour
{
	[SerializeField] private int _id;
	public int ID => _id;

	private PlatformKeeper _platformKeeper;

	private string _playerTag;

	private void Start()
	{
		_playerTag = ConstantsGameTags.PlayerTextTag;
	}

	public void Init(PlatformKeeper platKepper, int id)
	{
		_platformKeeper = platKepper;
		_id = id;
	}

	private void OnTriggerEnter(Collider other)
	{
		FlipperKnife knife = other.GetComponentInParent<FlipperKnife>();

		if (knife && knife.CollisionsIsWork && knife.SaveCheckpoint)
		{
			_platformKeeper.AllowToStand(this);
		}

		//if (other. TryGetComponent<FlipperKnife>(out var knife) && knife.TriggerIsWork)
		//{
		//	_platformKeeper.AllowToStand(this);
		//}
	}
}

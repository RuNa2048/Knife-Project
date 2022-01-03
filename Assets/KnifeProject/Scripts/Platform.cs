using UnityEngine;

public abstract class Platform : MonoBehaviour
{
	private PlatformKeeper _platformKeeper;

	private string _playerTag;

	private void Start()
	{
		_playerTag = ConstantsGameTags.PlayerTextTag;
	}

	public void Init(PlatformKeeper platKepper)
	{
		_platformKeeper = platKepper;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == _playerTag)
		{
			_platformKeeper.AllowToStand(this);
		}
	}
}

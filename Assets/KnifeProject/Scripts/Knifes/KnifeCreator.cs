using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class KnifeCreator : MonoBehaviour
{
	[Header("Knifes Prefabs")] 
	[SerializeField] private Knife _knifePrefab;
	
	[Header("References")]
    //[SerializeField] Knife[] _knifes;
	[SerializeField] PlatformKeeper  _platformKeeper;
    
	public Knife CreateKnife()
	{
		Knife newKnife = Instantiate(_knifePrefab, transform);

		MovingKnife movingKnife = newKnife.GetComponent<MovingKnife>();
		movingKnife.Initialize(_platformKeeper);

		return newKnife;
	}
}

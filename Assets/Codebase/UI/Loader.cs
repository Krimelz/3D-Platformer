using UnityEngine;

namespace Assets.Codebase.UI
{
	public class Loader : MonoBehaviour
	{
		[SerializeField] private GameObject _loader;
		[SerializeField] private float rotationSpeed;

		private void Update()
		{
			_loader.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
		}
	}
}
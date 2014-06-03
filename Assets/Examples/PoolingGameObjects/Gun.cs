using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
	public GameObject bulletPrefab;

	//Optional: Warm the pool and preallocate memory
	void Start()
	{
		PoolManager.WarmPool(bulletPrefab, 20);

		//Notes
		// Make sure the prefab is inactive, or else it will run update before first use
	}

	void Update()
	{
		if(Input.GetButton("Fire1"))
		{
			Vector3 pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
			FireBullet(Camera.main.ScreenToWorldPoint(pos), Quaternion.identity);	
		}
	}

	//Spawn pooled objects
	void FireBullet(Vector3 position, Quaternion rotation)
	{
		var bullet = PoolManager.SpawnObject(bulletPrefab, position, rotation).GetComponent<Bullet>();

		//Notes:
		// bullet.gameObject.SetActive(true) is automatically called on spawn 
		// When done with the instance, you MUST release it!
		// If the number of objects in use exceeds the pool size, new objects will be created
	}

}

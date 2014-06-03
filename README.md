Unity Object Pool
=================

An elegant object pool and manager implementation for Unity

Overview
----

An object pool provides an efficient way to reuse objects, and thus keep the memory foot print of all dynamically created objects within fixed bounds. This is crucial for maintianing consistent framerates in realtime games (especially on mobile), as frequent garbage collection spikes would likley lead to inconsistent performance.

Usage
----
There are two main points of interest:

1. A pool manager class for Unity Game Objects that allows you to easily pool scene objects

2. A generic object pool collection that can be used for non Unity Game Objects. 

###Pooling Unity Game Objects:
PoolManager.cs is a very flexible class that co-ordinates all your pooling requirements in a scene.

It requires no initialization, can be called from anywhere, and dynamically accomodates any prefab game object.

```csharp
//Optional: Warm the pool and preallocate memory
void Start()
{
	PoolManager.WarmPool(bulletPrefab, 50);
	
	//Notes
	// Make sure the prefab is inactive, or else it will run update before first use
}

//Spawn pooled objects
void FireBullet(Vector3 position, Quaternion rotation)
{
	var bullet = PoolManager.SpawnObject(bulletPrefab, position, rotation).GetComponent<Bullet>();
		
	//Notes:
	// bullet.gameObject.SetActive(true) is automatically called on spawn 
	// When done with the instance, you MUST release it!
	// if the number of objects in use exceeds the pool size, new objects will be created
	
}

//In Bullet.cs
void Finish()
{
    PoolManager.ReleaseObject(this.gameObject);
    
    //Notes
    // This takes the gameObject instance, and NOT the prefab instance.
    // Without this call the object will never be available for re-use!
    // gameObject.SetActive(false) is automatically called;
}

```

###Pooling C# objects:
This allows you to pool objects not derived from the Unity engine. In fact if you replaced the Debug statemetns you could use this in any other .NET or C# project.

This is the backbone of the PoolManager.cs class, but you can use it directly. For instance you could use it to pool events in a memory friendly observer pattern:
```csharp
//The factoryFunc (first arg) is the crux of the ObjectPool class. 
//It privodes a way for the ObjectPool to dynamically create new objects
eventPool = new ObjectPool<DelayedEvent>(()=> new DelayedEvent(), 5);

var evt = eventPool.GetItem();
evt.Start(Time.time, delay); //Configure object

//On event done:
eventPool.ReleaseItem(evt);
	
```

History
----
Created by Peter Cardwell-Gardner

Born out of the realization that he was writing the samething over and over again, this was designed to be the last object pool you ever need. 

This project is used extensively in the upcoming Unity game *[Cadence](http://www.playcadence.com)* by [Made With Monster Love](http://www.madewithmonsterlove.com). 

Licence
---
MIT
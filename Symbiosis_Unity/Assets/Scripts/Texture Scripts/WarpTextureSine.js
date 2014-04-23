#pragma strict

// animates main texture coordinates over time

function Start ()
{

}

function Update ()
{

	var offset = Time.time * 0.0015f;

	// animate texture roams in slow orbiting loops
	renderer.material.mainTextureOffset.x = Mathf.Sin(offset);
	renderer.material.mainTextureOffset.y = Mathf.Cos(offset);
	
	//renderer.material.mainTextureOffset = Vector2 (offset, 0); //old

}
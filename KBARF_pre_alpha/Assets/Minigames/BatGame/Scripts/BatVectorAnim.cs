using UnityEngine;
using System.Collections;
using Vectrosity;

[System.Serializable]
public class BatVectorAnim {

	public BatVectorSprite[] sprites;

	private int startFrame = 0;
	private int endFrame = 0;
	private bool animLoop = false;

	private float animSpeed = 0.0f;
	private float animInterval = 0.0f;

	private int currentFrame = 0;

	// Use this for initialization
	public void InitSprites () {
		foreach (BatVectorSprite e in sprites)
		{
			e.InitSprite();
		}
	}

	public void PlayOnce(int s, int e, float speed)
	{
		animLoop = false;

		startFrame = s;
		endFrame = e;

		animSpeed = speed;

		animLoop = false;

		if (s < e) 	currentFrame = s;
		else 		currentFrame = e;

		animInterval = animSpeed;
	}

	public void PlayLoop(int s, int e, float speed)
	{
		PlayOnce (s, e, speed);

		animLoop = true;
	}

	private void Play()
	{
		if (animInterval > 0.0f)
		{
			animInterval -= Time.deltaTime;
		}
		else
		{
			if (animSpeed == 0.0f) return;

			animInterval = animSpeed;

			if (animSpeed > 0.0f && currentFrame <= endFrame)
			{
				currentFrame++;
			}
			else if (animSpeed < 0.0f && currentFrame >= startFrame)
			{
				currentFrame--;
			}

			// If loop or no loop in reverse
			if (currentFrame < startFrame && animSpeed < 0.0f)
			{
				if (animLoop) currentFrame = endFrame;
				else currentFrame = startFrame;
			}
			else if (currentFrame > endFrame && animSpeed > 0.0f)
			{
				
				if (animLoop) currentFrame = startFrame;
				else currentFrame = endFrame;
			}
		}
	}

	public void Draw(Vector2 pos) {
		if (sprites.Length == 0) return;

		Play ();
		
		for (int i = 0; i < sprites.Length; i++)
		{
			if (i == currentFrame)
			{
				sprites[currentFrame].Draw (pos);
			}
			else
			{
				sprites[i].Hide();
			}
		}
	}

	public void Delete() {
		for (int i = 0; i < sprites.Length; i++)
		{
			sprites[i].Delete();
		}
	}

	public void Explode(Vector2 pos) {
		animSpeed = 0.0f;

		for (int i = 0; i < sprites.Length; i++)
		{
			sprites[i].Explode(pos);
		}
	}

	public void Scale(float s) {
		for (int i = 0; i < sprites.Length; i++)
		{
			sprites[i].Scale(s);
		}
	}
}

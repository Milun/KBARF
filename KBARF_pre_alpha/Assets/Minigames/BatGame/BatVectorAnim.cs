using UnityEngine;
using System.Collections;
using Vectrosity;

[System.Serializable]
public class BatVectorAnim {

	public BatVectorSprite[] sprites;

	// Use this for initialization
	public void InitSprites () {
		foreach (BatVectorSprite e in sprites)
		{
			e.InitSprite();
		}
	}

	public void Draw(Vector2 pos) {
		if (sprites.Length == 0) return;
		
		sprites[((int)Time.frameCount/10) % sprites.Length].Draw (pos);
	}
}

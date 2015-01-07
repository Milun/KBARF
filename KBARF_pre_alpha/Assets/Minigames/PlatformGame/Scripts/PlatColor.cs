using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatColor : MonoBehaviour {

	SpriteRenderer spriteRenderer;

	public enum SpriteColor {
		WHITE,
		RED,
		PINK,
		ORANGE,
		YELLOW,
		GREEN_DARK,
		GREEN_LIGHT,
		BLUE,
		PURPLE_LIGHT,
		PURPLE_DARK,
		MAGENTA,
		BROWN,
		RANDOM
	};

	[SerializeField] SpriteColor spriteColor;

	// Use this for initialization
	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void Start () {
		if (spriteRenderer == null || spriteColor == null) return;

		spriteRenderer.color = GetColor (spriteColor);
	}

	public SpriteColor SprColor
	{
		get
		{
			return spriteColor;
		}
	}

	public Color GetColor (SpriteColor sc)
	{
		switch (sc)
		{
		case SpriteColor.WHITE:
			return new Color32(255, 255, 255, 255);
			break;
			
		case SpriteColor.RED:
			return new Color32(136, 57, 50, 255);
			break;
			
		case SpriteColor.PINK:
			return new Color32(184, 105, 98, 255);
			break;
			
		case SpriteColor.ORANGE:
			return new Color32(139, 84, 41, 255);
			break;
			
		case SpriteColor.YELLOW:
			return new Color32(191, 206, 114, 255);
			break;
			
		case SpriteColor.GREEN_LIGHT:
			return new Color32(148, 224, 137, 255);
			break;
			
		case SpriteColor.GREEN_DARK:
			return new Color32(85, 160, 73, 255);
			break;
			
		case SpriteColor.BLUE:
			return new Color32(103, 182, 189, 255);
			break;
			
		case SpriteColor.PURPLE_LIGHT:
			return new Color32(120, 105, 196, 255);
			break;
			
		case SpriteColor.PURPLE_DARK:
			return new Color32(64, 49, 141, 255);
			break;
			
		case SpriteColor.MAGENTA:
			return new Color32(139, 63, 150, 255);
			break;
			
		case SpriteColor.BROWN:
			return new Color32(87, 66, 0, 255);
			break;
			
		default:
			return new Color32(255, 255, 255, 255);
			break;
		}
	}
}

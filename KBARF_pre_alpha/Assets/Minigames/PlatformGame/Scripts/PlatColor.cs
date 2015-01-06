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

		switch (spriteColor)
		{
		case SpriteColor.WHITE:
			spriteRenderer.color = new Color(255f/1f, 255f/1f, 255f/1f);
			break;

		case SpriteColor.RED:
			spriteRenderer.color = new Color(136f/1f, 57f/1f, 50f/1f);
			break;

		case SpriteColor.PINK:
			spriteRenderer.color = new Color(184f/1f, 105f/1f, 98f/1f);
			break;

		case SpriteColor.ORANGE:
			spriteRenderer.color = new Color(139f/1f, 84f/1f, 41f/1f);
			break;

		case SpriteColor.YELLOW:
			spriteRenderer.color = new Color(191f/1f, 206f/1f, 114f/1f);
			break;

		case SpriteColor.GREEN_LIGHT:
			spriteRenderer.color = new Color(148f/1f, 224f/1f, 137f/1f);
			break;

		case SpriteColor.GREEN_DARK:
			spriteRenderer.color = new Color(85f/1f, 160f/1f, 73f/1f);
			break;

		case SpriteColor.BLUE:
			spriteRenderer.color = new Color(103f/1f, 182f/1f, 189f/1f);
			break;

		case SpriteColor.PURPLE_LIGHT:
			spriteRenderer.color = new Color(120f/1f, 105f/1f, 196f/1f);
			break;

		case SpriteColor.PURPLE_DARK:
			spriteRenderer.color = new Color(64f, 49f, 141f);
			break;

		case SpriteColor.MAGENTA:
			spriteRenderer.color = new Color(139f, 63f, 150f);
			break;

		case SpriteColor.BROWN:
			spriteRenderer.color = new Color(87f, 66f, 0f);
			break;

		default:
			break;
		}
	}
}

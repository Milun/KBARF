using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Anim : MonoBehaviour
{
	// Use this for initialization
	void Awake ()
	{

	}
	
	void Start()
	{

	}
	
	/**
	 * Returns a value from 0 - 1 which is the percentage of where the current animation is in being played.
	 **/
	public float GetAnimPercent(string animName)
	{
		if (animation.GetClip(animName) == null) {Debug.Log(this.gameObject.name + " missing " + animName); return -1.0f;}

		if (!animation.IsPlaying(animName))
		{
			return 1.0f;
		}
		
		return animation[animName].time/animation[animName].length;
	}

	/**
	 * Return the length of the animation.
	 */
	public float GetAnimLength(string animName)
	{
		if (animation.GetClip(animName) == null) {Debug.Log(this.gameObject.name + " missing " + animName); return -1.0f;}

		return animation[animName].clip.length;
	}

	/**
	 * Check if the animation is still playing.
	 */
	public bool AnimEnded(string animName)
	{
		if (animation.GetClip(animName) == null) {Debug.Log(this.gameObject.name + " missing " + animName); return false;}

		return (animation[animName].time >= animation[animName].length);
	}

	/**
	 * Just plays the animation. It smooths the length = to transitionTime.
	 */
	public void PlayAnimation(string animName, float newLength, float transitionTime = 0.05f)
	{
		if (animation.GetClip(animName) == null) {Debug.Log(this.gameObject.name + " missing " + animName); return;}

		animation[animName].time = 0.0f;
		animation[animName].speed = animation[animName].length / newLength;

		animation.CrossFade (animName, transitionTime);
	}

	/**
	 * Will keep looping the animation if being called.
	 */
	public void PlayAnimationLooped(string animName, float newLength, float transitionTime = 0.05f)
	{
		if (animation.GetClip(animName) == null) {Debug.Log(this.gameObject.name + " missing " + animName); return;}
		
		animation[animName].speed = animation[animName].length / newLength;
		animation.CrossFade (animName, transitionTime);
	}

	/**
	 * Sets the time period in the animation
	 */
	public void SetAnimationTime(string animName, float time = 0.0f)
	{
		if (animation.GetClip(animName) == null) {Debug.Log(this.gameObject.name + " missing " + animName); return;}

		animation[animName].time = time;
	}

	/**
	 * Play a single frame. The frame that is played at time % of the animation.
	 */
	public void PlayAnimationFrame(string animName, float time = 0.0f, float transitionTime = 0.05f)
	{
		if(animation.GetClip(animName) == null)
		{
			Debug.Log(animName);
		}

		animation.CrossFade (animName, transitionTime);
		animation[animName].speed = 0.0f;
		animation[animName].normalizedTime = time;
	}
}

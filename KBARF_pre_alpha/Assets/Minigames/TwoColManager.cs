using UnityEngine;
using System.Collections.Generic;

public class TwoColManager : MonoBehaviour {
	
	[SerializeField] private List<TwoCol> twoCol = new List<TwoCol>();

	public TwoCol CheckCol(TwoCol other)
	{
		foreach (TwoCol e in twoCol)
		{
			if (e == other)
			{
				continue;
			}

			if (other.CheckCol(e))
			{
				return e;
			}
		}

		// No collisions.
		return null;
	}

	public void AddCol(TwoCol pass)
	{
		twoCol.Add (pass);
	}

	public void RemoveCol(TwoCol pass)
	{
		twoCol.Remove (pass);
	}
}

using UnityEngine;
using System.Collections.Generic;

public class TwoColManager : MonoBehaviour {
	
	[SerializeField] private List<TwoCol> twoCol = new List<TwoCol>();

	public struct Col {
		public TwoCol 	col;
		public Vector2 	move;
	};

	public List<Col> CheckCol(TwoCol other)
	{
		List<Col> output = new List<Col>();

		foreach (TwoCol e in twoCol)
		{
			if (e == other ||
			    (other.Type == TwoCol.ColType.PHYSICS_TAKE && e.Type != TwoCol.ColType.PHYSICS_GIVE) ||
			    (other.Type == TwoCol.ColType.OFFENSE_TAKE && e.Type != TwoCol.ColType.DEFENCE_GIVE)
			    )
			{
				continue;
			}

			Col col = new Col();
			Vector2 vec = other.CheckCol(e);
			if (vec != Vector2.zero)
			{
				col.col = e;
				col.move = vec;

				output.Add ( col );
			}
		}

		// No collisions.
		return output;
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

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
			if (e == other || !e.CanCollide(other))
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

	// This is specific to squares.
	public Vector2 CheckColPhys(TwoColSquare other, ref TwoCommon tCommon)
	{
		Vector2 output = Vector2.zero;
		
		foreach (TwoCol e in twoCol)
		{
			if (e == other || !e.CanCollide(other))
			{
				continue;
			}

			Vector2 vec = Vector2.zero;
			if (e.GetType() == typeof(TwoColSquare))
			{
				vec = other.CheckColSquarePhys((TwoColSquare)e, ref tCommon);
			}
			else
			{
				vec = other.CheckCol (e);
				tCommon.Pos += vec;
			}

			if (vec != Vector2.zero)
			{
				output += vec;
			}
		}
		
		// No collisions.
		return output;
	}

	public Vector2 CheckColMove(TwoCol other, TwoCol.ColType type)
	{
		Vector2 output = Vector2.zero;
	
		foreach (TwoCol e in twoCol)
		{
			if (e == other || !e.HasType(type) || !e.CanCollide(other))
			{
				continue;
			}

			output += other.CheckCol(e);
		}

		// No collisions.
		return output;
	}

	public TwoCol IsCol(TwoCol other, TwoCol.ColType type)
	{
		foreach (TwoCol e in twoCol)
		{
			if (e == other || !e.HasType(type) || !e.CanCollide(other))
			{
				continue;
			}

			if (other.CheckCol(e) != Vector2.zero)
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

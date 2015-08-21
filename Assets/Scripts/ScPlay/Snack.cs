using UnityEngine;
using System.Collections;

public class Snack : Equipment 
{
	private int snackCode;
	
	public void initGrandChild (int snackCode_)
	{
		snackCode = snackCode_;
	}
}

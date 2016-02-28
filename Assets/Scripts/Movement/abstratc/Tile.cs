using UnityEngine;
using System.Collections;

public abstract  class Tile : MonoBehaviour {
    
    public abstract void Move(float force);
    public abstract void Clear();
}

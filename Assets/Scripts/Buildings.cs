using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{
    public List<GameObject> _buildables = new List<GameObject>();
    public List<GameObject> _builtObjects = new List<GameObject>(); // Buildings we've already built
}

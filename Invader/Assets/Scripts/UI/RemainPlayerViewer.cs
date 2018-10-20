using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemainPlayerViewer : MonoBehaviour
{
    /// <summary>
    /// Playerの残機を表すSpriteの表示
    /// </summary>
    [SerializeField]
    private Image[] remainHearts = null;

	public void SetRemain(int remain)
	{
		if (remain == 0)
		{
			return;
		}

        for (int i = 0; i < remainHearts.Length; i++)
		{
			bool isActive = i < remain - 1; //表示するか
            remainHearts[i].enabled = isActive;
		}
	}
}

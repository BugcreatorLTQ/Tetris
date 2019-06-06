using UnityEngine;

/// <summary>
/// 方块AI
/// </summary>
public class CubeAI : MonoBehaviour
{
    /// <summary>
    /// 是否可操控
    /// </summary>
    public bool consoleAble = true;

    /// <summary>
    /// 下坠计时器
    /// </summary>
    public int fall_count = 0;

    /// <summary>
    /// 下坠速度
    /// </summary>
    [Range(5, 20)]
    public int fall_speed = 5;

    void Update()
    {
        if (!consoleAble)
        {
            return;
        }
        // 下落
        fall_count++;
        if (fall_count == 20 - fall_speed)
        {
            fall_count = 0;
            GetComponent<CubeMove>().MoveDown();
        }
    }


}

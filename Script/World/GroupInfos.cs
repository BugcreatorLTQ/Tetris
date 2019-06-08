using UnityEngine;

/// <summary>
/// 方块组信息
/// </summary>
public class GroupInfos : MonoBehaviour
{

    /// <summary>
    /// 方块样式个数
    /// </summary>
    public int infoSize;

    /// <summary>
    /// 方块样式
    /// </summary>
    public bool[][,,] infos;

    /// <summary>
    /// 一维数组转换为方块信息
    /// </summary>
    /// <param name="input">输入信息</param>
    /// <returns>方块信息</returns>
    private bool[,,] ArrayToInfo(int[] input)
    {
        bool[,,] info = new bool[3, 3, 3];
        int index = 0;
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                for (int k = 0; k < 3; k++)
                    info[i, j, k] = input[index++] != 0;
        return info;
    }

    /// <summary>
    /// 设置方块样式
    /// </summary>
    public void Start()
    {
        infoSize = 4;
        infos = new bool[infoSize][,,];
        infos[0] = ArrayToInfo(new int[] {
            0,0,1,
            0,0,1,
            0,0,0,

            0,0,0,
            0,1,1,
            0,0,0,

            0,0,0,
            0,1,0,
            0,0,0
        });
        infos[1] = ArrayToInfo(new int[] {
            0,1,0,
            0,0,0,
            0,0,0,

            0,0,1,
            0,1,0,
            1,0,0,

            0,0,0,
            0,0,0,
            0,0,0
        });
        infos[2] = ArrayToInfo(new int[] {
            0,0,1,
            0,0,1,
            0,0,0,

            0,0,0,
            0,1,1,
            0,0,0,

            0,0,0,
            0,1,0,
            0,0,0
        });
        infos[3] = ArrayToInfo(new int[] {
            0,1,0,
            0,0,0,
            0,0,0,

            0,0,1,
            0,1,0,
            1,0,0,

            0,0,0,
            0,0,0,
            0,0,0
        });
    }
}

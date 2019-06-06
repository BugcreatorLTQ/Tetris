using UnityEngine;

/// <summary>
/// 方块组AI
/// </summary>
public class GroupAI : MonoBehaviour
{

    /// <summary>
    /// 是否可操控
    /// </summary>
    public bool consoleAble = true;

    /// <summary>
    /// 方块组信息
    /// </summary>
    private bool[,,] info;

    /// <summary>
    /// 碰触
    /// </summary>
    public void Touch()
    {
        consoleAble = false;
        foreach (Cube cube in GetComponentsInChildren<Cube>())
        {
            cube.GetComponent<CubeAI>().consoleAble = false;
            if (cube.transform.position.y > 10)
            {
                transform.parent.GetComponent<BaseAI>().GameOver();
            }
            // cube父物体改为world
             cube.transform.SetParent(transform.parent);
        }
        // 保存baseAI信息
        BaseAI baseAI = transform.parent.GetComponent<BaseAI>();
        // 消除旧Group
        Destroy(transform.gameObject);
        // 创建新的Group
        baseAI.CrateGroup();
    }


    /// <summary>
    /// 初始化方块组
    /// </summary>
    public void Init(GameObject CubePre)
    {
        info = new bool[3, 3, 3];
        bool flag = true;
        while (flag)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        info[i, j, k] = Random.Range(0, 9) == 0;
                        if (info[i, j, k])
                        {
                            flag = false;
                            GameObject cube = GameObject.Instantiate(CubePre, transform);
                            cube.transform.localPosition = new Vector3(k - 0.5f, i, j - 0.5f);
                        }
                    }
                }
            }
        }
    }

}

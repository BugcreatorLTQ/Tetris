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
    /// 方块预制件
    /// </summary>
    private GameObject Cubepre;

    /// <summary>
    /// 方块组信息
    /// </summary>
    public bool[,,] info;

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
        if (fall_count == 30 - fall_speed)
        {
            fall_count = 0;
            GetComponent<GroupMove>().MoveDown();
        }
    }

    void Start()
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
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 设置方块预制件
    /// </summary>
    /// <param name="_Cubepre">方块预制件</param>
    public void SetCubePre(GameObject _Cubepre)
    {
        Cubepre = _Cubepre;
    }

    /// <summary>
    /// 碰触
    /// </summary>
    public void Touch()
    {
        consoleAble = false;
        foreach (Cube cube in GetComponentsInChildren<Cube>())
        {
            if (cube.transform.position.y > 20)
            {
                transform.parent.GetComponent<BaseAI>().GameOver();
                break;
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
    public void Init()
    {
        GroupInfos groupInfos = transform.parent.GetComponent<GroupInfos>();
        // 销毁当前方块
        foreach(Cube cube in GetComponentsInChildren<Cube>())
        {
            Destroy(cube.gameObject);
        }
        // 随机选择一个结构
        info = groupInfos.infos[Random.Range(0, groupInfos.infoSize)];
        // 构建新方块
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (info[i, j, k])
                    {
                        GameObject cube = GameObject.Instantiate(Cubepre, transform);
                        // 相对于中心的位置生成方块
                        cube.transform.localPosition = new Vector3(k - 0.5f - 1, i - 1, j - 0.5f - 1);
                    }
                }
            }
        }

    }

}

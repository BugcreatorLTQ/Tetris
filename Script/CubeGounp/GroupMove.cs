using UnityEngine;

/// <summary>
/// 方块组移动
/// </summary>
public class GroupMove : MonoBehaviour
{
    /// <summary>
    /// 方块信息维度1
    /// </summary>
    private Cube[] info_1;

    /// <summary>
    /// 方块信息维度2
    /// </summary>
    private Cube[] info_2;

    /// <summary>
    /// 方块信息维度3
    /// </summary>
    private Cube[] info_3;

    /// <summary>
    /// 方块信息维度4
    /// </summary>
    private Cube[] info_4;


    void Start()
    {
        info_1 = new Cube[10];
        info_2 = new Cube[10];
        info_3 = new Cube[10];
        info_4 = new Cube[10];
        // 初始化方块信息数组
        for (int i = 0; i < 10; i++)
        {
            info_1[i] = null;
            info_2[i] = null;
            info_3[i] = null;
            info_4[i] = null;
        }
        // 记录方块信息
        foreach (Transform trans in transform.parent)
        {
            print(trans);
            Cube cube = trans.GetComponent<Cube>();
            if (!cube)
                continue;
            Vector3 posi = cube.transform.position;
            int x = (int)(posi.x + 4.5), z = (int)(posi.z + 4.5);
            if (x >= 10 || z >= 10)
            {
                continue;
            }
            if (info_1[x] == null || info_1[x].transform.position.y < posi.y)
                info_1[x] = cube.GetComponent<Cube>();
            if (info_2[z] == null || info_2[z].transform.position.y < posi.y)
                info_2[z] = cube.GetComponent<Cube>();
            if (info_3[x] == null || info_3[x].transform.position.y > posi.y)
                info_3[x] = cube.GetComponent<Cube>();
            if (info_4[z] == null || info_4[z].transform.position.y > posi.y)
                info_4[z] = cube.GetComponent<Cube>();
        }
    }

    /// <summary>
    /// 左移
    /// </summary>
    public void MoveLeft()
    {
        foreach (Cube cube in GetComponentsInChildren<Cube>())
        {
            if (Mathf.Abs(Mathf.Abs(cube.transform.position.x) - 4.5f) < 1e-2 ||
                Mathf.Abs(Mathf.Abs(cube.transform.position.z) - 4.5f) < 1e-2)
            {
                return;
            }
        }
        foreach (CubeMove cube in GetComponentsInChildren<CubeMove>())
        {
            cube.MoveLeft();
        }
    }

    /// <summary>
    /// 右移
    /// </summary>
    public void MoveRight()
    {
        foreach (Cube cube in GetComponentsInChildren<Cube>())
        {
            if (Mathf.Abs(Mathf.Abs(cube.transform.position.x) - 4.5f) < 1e-2 ||
                Mathf.Abs(Mathf.Abs(cube.transform.position.z) - 4.5f) < 1e-2)
            {
                return;
            }
        }
        foreach (CubeMove cube in GetComponentsInChildren<CubeMove>())
        {
            cube.MoveRight();
        }
    }

    /// <summary>
    /// 下移
    /// </summary>
    public void MoveDown()
    {
        foreach (CubeMove cube in GetComponentsInChildren<CubeMove>())
        {
            cube.MoveDown();
        }
    }

    /// <summary>
    /// 判断
    /// </summary>
    public void Judge()
    {
        // 获取摄像机
        Camera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        // 判断方向
        bool X = (int)camera.transform.forward.z != 0;
        // 维度方块信息
        Cube[] info = X?info_3:info_4;
        bool clear_flag = true;
        // 查看是的满足消除条件
        foreach(Cube cube in info)
        {
            if (!cube || cube.transform.position.y != 0)
            {
                clear_flag = false;
                break;
            }
        }
        // 清除最后一面
        if (clear_flag)
        {
            transform.parent.GetComponent<BaseAI>().Clear();
        }
        info = X ? info_1 : info_2;
        // 控制方块信息
        foreach (Cube cube in GetComponentsInChildren<Cube>())
        {
            Vector3 posi = cube.transform.position;
            int x = (int)(posi.x + 4.5), z = (int)(posi.z + 4.5);
            int temp = X ? x : z;
            if (Mathf.Abs(cube.transform.position.y) < 0.1 || ((temp < 10) && (info[temp] != null) && (cube.transform.position.y - info[temp].transform.position.y < 1.2)))
            {
                GetComponent<GroupAI>().Touch();
                break;
            }
        }

    }

}

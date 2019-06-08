using UnityEngine;

/// <summary>
/// 方块组移动
/// </summary>
public class GroupMove : MonoBehaviour
{

    /// <summary>
    /// 摄像机组件
    /// </summary>
    private new Camera camera;

    /// <summary>
    /// 获取摄像机变换组件
    /// </summary>
    private Transform camTran { get { return camera.transform; } }

    /// <summary>
    /// GroupAI组件
    /// </summary>
    private GroupAI groupAI;

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

    /// <summary>
    /// 界面大小
    /// </summary>
    private int size = 20;

    void Start()
    {
        // 获取摄像机组件
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        // 获取GroupAI组件
        groupAI = GetComponent<GroupAI>();
        // 初始话维度矩阵
        info_1 = new Cube[20];
        info_2 = new Cube[20];
        info_3 = new Cube[20];
        info_4 = new Cube[20];
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
            Cube cube = trans.GetComponent<Cube>();
            if (!cube)
                continue;
            Vector3 posi = cube.transform.position;
            int x = (int)(posi.x + (size / 2 - 0.5)), z = (int)(posi.z + (size / 2 - 0.5));
            if (x >= size || z >= size)
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
            if (Mathf.Abs(Mathf.Abs(cube.transform.position.x) - (size / 2 - 0.5f)) < 1e-2 ||
                Mathf.Abs(Mathf.Abs(cube.transform.position.z) - (size / 2 - 0.5f)) < 1e-2)
            {
                return;
            }
        }
        transform.Translate(-camTran.right);
    }

    /// <summary>
    /// 右移
    /// </summary>
    public void MoveRight()
    {
        foreach (Cube cube in GetComponentsInChildren<Cube>())
        {
            if (Mathf.Abs(Mathf.Abs(cube.transform.position.x) - (size / 2 - 0.5f)) < 1e-2 ||
                Mathf.Abs(Mathf.Abs(cube.transform.position.z) - (size / 2 - 0.5f)) < 1e-2)
            {
                return;
            }
        }
        transform.Translate(camTran.right);
    }

    /// <summary>
    /// 下移
    /// </summary>
    public void MoveDown()
    {
        transform.Translate(-camTran.up);
    }

    /// <summary>
    /// 将一个二维平面旋转
    /// </summary>
    /// <param name="info">平面信息</param>
    /// <param name="dire">旋转方向(true=左,false=右)</param>
    private void Ratate2D(ref bool[,] info, bool dire)
    {
        void copy(bool a, bool b, bool c, out bool ta, out bool tb, out bool tc)
        {
            ta = a;
            tb = b;
            tc = c;
        }
        if (dire)
        {
            copy(info[0, 0], info[0, 1], info[0, 2], out bool A, out bool B, out bool C);
            copy(info[0, 2], info[1, 2], info[2, 2], out info[0, 0], out info[0, 1], out info[0, 2]);
            copy(info[2, 2], info[2, 1], info[2, 0], out info[0, 2], out info[1, 2], out info[2, 2]);
            copy(info[2, 0], info[1, 0], info[0, 0], out info[2, 2], out info[2, 1], out info[2, 0]);
            copy(A, B, C, out info[2, 0], out info[1, 0], out info[0, 0]);
        }
        else
        {
            copy(info[0, 0], info[0, 1], info[0, 2], out bool A, out bool B, out bool C);
            copy(info[2, 0], info[1, 0], info[0, 0], out info[0, 0], out info[0, 1], out info[0, 2]);
            copy(info[2, 2], info[2, 1], info[2, 0], out info[2, 0], out info[1, 0], out info[0, 0]);
            copy(info[0, 2], info[1, 2], info[2, 2], out info[2, 2], out info[2, 1], out info[2, 0]);
            copy(A, B, C, out info[0, 2], out info[1, 2], out info[2, 2]);
        }
    }

    /// <summary>
    /// 旋转方块组
    /// </summary>
    /// <param name="dire">旋转方向(true=左,false=右)</param>
    private void Rotate(bool dire)
    {
        Vector3 forward = camera.transform.forward;
        bool[,,] info = groupAI.info;
        bool[][,] info2D = new bool[3][,];
        // 初始化
        for(int i = 0; i < 3; i++)
        {
            info2D[i] = new bool[3,3];
        }
        // 切片
        for (int lv = 0; lv < 3; lv++)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    info2D[lv][i,j] = forward.z == 0 ? info[lv, i, j] : info[i, j, lv];
        }
        // 旋转
        for (int i = 0; i < 3; i++)
        {
            Ratate2D(ref info2D[0], dire);
        }
        // 拼接
        for (int lv = 0; lv < 3; lv++)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (forward.z == 0)
                    {
                        info[lv, i, j] = info2D[lv][i, j];
                    }
                    else
                    {
                        info[i, j, lv] = info2D[lv][i, j];
                    }
        }
    }

    /// <summary>
    /// 左转
    /// </summary>
    public void RotateLeft()
    {
        Rotate(true);
        groupAI.Init();
    }

    /// <summary>
    /// 右转
    /// </summary>
    public void RotateRight()
    {
        Rotate(false);
        groupAI.Init();
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
        Cube[] info = X ? info_3 : info_4;
        bool clear_flag = true;
        // 查看是的满足消除条件
        foreach (Cube cube in info)
        {
            if (!cube || cube.transform.position.y != 0)
            {
                clear_flag = false;
                break;
            }
        }
        info = X ? info_1 : info_2;
        // 清除最后一面
        if (clear_flag)
        {
            transform.parent.GetComponent<BaseAI>().Clear();
        }
        // 控制方块信息
        foreach (Cube cube in GetComponentsInChildren<Cube>())
        {
            Vector3 posi = cube.transform.position;
            int x = (int)(posi.x + (size / 2 - 0.5)), z = (int)(posi.z + (size / 2 - 0.5));
            int temp = X ? x : z;
            // 判断是否满足碰撞条件
            if (Mathf.Abs(cube.transform.position.y) < 0.1 // 到达最底层
                || (info[temp] && (cube.transform.position.y - info[temp].transform.position.y < 1.1))) // y轴距离相差1
            {
                GetComponent<GroupAI>().Touch();
                break;
            }
        }

    }

}

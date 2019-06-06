using UnityEngine;

/// <summary>
/// 基础AI
/// </summary>
public class BaseAI : MonoBehaviour
{
    /// <summary>
    /// 方块预制件
    /// </summary>
    public GameObject CubePre;

    void Start()
    {
        CrateGroup();
    }

    /// <summary>
    /// 创建方块组
    /// </summary>
    public void CrateGroup()
    {
        // 随机选择创建位置
        GameObject group = new GameObject("CubeGroup");
        // 设置父组件
        group.transform.SetParent(transform);
        // 暂时固定位置
        group.transform.position = new Vector3(0, 9, 0);
        // 添加组件
        group.AddComponent<Group>();
        // 初始化
        group.GetComponent<GroupAI>().Init(CubePre);
    }

    /// <summary>
    /// 游戏结束提示信息
    /// </summary>
    public void GameOver()
    {
        foreach(GroupAI group in transform)
        {
            Destroy(group.gameObject);
        }
    }


    /// <summary>
    /// 清除最后一面
    /// </summary>
    public void Clear()
    {
        // 清除最后一面
        foreach (Cube cube in GetComponents<Cube>())
        {
            if (Mathf.Abs(cube.transform.position.y)< 1e-2){
                Destroy(cube.gameObject);
            }
        }
        // 其余方块向下移动一格
        foreach(Cube cube in GetComponents<Cube>())
        {
            cube.transform.Translate(Vector3.down);
        }
    }
}

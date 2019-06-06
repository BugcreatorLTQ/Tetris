using UnityEngine;

/// <summary>
/// 方块移动
/// </summary>
public class CubeMove : MonoBehaviour
{

    /// <summary>
    /// 摄像机组件
    /// </summary>
    private new static Camera camera;

    /// <summary>
    /// 获取摄像机变换组件
    /// </summary>
    private Transform camTran { get { return camera.transform; } }

    /// <summary>
    /// 获取操作性
    /// </summary>
    private bool consoleAble { get => GetComponent<CubeAI>().consoleAble; }

    private void Start()
    {
        // 获取摄像机组件
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (!camera)
        {
            throw new System.Exception("摄像机组件未找到");
        }
    }

    /// <summary>
    /// 左移
    /// </summary> 
    public void MoveLeft()
    {
        if (!consoleAble)
        {
            return;
        }
        Vector3 posi = transform.position;
        transform.Translate(-camTran.right);
    }

    /// <summary>
    /// 右移
    /// </summary>
    public void MoveRight()
    {
        if (!consoleAble)
        {
            return;
        }
        transform.Translate(camTran.right);
    }

    /// <summary>
    /// 下移
    /// </summary>
    public void MoveDown()
    {
        if (!consoleAble)
        {
            return;
        }
        transform.Translate(-camTran.up);
    }

}

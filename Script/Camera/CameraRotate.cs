using UnityEngine;

/// <summary>
/// 转动镜头
/// </summary>
public class CameraRotate : MonoBehaviour
{
    /// <summary>
    /// 镜头移动状态
    /// </summary>
    enum MoveStatus
    {
        NONE,
        LEFT,
        RIGHT
    }

    /// <summary>
    /// 镜头移动状态
    /// </summary>
    private MoveStatus status = MoveStatus.NONE;

    /// <summary>
    /// 镜头旋转速度
    /// </summary>
    [SerializeField]
    private int rotateSpeed = 1;

    /// <summary>
    /// 计数器 用于控制旋转角度
    /// </summary>
    private int count = 0;

    private void Start()
    {
        // 检查旋转速度
        if (90 % rotateSpeed != 0)
        {
            throw new System.Exception("旋转速度必须被90整除！");
        }
    }

    private void Update()
    {
        if (status == MoveStatus.NONE)
        {
            // 从键盘读取输入
            GetKey();
        }
        // 旋转镜头
        Rotate();
    }

    /// <summary>
    /// 按下按键时的操作
    /// </summary>
    private void GetKey()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            status = MoveStatus.LEFT;
            Time.timeScale = 0;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            status = MoveStatus.RIGHT;
            Time.timeScale = 0;
        }
    }

    /// <summary>
    /// 镜头旋转
    /// </summary>
    private void Rotate()
    {
        switch (status)
        {
            case MoveStatus.NONE:
                break;
            case MoveStatus.LEFT:
                transform.RotateAround(Vector3.zero, Vector3.up, rotateSpeed);
                count++;
                break;
            case MoveStatus.RIGHT:
                transform.RotateAround(Vector3.zero, Vector3.up, -rotateSpeed);
                count++;
                break;
        }
        if (count == 90 / rotateSpeed)
        {
            count = 0;
            status = MoveStatus.NONE;
            Time.timeScale = 1;
        }
    }
}

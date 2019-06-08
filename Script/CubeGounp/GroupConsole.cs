using UnityEngine;

/// <summary>
/// 方块组控制
/// </summary>
public class GroupConsole : MonoBehaviour
{

    /// <summary>
    /// 方块组移动组件
    /// </summary>
    private GroupMove groupMove;

    void Start()
    {
        groupMove = GetComponent<GroupMove>();
        if (!groupMove)
        {
            throw new System.Exception("未找到方块移动组件");
        }
    }

    void Update()
    {
        groupMove.Judge();
        if (!GetComponent<GroupAI>().consoleAble)
        {
            return; 
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            groupMove.MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            groupMove.MoveRight();
        }
        if (Input.GetKey(KeyCode.S))
        {
            groupMove.MoveDown();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            groupMove.RotateLeft();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            groupMove.RotateRight();
        }
    }

}

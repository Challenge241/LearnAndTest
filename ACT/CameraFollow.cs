using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 这是主角的 Transform 组件
    public float smoothSpeed = 0.125f; // 平滑速度，决定相机移动的速度
    public Vector3 offset; // 偏移，用于控制相机相对于主角的位置
    public bool isFollowing = true;

    void FixedUpdate() // 使用 FixedUpdate 而不是 Update 可以得到更稳定的跟随效果
    {
        if (isFollowing && target != null)
        {
            // 设定一个目标位置，该位置基于目标对象的位置，再加上一个偏移量。
            // 这里的target是一个Transform对象，表示摄像机应该追踪的目标对象。offset则是一个Vector3对象，表示摄像机相对于目标的偏移量。
            // 这行代码的结果，desiredPosition，是一个Vector3对象，表示摄像机应该在每一帧移动到的目标位置。
            Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);

            // 使用Vector3的Lerp方法来平滑地插值计算摄像机当前位置和目标位置之间的一个新位置。
            // 这个新位置将更接近目标位置，移动的速度由smoothSpeed参数决定（值在0-1之间，值越大移动越快）。
            // 这样可以让摄像机平滑地向目标移动，而不是瞬间移动到目标位置。
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // 更新摄像机的位置到新计算的位置。
            // 这样在下一帧时，摄像机就移动到了这个新的位置，给玩家一种摄像机平滑地跟随目标移动的感觉。
            transform.position = smoothedPosition;


            transform.LookAt(target); // 如果你想让相机始终看向主角，可以取消这一行的注释
        }
    }
}

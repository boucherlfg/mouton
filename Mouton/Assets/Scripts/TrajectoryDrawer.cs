using UnityEngine;

public class TrajectoryDrawer : MonoBehaviour {
    public static bool Activated = false;
    private InputService _input; 
    
    [SerializeField] private LineRenderer trajectoryRenderer;
    [SerializeField] private int trajectorySteps = 30;
    private HandScript trajectory;
    void Start() {
        trajectory = GetComponent<HandScript>();
        _input = ServiceManager.Instance.Get<InputService>();
    }
    void Update() {
        if(!trajectory.carried) {
            trajectoryRenderer.positionCount = 0;
            return;
        }

        var mouse = _input.WorldMouse;
        var delta = mouse - (Vector2)transform.position;

        var angle = Mathf.Atan2(delta.y, delta.x);
        var v0 = Mathf.Min(trajectory.maxVelocity, Ext.GetTrajectoryInitialVelocity(trajectory.maxHeight, angle));
        var maxTime = Ext.GetTotalTrajectoryTime(v0, angle, transform.position.y);
        var increments = maxTime / trajectorySteps;
        
        trajectoryRenderer.positionCount = 0;
        
        for(int i = 0; i < trajectorySteps; i++) {
            trajectoryRenderer.positionCount++;
            float t = increments * i;
            var nextPos = Ext.GetTrajectoryPosition(angle, v0, t);
            trajectoryRenderer.SetPosition(i, nextPos);
            if(nextPos.y < -transform.position.y) break;
        }
    }
}
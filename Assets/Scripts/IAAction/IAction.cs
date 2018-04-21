
public interface IAction {

    void  Initialize(ADrunkAI ai);

    float GetPourcentage(float humor, float alcool);

    void DoAction();
}

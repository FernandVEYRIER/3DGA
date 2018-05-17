namespace Assets.Scripts.Effects
{
    /// <summary>
    /// Makes a character stunned.
    /// </summary>
    public class EffectStun : AEffect
    {
        public override void Activate(AEvent ev)
        {
            ev.AddAction(ActionEnum.Action.Stun);
        }
    }
}
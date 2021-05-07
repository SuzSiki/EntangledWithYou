using DG.Tweening;

public interface ISwitchMotion
{
    Sequence Switch(bool onOff,bool active = true);
}
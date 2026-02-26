public class LaserInformationModel : Model
{
    public ReactiveProperty<int> CurrentCharges { get; } = new ReactiveProperty<int>();

    public ReactiveProperty<float> CurrentCD { get; private set; } = new ReactiveProperty<float>();

    public void ChangeCurrentCharges(int charge)
    {
        CurrentCharges.Value = charge;
    }

    public void ChangeCurrentCD(float cd)
    {
        CurrentCD.Value = cd;
    }
}

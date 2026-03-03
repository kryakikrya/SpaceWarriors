public class PlayerObjectPhysics : LivingObjectPhysics
{
    private PlayerMovementModel _model = new PlayerMovementModel();

    public PlayerMovementModel Model => _model;

    public override void Perform()
    {
        base.Perform();

        _model.ChangeCoordinates(_rb.transform.position);

        _model.ChangeRotationAngle(_rb.transform.rotation.eulerAngles.z);

        _model.ChangeVelocity(CurrentVelocity);
    }
}

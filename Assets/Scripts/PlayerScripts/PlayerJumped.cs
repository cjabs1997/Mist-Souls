/// <summary>
/// Fired when the player performs a Jump.
/// </summary>
/// <typeparam name="PlayerJumped"></typeparam>
public class PlayerJumped : Simulation.Event<PlayerJumped>
{
    public PlayerPhysThingRENAME player;

    public override void Execute()
    {
        if (player.m_AudioSource && player.jumpAudio)
            player.m_AudioSource.PlayOneShot(player.jumpAudio);
    }
}
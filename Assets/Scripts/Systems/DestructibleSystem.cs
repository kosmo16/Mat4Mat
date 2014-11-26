using Components;
namespace Systems
{
    public class DestructibleSystem : Framework.Core.System
    {
        public override void OnUpdate()
        {
            foreach (Destructible destructible in GetListOf<Destructible>())
            {
                //destructible.col
            }
        }
    }
}

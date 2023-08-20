namespace XState
{
    public static class ActorExtensions
    {
        public static bool IsActor(object item)
        {
            try
            {
                return item is ActorRef<object, object> actorRef && actorRef.Send != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsSpawnedActor(object item)
        {
            return IsActor(item) && item is ActorRef<object, object> actorRef && actorRef.Id != null;
        }
    }
}

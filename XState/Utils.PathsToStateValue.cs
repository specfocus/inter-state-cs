namespace XState
{
    internal static partial class Utils
    {
        public static StateValue PathsToStateValue(List<List<string>> paths)
        {
            var result = new StateValueMap();

            if (paths == null)
            {
                return result;
            }

            if (paths.Count == 1 && paths[0].Count == 1)
            {
                return new StateValueMap { { paths[0][0], paths[0][0] } };
            }

            foreach (var currentPath in paths)
            {
                var marker = result;
                for (int i = 0; i < currentPath.Count; i++)
                {
                    var subPath = currentPath[i];

                    if (i == currentPath.Count - 2)
                    {
                        marker[subPath] = currentPath[i + 1];
                        break;
                    }
                    if (!marker.ContainsKey(subPath))
                    {
                        marker[subPath] = new StateValueMap();
                    }
                    marker = marker[subPath];
                }
            }

            return result;
        }
    }
}

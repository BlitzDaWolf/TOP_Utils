using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace TOP_Utils
{
    [ExcludeFromCodeCoverage]
    public static class ActivityExtention
    {
        private static string BaseName = string.Empty;
        private static Dictionary<string, ActivitySource> activitySources = new Dictionary<string, ActivitySource>();

        public static void SetBaseName(string name)
        {
            BaseName = name;
        }

        public static Activity? StartActivity(this object o, string activityName, ActivityKind kind = ActivityKind.Internal, string? parentID = null)
        {
            var fillname = BaseName + (string.IsNullOrEmpty(BaseName) ? "" : ".") + o.GetType().FullName;
            if (!activitySources.ContainsKey(fillname))
                activitySources.Add(fillname, new ActivitySource(fillname));
            return activitySources[fillname].StartActivity(activityName, kind, parentID);
        }

        public static Activity? StartActivity<T>(string activityName, ActivityKind kind = ActivityKind.Internal, string? parentID = null)
        {
            var fillname = BaseName + (string.IsNullOrEmpty(BaseName) ? "" : ".") + typeof(T).FullName;
            if (!activitySources.ContainsKey(fillname))
                activitySources.Add(fillname, new ActivitySource(fillname));
            return activitySources[fillname].StartActivity(activityName, kind, parentID);
        }
    }
}

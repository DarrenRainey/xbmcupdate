using NLog;

namespace XbmcUpdate.UpdateEngine.Source
{
    class SourceManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();


        internal static Manifest SourceManifest { get; private set; }


        static SourceManager()
        {
            string manFile = Settings.SourceManifest;
            Logger.Info("Loading manifest file from '{0}'", manFile);
            SourceManifest = Tools.Serilizer.DeserializeObject<Manifest>(Tools.Serilizer.ReadFile(manFile));

            for (int i = 0; i < SourceManifest.Sources.Count; i++)
            {
                SourceManifest.Sources[i].Index = i;
            }
        }

        internal static SourceInfo GetSource(int index)
        {
            foreach (var source in SourceManifest.Sources)
            {
                if (source.Index == index)
                    return source;
            }

            return null;
        }

        internal static SourceInfo GetSource()
        {
            SourceInfo activeSource = null;

            foreach (var source in SourceManifest.Sources)
            {
                if (source.SourceName.ToLower() == Settings.SourceName.ToLower())
                    return source;

                if (source.Default) activeSource = source;
            }

            return activeSource;
        }
    }
}



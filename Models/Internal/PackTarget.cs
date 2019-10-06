namespace Hyperpack.Models.Internal
{
    public enum PackTarget
    {
        /// <summary>
        /// Builds a client-facing pack for distribution with the SK Launcher.
        /// </summary>
        SkLauncher,

        /// <summary>
        /// Builds a server-facing pack for deployment to the Minecraft server.
        /// </summary>
        Server,

        /// <summary>
        /// Packs all assets ready for upload to the web server.
        /// </summary>
        WebServer,

        All
    }
}
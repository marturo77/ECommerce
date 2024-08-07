public static class SignalRRegister
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="app"></param>
    public static void UseSignalR(this WebApplication app)
    {
        app.UseEndpoints(endpoints =>
        {
            _ = endpoints.MapHub<NotificationHub>("/api/notificationHub");
        });
    }
}
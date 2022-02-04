namespace Support.API
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Support.Api;

    /// <summary>
    /// initial starting.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// startinig method.
        /// </summary>
        /// <param name="args">args.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Basic Host builder.
        /// </summary>
        /// <param name="args">args.</param>
        /// <returns>host.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

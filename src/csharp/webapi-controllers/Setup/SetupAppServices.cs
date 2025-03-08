public static class SetupAppServicesExtension {
  public static IServiceCollection SetupAppServices(
    this IServiceCollection services
  ) {
    services.AddScoped<AppService>();
    return services;
  }
}

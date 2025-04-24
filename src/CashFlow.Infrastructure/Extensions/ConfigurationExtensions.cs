﻿using Microsoft.Extensions.Configuration;

namespace CashFlow.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static bool IsTestEnviroment(this IConfiguration configuration)
    {
        return configuration.GetValue<bool>("InMemoryTest");
    }
}

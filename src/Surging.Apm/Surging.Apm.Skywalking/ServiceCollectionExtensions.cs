using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SkyApm.Agent.Hosting;
using Surging.Apm.Skywalking.Abstractions;
using Surging.Apm.Skywalking.Abstractions.Common.Tracing;
using Surging.Apm.Skywalking.Abstractions.Common.Transport;
using Surging.Apm.Skywalking.Abstractions.Config;
using Surging.Apm.Skywalking.Abstractions.Diagnostics;
using Surging.Apm.Skywalking.Abstractions.Diagnostics.EntityFrameworkCore;
using Surging.Apm.Skywalking.Abstractions.Logging;
using Surging.Apm.Skywalking.Abstractions.Tracing;
using Surging.Apm.Skywalking.Abstractions.Transport;
using Surging.Apm.Skywalking.Configuration;
using Surging.Apm.Skywalking.Core;
using Surging.Apm.Skywalking.Core.Common;
using Surging.Apm.Skywalking.Core.Diagnostics;
using Surging.Apm.Skywalking.Core.Diagnostics.EntityFrameworkCore;
using Surging.Apm.Skywalking.Core.Diagnostics.Grpc.Client;
using Surging.Apm.Skywalking.Core.Diagnostics.Grpc.Server;
using Surging.Apm.Skywalking.Core.Diagnostics.GrpcClient;
using Surging.Apm.Skywalking.Core.Diagnostics.HttpClient;
using Surging.Apm.Skywalking.Core.Diagnostics.SqlClient;
using Surging.Apm.Skywalking.Core.Logging;
using Surging.Apm.Skywalking.Core.Sampling;
using Surging.Apm.Skywalking.Core.Service;
using Surging.Apm.Skywalking.Core.Tracing;
using Surging.Apm.Skywalking.Transport.Grpc;
using Surging.Apm.Skywalking.Transport.Grpc.V8;
using Surging.Core.CPlatform;
using Surging.Core.CPlatform.Diagnostics;
using System;
using SegmentReporter = Surging.Apm.Skywalking.Transport.Grpc.SegmentReporter;
using ServiceRegister = Surging.Apm.Skywalking.Transport.Grpc.ServiceRegister;
using PingCaller = Surging.Apm.Skywalking.Transport.Grpc.PingCaller;
using CLRStatsReporter = Surging.Apm.Skywalking.Transport.Grpc.CLRStatsReporter;

namespace Surging.Apm.Skywalking
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceBuilder AddSkyAPM(this IServiceBuilder builder)
        {
            builder.AddSkyAPMCore();
            return builder;
        }

        internal static IServiceBuilder AddSkyAPMCore(this IServiceBuilder builder)
        {
            var services = builder.Services;
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.RegisterType<AsyncQueueSegmentDispatcher>().As<ISegmentDispatcher>().SingleInstance();
            services.RegisterType<RegisterService>().As<IExecutionService>().SingleInstance();
            services.RegisterType<PingService>().As<IExecutionService>().SingleInstance();
            services.RegisterType<SegmentReportService>().As<IExecutionService>().SingleInstance();
            services.RegisterType<CLRStatsService>().As<IExecutionService>().SingleInstance();
            services.RegisterType<InstrumentStartup>().As<IInstrumentStartup>().SingleInstance();
            services.RegisterInstance<IRuntimeEnvironment>(RuntimeEnvironment.Instance).SingleInstance();
            services.RegisterType<TracingDiagnosticProcessorObserver>().SingleInstance();
            services.RegisterType<ConfigAccessor>().As<IConfigAccessor>().SingleInstance();
            services.RegisterType<ConfigurationFactory>().As<IConfigurationFactory>().SingleInstance();
            services.RegisterType<InstrumentationHostedService>().As<IHostedService>().SingleInstance();
            services.RegisterType<HostingEnvironmentProvider>().As<IEnvironmentProvider>().SingleInstance();
            builder.AddTracing()
                .AddSampling()
                .AddGrpcTransport()
                .AddSkyApmLogging()
                .AddHttpClient()
                .AddGrpcClient()
                .AddSqlClient()
                .AddGrpc()
                .AddEntityFrameworkCore(c => c.AddPomeloMysql().AddNpgsql().AddSqlite());

            return builder;
        }

        private static IServiceBuilder AddTracing(this IServiceBuilder builder)
        {
            var services = builder.Services;
            services.RegisterType<TracingContext>().As<ITracingContext>().SingleInstance();
            services.RegisterType<CarrierPropagator>().As<ICarrierPropagator>().SingleInstance();
            services.RegisterType<Sw8CarrierFormatter>().As<ICarrierFormatter>().SingleInstance();
            services.RegisterType<SegmentContextFactory>().As<ISegmentContextFactory>().SingleInstance();
            services.RegisterType<EntrySegmentContextAccessor>().As<IEntrySegmentContextAccessor>().SingleInstance();
            services.RegisterType<LocalSegmentContextAccessor>().As<ILocalSegmentContextAccessor>().SingleInstance();
            services.RegisterType<ExitSegmentContextAccessor>().As<IExitSegmentContextAccessor>().SingleInstance();
            services.RegisterType<SamplerChainBuilder>().As<ISamplerChainBuilder>().SingleInstance();
            services.RegisterType<UniqueIdGenerator>().As<IUniqueIdGenerator>().SingleInstance();
            services.RegisterType<SegmentContextMapper>().As<ISegmentContextMapper>().SingleInstance();
            services.RegisterType<Base64Formatter>().As<IBase64Formatter>().SingleInstance();
            return builder;
        }

        private static IServiceBuilder AddSampling(this IServiceBuilder builder)
        {
            var services = builder.Services;

            services.RegisterType<SimpleCountSamplingInterceptor>().SingleInstance();
            services.RegisterType<SimpleCountSamplingInterceptor>().As<ISamplingInterceptor>().SingleInstance();
            services.RegisterType<SimpleCountSamplingInterceptor>().As<IExecutionService>().SingleInstance();
            services.RegisterType<RandomSamplingInterceptor>().As<ISamplingInterceptor>().SingleInstance();
            return builder;
        }

        private static IServiceBuilder AddGrpcTransport(this IServiceBuilder builder)
        {
            var services = builder.Services;
            services.RegisterType<SegmentReporter>().As<ISegmentReporter>().SingleInstance();
            services.RegisterType<CLRStatsReporter>().As<ICLRStatsReporter>().SingleInstance();
            services.RegisterType<ConnectionManager>().SingleInstance();
            services.RegisterType<PingCaller>().As<IPingCaller>().SingleInstance();
            services.RegisterType<ServiceRegister>().As<IServiceRegister>().SingleInstance();
            services.RegisterType<ConnectService>().As<IExecutionService>().SingleInstance();
            return builder;
        }

        private static IServiceBuilder AddSkyApmLogging(this IServiceBuilder builder)
        {
            var services = builder.Services;
            services.RegisterType<DefaultLoggerFactory>().As<ILoggerFactory>().SingleInstance();
            return builder;
        }

        public static IServiceBuilder AddHttpClient(this IServiceBuilder builder)
        {
            var services = builder.Services;
            services.RegisterType<HttpClientTracingDiagnosticProcessor>().As<ITracingDiagnosticProcessor>().SingleInstance();
            services.RegisterType<DefaultRequestDiagnosticHandler>().As<IRequestDiagnosticHandler>().SingleInstance();
            services.RegisterType<GrpcRequestDiagnosticHandler>().As<IRequestDiagnosticHandler>().SingleInstance();
            return builder;
        }

        public static IServiceBuilder AddGrpcClient(this IServiceBuilder builder)
        {
            var services = builder.Services;
            services.RegisterType<GrpcClientDiagnosticProcessor>().As<ITracingDiagnosticProcessor>().SingleInstance();

            return builder;
        }

        public static IServiceBuilder AddSqlClient(this IServiceBuilder builder)
        {
            var services = builder.Services;
            services.RegisterType<SqlClientTracingDiagnosticProcessor>().As<ITracingDiagnosticProcessor>().SingleInstance();

            return builder;
        }

        public static IServiceBuilder AddGrpc(this IServiceBuilder builder)
        {
            var services = builder.Services;
            services.RegisterType<ClientDiagnosticProcessor>().SingleInstance();
            services.RegisterType<ClientDiagnosticInterceptor>().SingleInstance();
            services.RegisterType<ServerDiagnosticProcessor>().SingleInstance();
            services.RegisterType<ServerDiagnosticInterceptor>().SingleInstance();
            return builder;
        }

        public static IServiceBuilder AddEntityFrameworkCore(this IServiceBuilder builder, Action<DatabaseProviderBuilder> optionAction)
        {
            var services = builder.Services;
            services.RegisterType<EntityFrameworkCoreTracingDiagnosticProcessor>().As<ITracingDiagnosticProcessor>().SingleInstance();
            services.RegisterType<EntityFrameworkCoreSegmentContextFactory>().As<IEntityFrameworkCoreSegmentContextFactory>().SingleInstance();

            if (optionAction != null)
            {
                var databaseProviderBuilder = new DatabaseProviderBuilder(builder);
                optionAction(databaseProviderBuilder);
            }

            return builder;
        }
        
        public static DatabaseProviderBuilder AddPomeloMysql(this DatabaseProviderBuilder builder)
        {
            var services = builder.Builder.Services;
            services.RegisterType<MySqlEntityFrameworkCoreSpanMetadataProvider>().As<IEntityFrameworkCoreSpanMetadataProvider>().SingleInstance();
            return builder;
        }

        public static DatabaseProviderBuilder AddNpgsql(this DatabaseProviderBuilder builder)
        {
            var services = builder.Builder.Services;
            services.RegisterType<NpgsqlEntityFrameworkCoreSpanMetadataProvider>().As<IEntityFrameworkCoreSpanMetadataProvider>().SingleInstance();
            return builder;
        }

        public static DatabaseProviderBuilder AddSqlite(this DatabaseProviderBuilder builder)
        {
            var services = builder.Builder.Services;
            services.RegisterType<SqliteEntityFrameworkCoreSpanMetadataProvider>().As<IEntityFrameworkCoreSpanMetadataProvider>().SingleInstance();
            return builder;
        }
    }
}
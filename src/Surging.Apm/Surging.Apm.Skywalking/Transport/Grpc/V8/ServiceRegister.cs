/*
 * Licensed to the SkyAPM under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The SkyAPM licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

using System;
using System.Threading;
using System.Threading.Tasks;
using SkyWalking.NetworkProtocol.V3;
using Microsoft.Extensions.Logging;
using System.Linq;
using Surging.Apm.Skywalking.Abstractions.Config;
using Surging.Apm.Skywalking.Abstractions.Transport;
using Surging.Apm.Skywalking.Transport.Grpc.Common;
using Surging.Apm.Skywalking.Abstractions.Common;

namespace Surging.Apm.Skywalking.Transport.Grpc.V8
{
    internal class ServiceRegister : IServiceRegister
    {
        private const string OS_NAME = "os_name";
        private const string HOST_NAME = "host_name";
        private const string IPV4 = "ipv4";
        private const string PROCESS_NO = "process_no";
        private const string LANGUAGE = "language";

        private readonly ConnectionManager _connectionManager;
        private readonly ILogger _logger;
        private readonly GrpcConfig _config;

        public ServiceRegister(ConnectionManager connectionManager, IConfigAccessor configAccessor,
            ILoggerFactory loggerFactory)
        {
            _connectionManager = connectionManager;
            _config = configAccessor.Get<GrpcConfig>();
            _logger = loggerFactory.CreateLogger(typeof(ServiceRegister));
        }

        public Task<NullableValue> RegisterServiceAsync(ServiceRequest serviceRequest, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<NullableValue> RegisterServiceInstanceAsync(ServiceInstanceRequest serviceInstanceRequest, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ReportInstancePropertiesAsync(ServiceInstancePropertiesRequest serviceInstancePropertiesRequest, CancellationToken cancellationToken = default)
        {
            if (!_connectionManager.Ready)
            {
                return false;
            }

            var connection = _connectionManager.GetConnection();
            return await new Call(_logger, _connectionManager).Execute(async () =>
            {
                var client = new ManagementService.ManagementServiceClient(connection);
                var instance = new InstanceProperties
                {
                    Service = serviceInstancePropertiesRequest.ServiceId,
                    ServiceInstance = serviceInstancePropertiesRequest.ServiceInstanceId,
                };

                instance.Properties.Add(new KeyStringValuePair
                { Key = OS_NAME, Value = serviceInstancePropertiesRequest.Properties.OsName });
                instance.Properties.Add(new KeyStringValuePair
                { Key = HOST_NAME, Value = serviceInstancePropertiesRequest.Properties.HostName });
                instance.Properties.Add(new KeyStringValuePair
                { Key = PROCESS_NO, Value = serviceInstancePropertiesRequest.Properties.ProcessNo.ToString() });
                instance.Properties.Add(new KeyStringValuePair
                { Key = LANGUAGE, Value = serviceInstancePropertiesRequest.Properties.Language });
                foreach (var ip in serviceInstancePropertiesRequest.Properties.IpAddress)
                    instance.Properties.Add(new KeyStringValuePair
                    { Key = IPV4, Value = ip });

                var mapping = await client.reportInstancePropertiesAsync(instance,
                    _config.GetMeta(), _config.GetTimeout(), cancellationToken);

                //todo: should assert the result?
                return true;
            },
                () => false,
                () => ExceptionHelpers.ReportServiceInstancePropertiesError);
        }
    }
}
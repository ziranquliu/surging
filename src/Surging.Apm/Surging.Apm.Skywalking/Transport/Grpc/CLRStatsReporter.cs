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
using Surging.Apm.Skywalking.Abstractions;
using Surging.Apm.Skywalking.Abstractions.Config;
using Surging.Apm.Skywalking.Abstractions.Transport;
using Microsoft.Extensions.Logging;

namespace Surging.Apm.Skywalking.Transport.Grpc
{
    public class CLRStatsReporter : ICLRStatsReporter
    {
        private readonly TransportConfig _transportConfig;
        private readonly ICLRStatsReporter _clrStatsReporterV8;

        public CLRStatsReporter(ConnectionManager connectionManager, ILoggerFactory loggerFactory,
            IConfigAccessor configAccessor, IRuntimeEnvironment runtimeEnvironment)
        {
            _transportConfig = configAccessor.Get<TransportConfig>();
            _clrStatsReporterV8 = new V8.CLRStatsReporter(connectionManager, loggerFactory, configAccessor, runtimeEnvironment);
        }

        public async Task ReportAsync(CLRStatsRequest statsRequest,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (_transportConfig.ProtocolVersion == ProtocolVersions.V8)
                await _clrStatsReporterV8.ReportAsync(statsRequest);
        }
    }
}
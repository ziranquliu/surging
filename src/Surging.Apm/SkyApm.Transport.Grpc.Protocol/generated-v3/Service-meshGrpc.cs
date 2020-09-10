// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: service-mesh-probe/service-mesh.proto
// </auto-generated>
// Original file comments:
//
// Licensed to the Apache Software Foundation (ASF) under one or more
// contributor license agreements.  See the NOTICE file distributed with
// this work for additional information regarding copyright ownership.
// The ASF licenses this file to You under the Apache License, Version 2.0
// (the "License"); you may not use this file except in compliance with
// the License.  You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
//
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace SkyWalking.NetworkProtocol.V3 {
  public static partial class ServiceMeshMetricService
  {
    static readonly string __ServiceName = "ServiceMeshMetricService";

    static readonly grpc::Marshaller<global::SkyWalking.NetworkProtocol.V3.ServiceMeshMetric> __Marshaller_ServiceMeshMetric = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::SkyWalking.NetworkProtocol.V3.ServiceMeshMetric.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::SkyWalking.NetworkProtocol.V3.MeshProbeDownstream> __Marshaller_MeshProbeDownstream = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::SkyWalking.NetworkProtocol.V3.MeshProbeDownstream.Parser.ParseFrom);

    static readonly grpc::Method<global::SkyWalking.NetworkProtocol.V3.ServiceMeshMetric, global::SkyWalking.NetworkProtocol.V3.MeshProbeDownstream> __Method_collect = new grpc::Method<global::SkyWalking.NetworkProtocol.V3.ServiceMeshMetric, global::SkyWalking.NetworkProtocol.V3.MeshProbeDownstream>(
        grpc::MethodType.ClientStreaming,
        __ServiceName,
        "collect",
        __Marshaller_ServiceMeshMetric,
        __Marshaller_MeshProbeDownstream);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::SkyWalking.NetworkProtocol.V3.ServiceMeshReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of ServiceMeshMetricService</summary>
    [grpc::BindServiceMethod(typeof(ServiceMeshMetricService), "BindService")]
    public abstract partial class ServiceMeshMetricServiceBase
    {
      public virtual global::System.Threading.Tasks.Task<global::SkyWalking.NetworkProtocol.V3.MeshProbeDownstream> collect(grpc::IAsyncStreamReader<global::SkyWalking.NetworkProtocol.V3.ServiceMeshMetric> requestStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for ServiceMeshMetricService</summary>
    public partial class ServiceMeshMetricServiceClient : grpc::ClientBase<ServiceMeshMetricServiceClient>
    {
      /// <summary>Creates a new client for ServiceMeshMetricService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public ServiceMeshMetricServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for ServiceMeshMetricService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public ServiceMeshMetricServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected ServiceMeshMetricServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected ServiceMeshMetricServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual grpc::AsyncClientStreamingCall<global::SkyWalking.NetworkProtocol.V3.ServiceMeshMetric, global::SkyWalking.NetworkProtocol.V3.MeshProbeDownstream> collect(grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return collect(new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncClientStreamingCall<global::SkyWalking.NetworkProtocol.V3.ServiceMeshMetric, global::SkyWalking.NetworkProtocol.V3.MeshProbeDownstream> collect(grpc::CallOptions options)
      {
        return CallInvoker.AsyncClientStreamingCall(__Method_collect, null, options);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override ServiceMeshMetricServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new ServiceMeshMetricServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(ServiceMeshMetricServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_collect, serviceImpl.collect).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, ServiceMeshMetricServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_collect, serviceImpl == null ? null : new grpc::ClientStreamingServerMethod<global::SkyWalking.NetworkProtocol.V3.ServiceMeshMetric, global::SkyWalking.NetworkProtocol.V3.MeshProbeDownstream>(serviceImpl.collect));
    }

  }
}
#endregion

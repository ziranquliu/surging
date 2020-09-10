// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: language-agent/Tracing.proto
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
  public static partial class TraceSegmentReportService
  {
    static readonly string __ServiceName = "TraceSegmentReportService";

    static readonly grpc::Marshaller<global::SkyWalking.NetworkProtocol.V3.SegmentObject> __Marshaller_SegmentObject = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::SkyWalking.NetworkProtocol.V3.SegmentObject.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::SkyWalking.NetworkProtocol.V3.Commands> __Marshaller_Commands = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::SkyWalking.NetworkProtocol.V3.Commands.Parser.ParseFrom);

    static readonly grpc::Method<global::SkyWalking.NetworkProtocol.V3.SegmentObject, global::SkyWalking.NetworkProtocol.V3.Commands> __Method_collect = new grpc::Method<global::SkyWalking.NetworkProtocol.V3.SegmentObject, global::SkyWalking.NetworkProtocol.V3.Commands>(
        grpc::MethodType.ClientStreaming,
        __ServiceName,
        "collect",
        __Marshaller_SegmentObject,
        __Marshaller_Commands);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::SkyWalking.NetworkProtocol.V3.TracingReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of TraceSegmentReportService</summary>
    [grpc::BindServiceMethod(typeof(TraceSegmentReportService), "BindService")]
    public abstract partial class TraceSegmentReportServiceBase
    {
      public virtual global::System.Threading.Tasks.Task<global::SkyWalking.NetworkProtocol.V3.Commands> collect(grpc::IAsyncStreamReader<global::SkyWalking.NetworkProtocol.V3.SegmentObject> requestStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for TraceSegmentReportService</summary>
    public partial class TraceSegmentReportServiceClient : grpc::ClientBase<TraceSegmentReportServiceClient>
    {
      /// <summary>Creates a new client for TraceSegmentReportService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public TraceSegmentReportServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for TraceSegmentReportService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public TraceSegmentReportServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected TraceSegmentReportServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected TraceSegmentReportServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual grpc::AsyncClientStreamingCall<global::SkyWalking.NetworkProtocol.V3.SegmentObject, global::SkyWalking.NetworkProtocol.V3.Commands> collect(grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return collect(new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncClientStreamingCall<global::SkyWalking.NetworkProtocol.V3.SegmentObject, global::SkyWalking.NetworkProtocol.V3.Commands> collect(grpc::CallOptions options)
      {
        return CallInvoker.AsyncClientStreamingCall(__Method_collect, null, options);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override TraceSegmentReportServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new TraceSegmentReportServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(TraceSegmentReportServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_collect, serviceImpl.collect).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, TraceSegmentReportServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_collect, serviceImpl == null ? null : new grpc::ClientStreamingServerMethod<global::SkyWalking.NetworkProtocol.V3.SegmentObject, global::SkyWalking.NetworkProtocol.V3.Commands>(serviceImpl.collect));
    }

  }
}
#endregion

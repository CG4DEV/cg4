using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using CG4.Impl.Rabbit.Extensions;
using Xunit;

namespace CG4.Impl.Rabbit.Tests.Extensions;

public class QueueMessageExtensionsTests
{
    [Fact]
    public void ConvertToBody_CorrectQueueMessage_ReturnBytes()
    {
        var testMessage = new QueueMessageRabbitMQ()
        {
            Uid = Guid.NewGuid(),
            Delay = 100,
            Body = "{\"Date\":\"2022-04-08T14:08:20.2810335+00:00\",\"TemperatureCelsius\":33,\"Summary\":\"will be warm\"}",
            Errors = new List<string>(){"r"},
        };
        var result = testMessage.ConvertToBody();
        var resultObject = JsonSerializer.Deserialize<QueueMessageRabbitMQ>(result);
        Assert.Equal(testMessage.Uid, resultObject?.Uid);
        Assert.Equal(testMessage.Body, resultObject?.Body);
    }
    
    [Fact]
    public void ConvertFromBody_CorrectBytes_ReturnQueueMessage()
    {
        var testMessage = new QueueMessageRabbitMQ()
        {
            Uid = Guid.NewGuid(),
            Delay = 100,
            Body = "{\"Date\":\"2022-04-08T14:08:20.2810335+00:00\",\"TemperatureCelsius\":33,\"Summary\":\"will be warm\"}",
            Errors = new List<string>(){"r"},
        };
        var jsonSerializerSettings = new JsonSerializerOptions()
        {
            MaxDepth = 5,
            UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
        };
        var preparedBytes = JsonSerializer.SerializeToUtf8Bytes(testMessage, jsonSerializerSettings);
        var result = preparedBytes.ConvertFromBody();
        Assert.Equal(testMessage.Uid, result.Uid);
        Assert.Equal(testMessage.Body, result.Body);
    }
}
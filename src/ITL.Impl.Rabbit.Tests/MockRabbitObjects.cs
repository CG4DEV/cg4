using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using RabbitMQ.Client;

namespace ITL.Impl.Rabbit.Tests;

public static class MockRabbitObjects
{
    public static IConnectionFactory GetFactory(Mock<IModel> model, string testQueue)
    {
        var connectionFactory = new Mock<IConnectionFactory>();

        var connection = new Mock<IConnection>();

        var basicProperties = new Mock<IBasicProperties>();

        model.Setup(x => x.BasicGet(testQueue, It.IsAny<bool>()))
            .Returns(new BasicGetResult(110, false, "", "", 9, basicProperties.Object, Encoding.ASCII.GetBytes("Hi")));
        model.Setup(x => x.QueueDeclare(testQueue, It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(), null))
            .Returns(new QueueDeclareOk(testQueue, 122, 3));
        model.Setup(x => x.CreateBasicProperties())
            .Returns(new Mock<IBasicProperties>().Object);


        connection.Setup(x => x.CreateModel())
            .Returns(model.Object);
        connectionFactory.Setup(x => x.CreateConnection())
            .Returns(connection.Object);

        return connectionFactory.Object;
    }

    public static IQueueMessage GetCorrectMessage()
    {
        var testMessage = new QueueMessageRabbitMQ()
        {
            Uid = Guid.NewGuid(),
            Delay = 0,
            Body = "{\"Date\":\"2022-04-08T14:08:20.2810335+00:00\",\"TemperatureCelsius\":33,\"Summary\":\"will be warm\"}",
            Errors = new List<string>(),
        };
        return testMessage;
    }
}
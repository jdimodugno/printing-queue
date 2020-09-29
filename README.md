# tfi-printing-queue
This is the first exam for the subject "Trabajo Final de Ingeniería"

I'm using [RabbitMQ](https://www.rabbitmq.com/getstarted.html) and [MongoDB Client for C#](https://www.mongodb.com/blog/post/quick-start-c-sharp-and-mongodb-starting-and-setup)

For this project, I've taken the the docker approach, running the MQ and Mongo inside containers.

Ensure that you have running a RabbitMQ instance and the MongoDB service before you start.

Steps

Run the API: Inside the API project, type and execute: dotnet run;

Run the TestClient: Inside the TestClient project, type and execute: dotnet run;
*It will send test payloads to our API*

Run the ReceiverApplication: Inside the ReceiverApplication project, type and execute: dotnet run;
*It will receive enqueued printRequests from our API*

Run the JobsLogger: Inside the JobsLogger project, type and execute: dotnet run;
*It will receive all the printing job statuses enqueued by our received app*

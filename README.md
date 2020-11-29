# RabbitMQ Practices with .Net Core

Hi everyone. I worked with message broker RabbitMQ in this repository. I made examples step by step from simple to difficult. 


## How can you use this examples?


### 1. Setup RabbitMQ Local PC or Cloud

### a. Local setup

You can use RabbitMQ with setup on your local PC or cloud. For setup local PC, click the [link](https://www.rabbitmq.com/download.html), Afterwards download exe for your OS  and setup.

![enter image description here](https://i.ibb.co/fnsbwsh/Rabbit-MQDownload.png)
.


You must run 'rabbitmq-plugins enable rabbitmq_management' cmd command in 'C:\Program Files\RabbitMQ Server\rabbitmq_server-3.8.9\sbin' url, for using management page via browser.


![enter image description here](https://i.ibb.co/HPF5Spc/cmd.png)

.
Open your browser, go to http://localhost:15672/ url, login with default 'guest' username and password.


![enter image description here](https://i.ibb.co/JsGWzSZ/loginpage.png)

.
You can see RabbitMQ server management page now.


![enter image description here](https://i.ibb.co/hLWtbxW/localmanagement-Page.png)

.

#### b. Cloud setup

You can use RabbitMQ on cloud with free account. For this go to 'https://www.cloudamqp.com/' adress and sign up or login with your google or github account. Then click create new instance and setup instance in four step.


![enter image description here](https://i.ibb.co/ZMWfGhj/cloud-Create.png)


.
You can use your cloud instance with AMQP URL.

![enter image description here](https://i.ibb.co/v482SYH/clouddetail.png)



### 2. Download Repository
Download repository and change url according to your setup.
In the Solution Explorer, right click the consumer project and click Open in Terminal. Do this three times and write opened windows respectively "dotnet run 300", "dotnet run 700" and "dotnet run 1500". You ran the three consumer application what have different configuration times. They are listening to queue for new messages. Go to the solution Explorer and click the publisher project and click Open in Terminal. Write "dotnet run work 30" this time. You can see published messages and consumed messages in power shell screens.

![enter image description here](https://miro.medium.com/max/1250/1*4Kt0rkJz6pU6eDLtRt6bsA.gif)

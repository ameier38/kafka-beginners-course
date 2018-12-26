# Kafka Beginners Course
Source code from working through the [Udemy Kafka course](https://www.udemy.com/apache-kafka/)
using F# instead of Java. Instructions for setting up a Kafka environment and an explanation of concepts
can be found at [https://andrewcmeier.com/kafka-tutorial](https://andrewcmeier.com/kafka-tutorial).

## Environment set up
1. Install .NET Core SDK. On Linux:
    ```
    $ wget -q https://packages.microsoft.com/config/ubuntu/16.04/packages-microsoft-prod.deb
    $ sudo dpkg -i packages-microsoft-prod.deb
    $ sudo apt-get install apt-transport-https
    $ sudo apt-get update
    $ sudo apt-get install dotnet-sdk-2.2
    ```
    On Windows:
    ```
    > scoop install dotnet-sdk
    ```

2. Install Kafka. See [the accompanying blog post](https://andrewcmeier.com/kafka-tutorial) for
instructions on installing Kafka locally using Helm.

## Usage

1. Clone the repo
    ```
    > git clone https://github.com/ameier38/kafka-beginners-course.git
    > cd kafka-beginners-course
    ```

2. Install dependencies.
    ```
    > .paket/paket.exe install
    ```
    > If you are using OSX or Linux you will need to install [Mono](https://www.mono-project.com/).

3. Restore the project.
    ```
    > dotnet restore
    ```

4. Compile the application.
    ```
    > dotnet publish -o out
    ```
    > This will compile the application and add the compiled assets into
    a directory called `out`.

5. Start the consumer.
    ```
    > dotnet out/Tutorial.dll consumer kafka:9092 test_topic test_group
    ```
    > This will start a consumer that will try to connect to a Kafka broker
    at `kafka:9092` listening on the topic `test_topic` within the group `test_group`.

6. Open a new terminal and start the producer.
    ```
    > dotnet out/Tutorial.dll producer kafka:9092 test_topic test_key
    ```
    > This will start a producer that will try to connect to a Kafka broker
    at `kafka:9092` producing to the topic `test_topic` using the key `test_key`.

If you type a message into the producer terminal then you should see the messages
appear in the consumer terminal. See the 
[accompanying blog post](https://andrewcmeier.com/kafka-tutorial)
for a full walk through.

## Resources
- [Kafka homepage](https://kafka.apache.org/)
- [Udemy Kafka course](https://www.udemy.com/apache-kafka/)
- [F# wrapper for Confluent Kafka](https://github.com/jet/confluent-kafka-fsharp)
- [Confluent .NET client](https://github.com/confluentinc/confluent-kafka-dotnet)

module Consumer

open System.Text
open Confluent.Kafka

let bytesToString (bytes:byte []) =
    Encoding.UTF8.GetString bytes

let runConsumer (broker:string) (topic:string) (groupId:string) =
    use consumer =
        // create initial config Map with safe defaults
        // see https://github.com/edenhill/librdkafka/blob/master/CONFIGURATION.md
        // for a full list of configuration options
        Config.Consumer.safe
        // add bootstrap.servers entry to config Map
        |> Config.bootstrapServers broker
        // add group.id entry to config Map
        |> Config.Consumer.groupId groupId
        // add auto.offset.reset entry to config Map (read from beginning)
        |> Config.Consumer.Topic.autoOffsetReset Config.Consumer.Topic.Beginning
        // add client.id entry to config Map
        |> Config.clientId "test-client"
        // create a Consumer instance from above configuration
        |> Consumer.create

    // define message handler which prints the message to the console
    let handle (messageSet:ConsumerMessageSet) =
        async {
            messageSet.messages
            |> Seq.iter (fun msg -> msg.Value |> bytesToString |> printfn "message: %s")
        }

    // tell the consumer to listen on a particular topic
    consumer.Subscribe topic

    // start the consumer
    Consumer.consume consumer 1000 1000 10 handle
    |> Async.RunSynchronously

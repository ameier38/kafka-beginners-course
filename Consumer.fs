module Consumer

open System.Text
open Confluent.Kafka

let bytesToString (bytes:byte []) =
    Encoding.UTF8.GetString bytes

let runConsumer (broker:string) (topic:string) (groupId:string) =
    let config =
        Config.Consumer.safe
        |> Config.bootstrapServers broker
        |> Config.Consumer.groupId groupId
        |> Config.Consumer.Topic.autoOffsetReset Config.Consumer.Topic.Beginning
        |> Config.clientId "test-client"

    use consumer = Consumer.create config

    let handle (messageSet:ConsumerMessageSet) =
        async {
            messageSet.messages
            |> Seq.iter (fun msg -> msg.Value |> bytesToString |> printfn "message: %s")
        }

    consumer.Subscribe topic

    Consumer.consume consumer 1000 1000 10 handle
    |> Async.RunSynchronously

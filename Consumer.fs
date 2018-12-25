module Consumer

open Confluent.Kafka

let runConsumer (broker:string) (topic:string) (groupId:string) =
    let config =
        Config.Consumer.safe
        |> Config.bootstrapServers broker
        |> Config.Consumer.groupId groupId
        |> Config.clientId "test-client"

    use consumer = Consumer.create config

    let handle (messageSet:ConsumerMessageSet) =
        async {
            printfn "%A" messageSet.messages
        }

    Consumer.consume consumer 1000 1000 1 handle
    |> Async.RunSynchronously

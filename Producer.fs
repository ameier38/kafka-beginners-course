module Producer

open System
open Confluent.Kafka

let runProducer (broker:string) (topic:string) (key:string) =
    use producer =
        // create initial config Map with safe defaults
        // see https://github.com/edenhill/librdkafka/blob/master/CONFIGURATION.md
        // for a full list of configuration options
        Config.Producer.safe
        // adds bootstrap.servers entry to config Map
        |> Config.bootstrapServers broker
        // add client.id entry to config Map
        |> Config.clientId "test-client"
        // create Producer instance from above configuration
        |> Producer.create

    // continuously read from console
    fun _ -> Console.ReadLine()
    |> Seq.initInfinite
    |> Seq.takeWhile ((<>) null)
    |> Seq.iter (fun msg ->
        // send message to Kafka for each line entered in console
        Producer.produceString producer topic (key, msg)
        |> Async.RunSynchronously
        |> printfn "done: %A")

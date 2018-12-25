module Producer

open System
open Confluent.Kafka

let runProducer (broker:string) (topic:string) (key:string) =
    use producer =
        Config.Producer.safe
        |> Config.bootstrapServers broker
        |> Config.clientId "test-client"
        |> Producer.create

    fun _ -> Console.ReadLine()
    |> Seq.initInfinite
    |> Seq.takeWhile ((<>) null)
    |> Seq.iter (fun msg ->
        Producer.produceString producer topic (key, msg)
        |> Async.RunSynchronously
        |> printfn "done: %A")

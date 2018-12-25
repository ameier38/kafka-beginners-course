module RawProducer

open System
open System.Text
open Confluent.Kafka

let inline stringToBytes (s:string) : byte [] =
    Encoding.UTF8.GetBytes s

let createProducer (config: Map<string, obj>) =
    let producer = new Producer (config, true, true)
    producer

let createConsumer (config: Map<string, obj>) =
    let consumer = new Consumer (config)
    consumer

let runProducer (broker:string) (topic:string) (key:string) =
    let config : Map<string, obj> =
        ["bootstrap.servers", broker |> box] 
        |> Map.ofList

    use producer = createProducer config

    let keyBytes = key |> stringToBytes

    fun _ -> Console.ReadLine()
    |> Seq.initInfinite
    |> Seq.takeWhile ((<>) null)
    |> Seq.map (fun msg -> 
        producer.ProduceAsync (topic, keyBytes, msg |> stringToBytes) 
        |> Async.AwaitTask
        |> Async.RunSynchronously)
    |> printfn "done: %A"

let runConsumer (broker:string, topic:string, groupId:string) =
    let config : Map<string, obj> =
        [ "bootstrap.servers", broker |> box 
          "auto.offset.reset", "beginning" |> box ]
        |> Map.ofList
    
    use consumer = createConsumer config

    consumer.Subscribe(topic)
    let msg = consumer.Consume()
    printfn "message: %s" msg


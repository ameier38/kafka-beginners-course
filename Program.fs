let [<Literal>] ProducerArg = "producer"
let [<Literal>] ConsumerArg = "consumer"

[<EntryPoint>]
let main argv =
    printfn "received args: %A" argv
    try
        match argv with
        | [|ProducerArg; broker; topic; key|] ->
            printfn "running raw producer..."
            Producer.runProducer broker topic key
        | [|ConsumerArg; broker; topic; groupId|] ->
            Consumer.runConsumer broker topic groupId
        | _ -> printfn "invalid command %A" argv
    with ex ->
        printfn "Exception: %A" ex
    0 // return an integer exit code

$(document).ready(function () {
    const broadcastMessageToAllClientHubMethodCall = "BroadcastMessageToAllClient";

    const receiverMessageForAllClientMethodCall = "ReceiverMessageForAllClient";
    const receiveConnectedClientCountAllClient = "ReceiveConnectedClientCountAllClient";
    const receiveMessageForCallerClient = "ReceiveMessageForCallerClient";
    const broadcastMessageToCallerClient = "BroadcastMessageToCallerClient";

    const broadcastMessageToOthersClient = "BroadcastMessageToOthersClient";
    const receiveMessageForOthersClient = "ReceiveMessageForOthersClient";

    const connection = new signalR.HubConnectionBuilder().withUrl("/exampleTypeSafeHub").configureLogging(signalR.LogLevel.Information).build();

    function start() {
        connection.start().then(() => console.log("Hub ile bağlantı kuruldu"));
    }

    try {
        start();
    } catch {
        setTimeout(() => start(), 5000);
    }

    connection.on(receiverMessageForAllClientMethodCall, (message) => {
        console.log("Gelen mesaj", message);
    });


    connection.on(receiveMessageForCallerClient, (message) => {
        console.log("(Caller) Gelen mesaj", message);
    });


    connection.on(receiveMessageForOthersClient, (message) => {
        console.log("(Other) Gelen mesaj", message);
    });


    const span_client_count = $("#span-connected-client-count");
    connection.on(receiveConnectedClientCountAllClient, (count) => {
        span_client_count.text(count);
        console.log("Connected Client Count: ", count);
    })
    $('#btn-send-message-all-client').click(function () {
        const message = "hello word";

        connection.invoke(broadcastMessageToAllClientHubMethodCall, message).catch(err => console.error("hata", err));
    });

    $('#btn-send-message-caller-client').click(function () {
        const message = "hello word";

        connection.invoke(broadcastMessageToCallerClient, message).catch(err => console.error("hata", err));
    })


    $('#btn-send-message-other-client').click(function () {
        const message = "hello word";

        connection.invoke(broadcastMessageToOthersClient, message).catch(err => console.error("hata", err));
    })

})
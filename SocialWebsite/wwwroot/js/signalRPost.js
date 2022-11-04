$(() => {

    // create connection
    var connectionProductHub = new signalR.HubConnectionBuilder()
        // .configureLogging(signalR.LogLevel.Information)
        .withUrl('/hubs/post', signalR.HttpTransportType.WebSockets).build();

    // connect to methods that hub invokes aka receive notifications from hub
    connectionProductHub.on('Notification', (author, postTitle, method) => {
        ShowNotification(author, postTitle, method);
    });

    // invoke hub methods aka send notification to hub
    function sendMessageToHub() {
        connectionProductHub.send('functionName');
    }

    // start connection
    function fulfilled() {
        // do something on start
        console.log('Connection to User Hub Successful');
    }

    function rejected() {
        // rejected logs
        console.log('Connection to User Hub Failed');
    }

    // start connection
    connectionProductHub.start().then(fulfilled, rejected);

    function ShowNotification(author, postTitle, method) {
        if (method == "create") {
            // Display a success toast, with a title
            toastr.success(postTitle, `${author} đã đăng 1 bài post`);
        } else if (method == "update") {
            // Display a success toast, with a title
            toastr.success(postTitle, `${author} đã cập nhật 1 bài post`);
        }
     
    }

})
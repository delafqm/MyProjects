"use strict";

//初始化连接
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub", { accessTokenFactory: () => "" }).build();

//禁用发送按钮，直到建立连接
document.getElementById("sendButton").disabled = true;

//接收事件
connection.on("ReceiveMessage1", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.on("over", function (message) {
    var msg = message + "结束";
    var li = document.createElement("li");
    li.textContent = msg;
    document.getElementById("messagesList").appendChild(li);

    document.getElementById("sendButton").disabled = true;
});

//建立连接，并启用发送按钮
connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

//绑定发磅按钮事件
document.getElementById("sendButton").addEventListener("click", function (event) {
    //获取参数
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    //执行方法SendMessage1
    connection.invoke("SendMessage1", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
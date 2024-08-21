var WebSocketLibrary = {
    $instances: {},
    Jslib_InitializeWebSocket: function(onOpen, onMessage, Connecting, SetRoomInfo, JoinClient){
        console.log("Initializing");
        instances = {
            socket: null, onOpen:onOpen, onMessage:onMessage, Connecting:Connecting, 
            SetRoomInfo: SetRoomInfo, JoinClient: JoinClient, MyID: null, MyRoomID: null,
            RoomInfo: null
        };
        console.log(instances);
    },
    Jslib_ConnectWebSocket: function(){
        console.log("Connect Start");
        var instance = instances;
        var urlStr = 'wss://192.168.174.86:8000/';
        instance.socket = new WebSocket(urlStr);
        console.log("socket");
        instance.socket.onopen = function(e){
            dynCall('v', instance.onOpen);
            console.log("ConnectedNow");
        };

        instance.socket.onmessage = function(e) {  
            console.log(e.data);
            if(e.data == "ConnectOK")
            {
                dynCall('v', instance.Connecting);
            }
            else if(e.data == "JoinClient")
            {
                
            }
            else if(e.data.slice(0, 2) == 'id')
            {
                instance.MyID = e.data.slice(2);
                console.log("ID:"+ instance.MyID);
                console.log(instances.MyID);
                console.log(instances);
            }
            else if(e.data.slice(0, 2) == 'sy')
            {
                var RecievedPacket = JSON.parse(e.data.slice(2));
                const StageID = RecievedPacket["StageID"]
                RecievedPacket["GameSyncInfo"].DiscoveredObjName.forEach(function(val){
                    var message = val;
                    var length = lengthBytesUTF8(message) + 1;
                    var buffer = _malloc(length);
                    stringToUTF8(message, buffer, length);
                    try{
                        dynCall('viii', instance.onMessage, [buffer,length],StageID); 
                    }finally{
                        _free(buffer);
                    }
                }); 
            }
            else if(e.data.slice(0, 2) == 'ri')
            {
                console.log(e.data.slice(2));
                var obj = JSON.parse(e.data.slice(2));
                console.log(obj);
                const keys = Object.keys(obj);
                keys.forEach(key => {
                    console.log(obj);
                    console.log("key:"+key +" "+ "value:"+obj[key]);
                    dynCall('vii', instance.SetRoomInfo, [key, obj[key]]);
                    console.log("finish_getroomInfo");
                });
            }
            else if(e.data.slice(0, 2) == 'me')
            {
                var message = e.data.slice(2);
                var length = lengthBytesUTF8(message) + 1;
                var buffer = _malloc(length);
                stringToUTF8(message, buffer, length);
                try{
                    dynCall('vii', instance.onMessage, [buffer,length]); 
                }finally{
                    _free(buffer);
                }
            }
            else if(e.data.slice(0, 2) == 'yr')
            {
                instance.MyRoomID = e.data.slice(2);
                console.log("YourRoomID:"+instance.MyRoomID)
            }

        };
    },
    Jslib_SendWebSocketMessage: function(str){
        console.log("send"+UTF8ToString(str)); 
        instances.socket.send('me'+UTF8ToString(str));
    },
    Jslib_CreateNewRoom: function()
    {
        console.log(instances.socket);
        instances.socket.send('cr' + instances.MyID);  
    },
    Jslib_GetRoomInfo: function()
    {
        console.log("sendMyID"+ instances.MyID);
        instances.socket.send('gr' + instances.MyID);
    },
    Jslib_JoinRoom: function(int)
    {
        const sendPacket = {ID:instances.MyID, RoomID:int}
        const json = JSON.stringify(sendPacket)
        console.log(json);
        instances.socket.send('jr' + json);
    },
    Jslib_JoinClient: function(str)
    {
        console.log(UTF8ToString('me'+str));
        instances.socket.send(UTF8ToString('me'+str));
    }

}

autoAddDeps(WebSocketLibrary, '$instances');

mergeInto(LibraryManager.library, WebSocketLibrary);

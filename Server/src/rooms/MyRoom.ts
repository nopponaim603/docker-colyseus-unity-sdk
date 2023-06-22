import { Room, Client } from "colyseus";
import { MyRoomState, Player } from "./schema/MyRoomState";

export type Position = {
  x: number,
  y: number
}

export class MyRoom extends Room<MyRoomState> {

  maxClients = 4;

  onCreate (options: any) {
    this.setState(new MyRoomState());

    this.onMessage("simple-chat", (client, message) => {
      //
      // handle "type" message
      //
      console.log("Message from " + client.sessionId + " have message -> " + message);
      
      // setting state
      this.state.mySynchronizedProperty = message;

      // broadcast information per request
      this.broadcast("simple-chat-from-server", {
        sessionID: client.sessionId,
        message: message,
      });
    });
  }

  onJoin (client: Client, options: any) {
    console.log(client.sessionId, "joined!");
    
    this.state.players.set(client.sessionId, new Player());

    // Send welcome message to the client.
    client.send("welcomeMessage", "Welcome to Colyseus!");

    // Listen to position changes from the client.
    this.onMessage("position", (client, position: Position) => {
      const player = this.state.players.get(client.sessionId);
      player.x = position.x;
      player.y = position.y;
    });
  }

  onLeave (client: Client, consented: boolean) {
    console.log(client.sessionId, "left!");
  }

  onDispose() {
    console.log("room", this.roomId, "disposing...");
  }

}

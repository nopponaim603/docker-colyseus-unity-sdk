// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 2.0.9
// 

using Colyseus.Schema;
using Action = System.Action;

public partial class MyRoomState : Schema {
	[Type(0, "map", typeof(MapSchema<Player>))]
	public MapSchema<Player> players = new MapSchema<Player>();

	[Type(1, "string")]
	public string mySynchronizedProperty = default(string);

	/*
	 * Support for individual property change callbacks below...
	 */

	protected event PropertyChangeHandler<MapSchema<Player>> _playersChange;
	public Action OnPlayersChange(PropertyChangeHandler<MapSchema<Player>> handler) {
		if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
		__callbacks.AddPropertyCallback(nameof(players));
		_playersChange += handler;
		return () => {
			__callbacks.RemovePropertyCallback(nameof(players));
			_playersChange -= handler;
		};
	}

	protected event PropertyChangeHandler<string> _mySynchronizedPropertyChange;
	public Action OnMySynchronizedPropertyChange(PropertyChangeHandler<string> handler) {
		if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
		__callbacks.AddPropertyCallback(nameof(mySynchronizedProperty));
		_mySynchronizedPropertyChange += handler;
		return () => {
			__callbacks.RemovePropertyCallback(nameof(mySynchronizedProperty));
			_mySynchronizedPropertyChange -= handler;
		};
	}

	protected override void TriggerFieldChange(DataChange change) {
		switch (change.Field) {
			case nameof(players): _playersChange?.Invoke((MapSchema<Player>) change.Value, (MapSchema<Player>) change.PreviousValue); break;
			case nameof(mySynchronizedProperty): _mySynchronizedPropertyChange?.Invoke((string) change.Value, (string) change.PreviousValue); break;
			default: break;
		}
	}
}


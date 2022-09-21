## `DataSender`

```csharp
public class UnityConnector.DataSender

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Single[]` | getAvatarSegmentRotations() | Get the rotations (quaternions) of the avatar's segments <br />  returns quaternions as a list of floats; quaternion order: (w, x, y, z) | 
| `Single[]` | getInvertedAvatarSegmentRotations() | Get the inverted rotations (quaternions) of the avatar's segments: <br />  inv = rotation * inv(initialRotation); result = (w, -x, -z, -y) | 
| `Single[]` | getInvertedShadowAvatarSegmentRotations() | Get the inverted rotations (quaternions) of the shadow avatar's segments: <br />  inv = rotation * inv(initialRotation); result = (w, -x, -z, -y) | 
| `Single[]` | getShadowAvatarSegmentRotations() | Get the rotations (quaternions) of the shadow avatar's segments<br />  returns quaternions as a list of floats; quaternion order: (w, x, y, z) | 
| `Int32` | initConnections(`String` ipAddr, `Int32` segmentPort, `Int32` shadowPort, `Int32` commandPort) | init connection with instant avatar (default: localhost, 4242, 4244, 4243)<br /> | 
| `void` | reconnect() | Request reconnection to the Unity InstantAvatar using last known (default) ip address and ports | 
| `Int32` | sendAvatarSegmentRotations(`Single[]` segment_floats) | send to the avatar new rotations (quaternions) to the initialized segments as list of floats;  length of the array must be the  4 * the number of initialized segments; expected quaternion order: (w, x, y, z) | 
| `Int32` | sendCommand(`String` message) | send command to avatar. Message options:<br /><code>camera,position</code> where position one of {front, left, right, topLeft, topRight}<br /><code>segments,segment1,segment2,...</code> where segments are elements of {Pelvis, LeftUpperLeg, LeftLowerLeg, LeftFoot,  RightUpperLeg, RightLowerLeg, RightFoot}<br /><code>switchScene</code> toggle between male and female avatar<br /><code>shadow,on</code> and <code>shadow,off</code> show or hide shadow avatar | 
| `String` | sendCommandWithReply(`String` message) | request-reply command. Message options: <br /><code>getCamPosition</code> -&gt; response: current camera position | 
| `Int32` | sendShadowAvatarSegmentRotations(`Single[]` segment_floats) | send to the shadow avatar new rotations (quaternions) to the initialized segments as list of floats;  length of the array must be the  4 * the number of initialized segments; expected quaternion order: (w, x, y, z) | 



/*========================================================================
Product:    #HMDGazeAnalyzer
Developer:  #Jonas Iacobi
Company:    #KTH | SVRVIVE Studios
Date:       #2018-04-18
========================================================================*/

Hello!
This explains how to set up the eyetracking analyzing software I made.
The software is operational but only intended as a proof of concept,
as is the demo. A short summary of the main points is available in
Summary.txt. Hopefully this all might prove useful.

Prerequisites:
From the SteamVR SDK, drag the [SteamVR] and [CameraRig] prefabs into the scene.
From the TobiiPro SDK, drag the [VREyeTacker] prefab into the scene.

Alternatively, use your own VR and eye tracking software. This will require a tweak in the HMDEyeRecording script to use your custom eye data object rather than the standard eye tracker's IVRGazeData.
The script is designed to be easily modified.

*Drag the HMDGazeRecorder and/or HMDGazeReplayer prefab(s) into the scene.

-Recorder:
*Drag the main camera (in SteamVR this corresponds to [CameraRig]->Camera(head)->Camera(eye)) to the Main Camera field.
*Optionally enter a custom file name. OIf no name is entered, the recorder will use the default name specified in HMDDataSettings.
*Set the toggle record key and/or use the public StartRecording(string filename) or StartRecording(), and StopRecording() methods.
*Remember to stop recording, otherwise data won't be saved.
*Hover over the fields in the inspector for a description.
**Attn: If not used with the demo, remove lines 116-120 which make a call to a demo prefab.

-Replayer:
*If a custom file name was used for recording, enter it into the appropriate field on the HMDDataLoader script.
*Set up the controls in the inspector (or better yet make a better system for controlling).
*Hover over the fields in the inspector for a description of the variable.
*Play the scene to try it out.

*All files relating to the recorder and replayer use the HMDGazeAnalyzing namespace to avoid conflicts.
*All files relating to the demo scene use the HMDDemo namespace.

-Demo:
*The Demo is set to start recording when the Start button is pressed, and end recording when a car image has been pressed.
*To replay data from the demo, it is easiest to disable the HMDDemo->UICanvas.

CREDITS:
Eyetracking: 
Hardware integration by Tobii
https://www.tobiipro.com/

SDK by Tobii/TobiiPro
https://www.tobiipro.com/product-listing/vr-integration/#GetStarted

Brick Texture: "15 Original Bricks Texures" by NevLext
https://assetstore.unity.com/publishers/23703

Test scene props: "Props for the Classroom" by WarKarma
www.assetstore.unity3d.com/en/#!/publisher/476 
http://vytautasramanauskas.wixsite.com/vr3d

Most of the rest by me
iacobi@kth.se
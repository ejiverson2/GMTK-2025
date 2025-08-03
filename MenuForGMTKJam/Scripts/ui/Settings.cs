using Godot;
using System;

public partial class Settings : Resource
{
	[Export(PropertyHint.Range, "-80,0,0.1")] public float MasterVolume = -6.0f;
	[Export(PropertyHint.Range, "-80,0,0.1")] public float MusicVolume = -6.0f;
	[Export(PropertyHint.Range, "-80,0,0.1")] public float SFXVolume = -6.0f;
	[Export] public bool MasterVolumeMuted = false;
	[Export] public bool MusicVolumeMuted = false;
	[Export] public bool SFXVolumeMuted = false;
	[Export] public Key pitchUpKey = Key.W;
	[Export] public Key pitchDownKey = Key.S;
	[Export] public bool debugOverlayAdvanced1 = false;
	[Export] public bool debugOverlayAdvanced2 = false;
	[Export] public bool debugOverlayAdvanced3 = false;
	[Export] public bool debugOverlayAdvanced4 = false;
	[Export] public bool debugOverlayAdvanced5 = false;
	[Export] public bool debugOverlayAdvanced6 = false;
	[Export] public bool ShowDebug = false;
	[Export] public NodePath PlayerPath;
	[Export] public float playerRotationSpeed = 360f;
	[Export] public float playerAirSpeed = 500f;
	[Export] public float playerGravity = 9800f;
	[Export] public int playerTrailMaxPoints = 500;
	[Export] public int playerTrailSegmentLength = 30;
}

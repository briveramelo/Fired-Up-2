﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FU{

    public static class FireFighter {
        public static Transform playerTransform;
        public static Transform playerShoulderTransform;
        public static Transform followSpotTransform;
        public static Transform pointsSpot;

        public static Quaternion LookAtPlayer(this Quaternion quat, Vector3 displayPosition){
            return Quaternion.LookRotation(displayPosition - FireFighter.playerTransform.position);
        }
    }

	#region Layers
	public static class Layers {
		#region LayerMasks
		public static class LayerMasks{
			public static LayerMask allFires = LayerMaskExtensions.Create (Fires.electricFire, Fires.solidFire, Fires.liquidFire);
			public static LayerMask allExtinguishers = LayerMaskExtensions.Create (Extinguishers.blackDeath, Extinguishers.kBomb, Extinguishers.sonicHose, Extinguishers.powder, Extinguishers.CO2);
			public static LayerMask allObjects = LayerMaskExtensions.Create (Rooms.furniture, Rooms.ground);
			public static LayerMask allPeople = LayerMaskExtensions.Create (People.NPC,People.you);
            public static LayerMask allCollectables = LayerMaskExtensions.Create(Collectables.CO2_Cannister, Collectables.Powder_Cannister, Collectables.K_Bomb, Collectables.BlackDeath, Collectables.SonicHose_Battery);
            public static LayerMask ground = LayerMaskExtensions.Create (Rooms.furniture, Rooms.ground, People.NPC);
            public static LayerMask groundAndWalls = LayerMaskExtensions.Create(Rooms.furniture, Rooms.ground, People.NPC, Rooms.walls);
        }
		#endregion

		#region Fires
		public static class Fires{
			public static int electricFire = 10;
			public static int solidFire = 11;
			public static int liquidFire = 12;

			public static string electricFireString = "ElectricFire";
			public static string solidFireString = "SolidFire";
			public static string liquidFireString = "LiquidFire";
		}
		#endregion

		#region Extinguishers
		public static class Extinguishers{
			public static int sonicHose = 13;
			public static int kBomb = 14;
			public static int blackDeath = 15;
            public static int powder = 28;
            public static int CO2 = 29;

            public static string sonicHoseString = "SonicHose";
			public static string kBombString = "KBomb";
			public static string blackDeathString = "BlackDeath";
		}
		#endregion

		#region Rooms
		public static class Rooms{
			public static int furniture = 17;
			public static int ground = 18;
            public static int walls = 30;
            public static int roomLocator = 26; 
            public static int room = 27;

			public static string furnitureString = "Furniture";
			public static string groundString = "Ground";
		}
        #endregion


        #region People
        public static class People{
			public static int NPC = 19;
			public static int you = 20;

			public static string NPCString = "NPC";
			public static string youString = "You";
		}
		#endregion

        #region Collectables
        public static class Collectables {
            public static int CO2_Cannister = 21;
            public static int Powder_Cannister = 22;
            public static int K_Bomb = 23;
            public static int BlackDeath = 24;
            public static int SonicHose_Battery = 25;
        }
        #endregion
    }
    #endregion

    #region Controls
    public static class Controls{

        public static string Forward;
        public static string Sideways;
        public static string LookUp;
        public static string LookSideways;
        public static string Jump;
        public static string UseHose;
        public static string ThrowGrenade;
        public static string ToggleHose;
        public static string ToggleGrenade;
        public static string FollowMe;
        public static string PickUpPlayer;
        public static string Squat;

        public static void SetControls (){
			if (Application.platform == RuntimePlatform.OSXPlayer ||
			    Application.platform == RuntimePlatform.OSXEditor ||
			    Application.platform == RuntimePlatform.OSXDashboardPlayer ||
			    Application.platform == RuntimePlatform.OSXWebPlayer){
			
				Forward = "Mac_Forward";
				Sideways = "Mac_Sideways";
				LookUp = "Mac_LookUp";
				LookSideways = "Mac_LookSideways";

				Jump = "Mac_Jump";
                UseHose = "Mac_UseHose";
                ThrowGrenade = "Mac_ThrowGrenade";
                ToggleHose = "Mac_ToggleHose";
                ToggleGrenade = "Mac_ToggleGrenade";
                FollowMe = "Mac_FollowMe";
                PickUpPlayer = "Mac_PickUpPlayer";
                Squat = "Mac_Squat";
            }
			else{
				Forward = "Win_Forward";
				Sideways = "Win_Sideways";
				LookUp = "Win_LookUp";
				LookSideways = "Win_LookSideways";
				
				Jump = "Win_Jump";
                UseHose = "Win_UseHose";
                ThrowGrenade = "Win_ThrowGrenade";
                ToggleHose = "Win_ToggleHose";
                ToggleGrenade = "Win_ToggleGrenade";
                FollowMe = "Win_FollowMe";
                PickUpPlayer = "Win_PickUpPlayer";
                Squat = "Win_Squat";
            }
		}

		
    }

    #endregion 

    public static class ExtensionMethods {
        public static Quaternion LookAtPlayer(this Transform trans, Vector3 displayPosition){
            return Quaternion.LookRotation(displayPosition - FireFighter.playerTransform.position);
        }
    }

    #region LayerMaskExtensions
    public static class LayerMaskExtensions{

		public static LayerMask Create(params string[] layerNames)
		{
			return NamesToMask(layerNames);
		}
		
		public static LayerMask Create(params int[] layerNumbers)
		{
			return LayerNumbersToMask(layerNumbers);
		}
		
		public static LayerMask NamesToMask(params string[] layerNames)
		{
			LayerMask ret = (LayerMask)0;
			foreach(var name in layerNames)
			{
				ret |= (1 << LayerMask.NameToLayer(name));
			}
			return ret;
		}
		
		public static LayerMask LayerNumbersToMask(params int[] layerNumbers)
		{
			LayerMask ret = (LayerMask)0;
			foreach(var layer in layerNumbers)
			{
				ret |= (1 << layer);
			}
			return ret;
		}
		
		public static LayerMask Inverse(this LayerMask original)
		{
			return ~original;
		}
		
		public static LayerMask AddToMask(this LayerMask original, params string[] layerNames)
		{
			return original | NamesToMask(layerNames);
		}
		
		public static LayerMask RemoveFromMask(this LayerMask original, params string[] layerNames)
		{
			LayerMask invertedOriginal = ~original;
			return ~(invertedOriginal | NamesToMask(layerNames));
		}
		
		public static string[] MaskToNames(this LayerMask original)
		{
			var output = new List<string>();
			
			for (int i = 0; i < 32; ++i)
			{
				int shifted = 1 << i;
				if ((original & shifted) == shifted)
				{
					string layerName = LayerMask.LayerToName(i);
					if (!string.IsNullOrEmpty(layerName))
					{
						output.Add(layerName);
					}
				}
			}
			return output.ToArray();
		}
		
		public static string MaskToString(this LayerMask original)
		{
			return MaskToString(original, ", ");
		}
		
		public static string MaskToString(this LayerMask original, string delimiter)
		{
			return string.Join(delimiter, MaskToNames(original));
		}

		public static bool IsInLayerMask(GameObject obj, LayerMask mask){
			return ((mask.value & (1<< obj.layer))>0);
		}
	}
	#endregion
}

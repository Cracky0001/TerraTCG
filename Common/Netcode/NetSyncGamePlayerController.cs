﻿using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using TerraTCG.Common.GameSystem;
using TerraTCG.Common.GameSystem.Drawing;
using TerraTCG.Common.GameSystem.GameState;
using TerraTCG.Content.NPCs;

namespace TerraTCG.Common.Netcode
{
	// Implementation of IGamePlayerController that performs actions based on 
	internal class NetSyncGamePlayerController : IGamePlayerController
	{
		public GamePlayer GamePlayer { get ; set ; }
		public CardCollection Deck { get ; set ; }
		public string DeckName { get ; set ; }

		public bool ShouldShuffle { get => false; }

		public List<NPCDuelReward> Rewards { get; set; }

		public Asset<Texture2D> Sleeve => TextureCache.Instance.CardSleeves[CardSleeve.FOREST];

		public void EndGame()
		{
			// TODO
		}

		public void StartGame(GamePlayer player, CardGame game)
		{
			GamePlayer = player;
		}
	}

	internal class NetSyncPlayerSystem : ModSystem
	{
		public static NetSyncPlayerSystem Instance => ModContent.GetInstance<NetSyncPlayerSystem>();

		public Dictionary<int, NetSyncGamePlayerController> SyncPlayerMap { get; private set; } = [];

		public NetSyncGamePlayerController RegisterPlayer(int playerId, CardCollection playerDecklist)
		{
			var controller = new NetSyncGamePlayerController()
			{
				Deck = playerDecklist
			};

			SyncPlayerMap[playerId] = controller;
			return controller;
		}
	}
}

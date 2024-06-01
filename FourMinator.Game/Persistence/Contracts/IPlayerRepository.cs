﻿using FourMinator.Persistence.Domain.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.GameServices.Persistence.Contracts
{
    public interface IPlayerRepository
    {

        public Task<Player> CreatePlayer(Player player);

        public Task<bool> UpdatePlayerState(uint playerId, PlayerState state);

        public Task<Player> GetPlayerById(uint playerId);

        public Task<Player> GetPlayerByUserId(int userId);
    }
}
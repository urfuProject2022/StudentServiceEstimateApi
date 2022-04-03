﻿using System.Threading.Tasks;
using MongoDB.Bson;
using StudentEstimateServiceApi.Common;
using StudentEstimateServiceApi.Models;
using StudentEstimateServiceApi.Repositories.Interfaces;

namespace StudentEstimateServiceApi.Infrastructure.Services.InviteService
{
    public class InviteService : IInviteService
    {
        private readonly IRoomRepository roomRepository;
        private readonly IUserRepository userRepository;

        public InviteService(IRoomRepository roomRepository, IUserRepository userRepository)
        {
            this.roomRepository = roomRepository;
            this.userRepository = userRepository;
        }

        public async Task<OperationResult> Accept(string roomId, ObjectId userId)
        {
            var findRoomResult = await roomRepository.FindById(roomId);

            if (findRoomResult.IsError)
                return OperationResult.Fail("Room not found");

            var findUserResult = await userRepository.FindById(userId);

            if (findUserResult.IsError)
                return OperationResult.Fail("User not found");

            var room = findRoomResult.Result;
            var user = findUserResult.Result;

            if (IsUserInRoom(room, userId))
                return OperationResult.Success();

            user.Rooms.Add(room.Id);
            if ((await userRepository.Update(user)).IsError)
                return OperationResult.Fail("Failed give access");

            room.Users.Add(userId);
            if ((await roomRepository.Update(room)).IsError)
                return OperationResult.Fail("Failed give access");

            return OperationResult.Success();
        }

        public OperationResult<string> GenerateInviteUrl(string domain, ObjectId roomId)
        {
            if (domain == null)
                return OperationResult<string>.Fail("Wrong domain");

            return OperationResult<string>.Success($"https://{domain}/api/invites/accept?roomId={roomId}");
        }

        private static bool IsUserInRoom(Room room, ObjectId user)
        {
            return room.Users.Contains(user) || room.OwnerId == user;
        }
    }
}
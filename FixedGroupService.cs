using System;
using System.Collections.Generic;
using Xmu.Crms.Shared.Models;
using Xmu.Crms.Shared.Service;

namespace Xmu.Crms.Services.Insomnia
{
    internal class FixedGroupService : IFixGroupService
    {
        public long InsertFixGroupByClassId(long classId, long userId) => throw new NotImplementedException();

        public void DeleteFixGroupMemberByFixGroupId(long fixGroupId)
        {
            throw new NotImplementedException();
        }

        public long InsertFixGroupMemberById(long userId, long groupId) => throw new NotImplementedException();

        public List<UserInfo> ListFixGroupMemberByGroupId(long groupId) => throw new NotImplementedException();

        public List<FixGroup> ListFixGroupByClassId(long classId) => throw new NotImplementedException();

        public void DeleteFixGroupByClassId(long classId)
        {
            throw new NotImplementedException();
        }

        public void DeleteFixGroupByGroupId(long groupId)
        {
            throw new NotImplementedException();
        }

        public void UpdateFixGroupByGroupId(long groupId, FixGroup fixGroupBo)
        {
            throw new NotImplementedException();
        }

        public FixGroup GetFixGroupByGroupId(long groupId) => throw new NotImplementedException();

        public void DeleteTopicByGroupId(long groupId)
        {
            throw new NotImplementedException();
        }

        public FixGroup GetFixedGroupById(long userId, long classId) => throw new NotImplementedException();

        public void UpdateSeminarGroupById(long groupId, SeminarGroup group)
        {
            throw new NotImplementedException();
        }
    }
}
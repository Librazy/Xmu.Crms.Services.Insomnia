using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xmu.Crms.Shared.Exceptions;
using Xmu.Crms.Shared.Models;
using Xmu.Crms.Shared.Service;

namespace Xmu.Crms.Services.Insomnia
{
    public class FixedGroupService : IFixGroupService
    {
        private readonly CrmsContext _db;

        public FixedGroupService(CrmsContext db)
        {
            _db = db;
        }

        public long InsertFixGroupByClassId(long classId, long userId)
        {
            if (classId < 0)
            {
                throw new ArgumentException(nameof(classId));
            }
            if (classId < 0)
            {
                throw new ArgumentException(nameof(userId));
            }
            var cls = _db.ClassInfo.Find(classId) ?? throw new ClassNotFoundException();
            var usr = _db.UserInfo.Find(userId) ?? throw new UserNotFoundException();
            var fg = _db.FixGroup.Add(new FixGroup {ClassInfo = cls, Leader = usr});
            _db.SaveChanges();
            return fg.Entity.Id;
        }

        public void DeleteFixGroupMemberByFixGroupId(long fixGroupId)
        {
            throw new NotImplementedException();
        }

        public long InsertFixGroupMemberById(long userId, long groupId)
        {
            throw new NotImplementedException();
        }

        public List<UserInfo> ListFixGroupMemberByGroupId(long groupId)
        {
            throw new NotImplementedException();
        }

        public List<FixGroup> ListFixGroupByClassId(long classId)
        {
            throw new NotImplementedException();
        }

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

        public List<FixGroupMember> GetFixGroupByGroupId(long groupId)
        {
            throw new NotImplementedException();
        }

        public long insertStudentIntoGroup(long userId, long groupId)
        {
            throw new NotImplementedException();
        }

        public void DeleteTopicByGroupId(long groupId)
        {
            throw new NotImplementedException();
        }

        public FixGroup GetFixedGroupById(long userId, long classId)
        {
            throw new NotImplementedException();
        }

        public void UpdateSeminarGroupById(long groupId, SeminarGroup @group)
        {
            throw new NotImplementedException();
        }

        public void fixedGroupToSeminarGroup(long semianrId, long fixedGroupId)
        {
            throw new NotImplementedException();
        }

        public void FixedGroupToSeminarGroup(long seminarId)
        {
            Debug.WriteLine($"=====FixedGroupToSeminarGroup { seminarId } { DateTime.Now }=====");
        }
    }
}
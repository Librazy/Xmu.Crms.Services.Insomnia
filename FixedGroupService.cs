using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
            if (classId <= 0)
            {
                throw new ArgumentException(nameof(classId));
            }
            if (userId <= 0)
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
            if (fixGroupId <= 0)
            {
                throw new ArgumentException(nameof(fixGroupId));
            }
            _db.FixGroupMember.RemoveRange(_db.FixGroupMember.Include(m => m.FixGroup)
                .Where(m => m.FixGroup.Id == fixGroupId));
            _db.SaveChanges();
        }

        public long InsertFixGroupMemberById(long userId, long groupId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException(nameof(userId));
            }
            if (groupId <= 0)
            {
                throw new ArgumentException(nameof(groupId));
            }
            var grp = _db.FixGroup.Find(groupId) ?? throw new FixGroupNotFoundException();
            var usr = _db.UserInfo.Find(userId) ?? throw new UserNotFoundException();
            var fgm = _db.FixGroupMember.Add(new FixGroupMember() {FixGroup = grp, Student = usr});
            _db.SaveChanges();
            return fgm.Entity.Id;
        }

        public IList<UserInfo> ListFixGroupMemberByGroupId(long groupId)
        {
            return _db.FixGroupMember.Include(f => f.FixGroup).Include(f => f.Student)
                .Where(f => f.FixGroup.Id == groupId).Select(f => f.Student).ToList();
        }

        public IList<FixGroup> ListFixGroupByClassId(long classId)
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

        public IList<FixGroupMember> GetFixGroupByGroupId(long groupId)
        {
            throw new NotImplementedException();
        }

        public long InsertStudentIntoGroup(long userId, long groupId)
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

        public void FixedGroupToSeminarGroup(long semianrId, long fixedGroupId)
        {
            throw new NotImplementedException();
        }

        public void FixedGroupToSeminarGroup(long seminarId)
        {
            Debug.WriteLine($"=====FixedGroupToSeminarGroup { seminarId } { DateTime.Now }=====");
        }
    }
}
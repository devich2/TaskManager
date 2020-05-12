using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Entities.Tables;

namespace TaskManager.Seed
{
    public class UnitEntitiesHolder
    {
        public List<Unit> GetUnits()
        {
            return new List<Unit>();
        }

        public List<Tag> GeTags()
        {
            return new List<Tag>();
        }

    }
}

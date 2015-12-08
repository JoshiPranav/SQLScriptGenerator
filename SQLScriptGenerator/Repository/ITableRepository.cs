﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLScriptGenerator.Entities;

namespace SQLScriptGenerator.Repository
{
    public interface ITableRepository
    {
        IEnumerable<TableDefinition> GetTableList();
    }
}

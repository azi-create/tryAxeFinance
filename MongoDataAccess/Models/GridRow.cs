﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDataAccess.Models
{
    public class GridRow
    {
        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        public List<GridCell> Cells { get; set; }
    }
}

﻿using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsCombustible.Listar
{
    public interface IListarDocsCombustibleLN
    {
        Task<List<DocsCombustibleDTO>> listar();
    }
}

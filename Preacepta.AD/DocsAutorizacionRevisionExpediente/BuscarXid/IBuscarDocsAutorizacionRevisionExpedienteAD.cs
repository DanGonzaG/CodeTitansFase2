﻿using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsAutorizacionRevisionExpediente.BuscarXid
{
    public interface IBuscarDocsAutorizacionRevisionExpedienteAD
    {
        Task<TDocsAutorizacionRevisionExpediente?> buscar(int id);
    }
}

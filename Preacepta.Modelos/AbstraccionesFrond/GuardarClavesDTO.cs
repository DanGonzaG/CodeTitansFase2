using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class GuardarClavesDTO
    {
        public string PublicKeyBase64 { get; set; }
        public string EncryptedPrivateKey { get; set; }
        public string Salt { get; set; }
        public string IV { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TCC.Models
{
    public class ModelPet
    {
        public string codPet { get; set; }

        [DisplayName("Nome")]
        public string nomePet { get; set; }
        public string imagePet { get; set; }

        public string portePet { get; set; }

        public string sexoPet { get; set; }
        [DisplayName("Raça")]
        public string racaPet { get; set; }
        public string especiePet { get; set; }


        public string codEspeciePet { get; set; }
        public string codClientePet { get; set; }

    }
}
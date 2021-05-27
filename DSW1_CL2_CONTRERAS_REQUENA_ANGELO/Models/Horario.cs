using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSW1_CL2_CONTRERAS_REQUENA_ANGELO.Models
{
    public class Horario
    {
        public int codHorario { get; set; }
        public string nomCurso { get; set; }
        public int codCurso { get; set; }
        public DateTime fecInicio { get; set; }
        public DateTime fecTerminio { get; set; }
        public int vacantes { get; set; }
    }
}
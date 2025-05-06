using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioDataSystem.Domain.Enums
{
    public enum Status : int
    {
        [Description("Pendente")]
        Pendente = 1,
        [Description("Em Progresso")]
        EmProgresso = 2,
        [Description("Concluído")]
        Concluida = 3,
    }
}
